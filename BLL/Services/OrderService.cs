using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly IUserRepository _userRepo;
        private readonly IOrderItemRepository _itemRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IPromoCodeRepository _promoCodeRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public OrderService(
            IOrderRepository orderRepo,
            IProductRepository productRepo,
            IUserRepository userRepo,
            IOrderItemRepository itemRepo,
            ICartRepository cartRepo,
            IPromoCodeRepository promoCodeRepo,
            IMapper mapper,
            IConfiguration config)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _userRepo = userRepo;
            _itemRepo = itemRepo;
            _cartRepo = cartRepo;
            _promoCodeRepo = promoCodeRepo;
            _mapper = mapper;
            _config = config;
        }

        public async Task<IEnumerable<OrderDto>> GetByUserAsync(int userId, CancellationToken cancellationToken)
        {
            var orders = await _orderRepo.GetByUserAsync(userId, cancellationToken);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> CreateOrderAsync(
            int userId,
            string deliveryMethod,
            string promoCode,
            CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(userId, cancellationToken);
            if (user == null)
                throw new ArgumentException($"User with ID {userId} not found.");

            var cart = await _cartRepo.GetByUserAsync(userId, cancellationToken);
            if (cart == null || !cart.Items.Any())
                throw new InvalidOperationException("Your cart is empty. Please add items to your cart before placing an order.");

            foreach (var cartItem in cart.Items)
            {
                var product = await _productRepo.GetByIdAsync(cartItem.ProductId, cancellationToken);
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {cartItem.ProductId} not found.");

                if (cartItem.Quantity > product.StockQuantity)
                    throw new InvalidOperationException($"Not enough stock for product {product.Name}. Available quantity: {product.StockQuantity}, requested: {cartItem.Quantity}");
            }

            if (string.IsNullOrEmpty(deliveryMethod))
                throw new ArgumentException("Delivery method is required.");

            decimal discount = 0m;
            if (!string.IsNullOrEmpty(promoCode))
            {
                var promo = await _promoCodeRepo.GetByCodeAsync(promoCode, cancellationToken);
                if (promo == null || !promo.IsActive || promo.ExpiryDate < DateTime.UtcNow)
                {
                    throw new InvalidOperationException("Invalid or expired promo code.");
                }
                discount = promo.DiscountValue;
            }

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Status = "Created",
                DeliveryMethod = deliveryMethod,
                TotalPrice = 0m
            };
            await _orderRepo.AddAsync(order, cancellationToken);

            decimal totalPrice = 0m;
            var orderItems = new List<OrderItem>();
            foreach (var cartItem in cart.Items)
            {
                var product = await _productRepo.GetByIdAsync(cartItem.ProductId, cancellationToken);
                if (product == null)
                    continue;

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = cartItem.Quantity,
                    UnitPrice = product.Price
                };
                totalPrice += orderItem.Quantity * orderItem.UnitPrice;

                orderItems.Add(orderItem);
            }

            totalPrice -= discount;

            order.TotalPrice = totalPrice;
            await _orderRepo.UpdateAsync(order, cancellationToken);

            foreach (var orderItem in orderItems)
            {
                await _itemRepo.AddAsync(orderItem, cancellationToken);
            }

            await SendOrderConfirmationEmail(order, cancellationToken);

            return _mapper.Map<OrderDto>(order);
        }


        private async Task SendOrderConfirmationEmail(Order order, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(order.UserId, cancellationToken);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                _config["Email:FromName"],
                _config["Email:FromAddress"]));
            message.To.Add(new MailboxAddress(user.Email, user.Email));
            message.Subject = $"Your order #{order.Id} is confirmed";

            var body = $@"
                <h3>Thank you for your order!</h3>
                <p>Order ID: {order.Id}</p>
                <p>Total: {order.TotalPrice:C}</p>
                <p>Delivery method: {order.DeliveryMethod}</p>
                <p>Status: {order.Status}</p>
            ";
            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _config["Email:SmtpHost"],
                int.Parse(_config["Email:SmtpPort"]),
                false, cancellationToken);
            await client.AuthenticateAsync(
                _config["Email:SmtpUser"],
                _config["Email:SmtpPass"], cancellationToken);
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }

        public async Task<OrderDto> UpdateStatusAsync(
            int orderId,
            string newStatus,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepo.GetDetailsAsync(orderId, cancellationToken);
            if (order == null)
                throw new ArgumentException($"Order with ID {orderId} not found.");

            order.Status = newStatus;
            await _orderRepo.UpdateAsync(order, cancellationToken);

            await SendStatusChangeEmail(order, cancellationToken);

            return _mapper.Map<OrderDto>(order);
        }

        private async Task SendStatusChangeEmail(Order order, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(order.UserId, cancellationToken);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                _config["Email:FromName"],
                _config["Email:FromAddress"]));
            message.To.Add(new MailboxAddress(user.Email, user.Email));
            message.Subject = $"Order #{order.Id} status updated";

            var body = $@"
                <p>Your order <strong>#{order.Id}</strong> status has changed to:</p>
                <p><strong>{order.Status}</strong></p>
            ";
            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _config["Email:SmtpHost"],
                int.Parse(_config["Email:SmtpPort"]),
                false, cancellationToken);
            await client.AuthenticateAsync(
                _config["Email:SmtpUser"],
                _config["Email:SmtpPass"], cancellationToken);
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}
