using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;


namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _repo.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _repo.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            var products = await _repo.GetByCategoryAsync(categoryId, cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> SearchAsync(string term, CancellationToken cancellationToken)
        {
            var products = await _repo.SearchAsync(term, cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task CreateAsync(ProductDto productDto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<DAL.Models.Product>(productDto);
            await _repo.AddAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(ProductDto productDto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<DAL.Models.Product>(productDto);
            await _repo.UpdateAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<ProductDto>> GetFilteredAsync(
            int? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            bool? inStock,
            string sortBy,
            bool ascending,
            CancellationToken cancellationToken)
        {
            var products = await _repo.GetFilteredAsync(
                categoryId,
                minPrice,
                maxPrice,
                inStock,
                sortBy,
                ascending,
                cancellationToken);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
