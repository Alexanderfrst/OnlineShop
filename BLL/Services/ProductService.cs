using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _prodRepo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository prodRepo, IMapper mapper)
        {
            _prodRepo = prodRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<ProductDto>>(await _prodRepo.GetAllAsync());

        public async Task<ProductDto> GetByIdAsync(int id) =>
            _mapper.Map<ProductDto>(await _prodRepo.GetByIdAsync(id));

        public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId) =>
            _mapper.Map<IEnumerable<ProductDto>>(await _prodRepo.GetByCategoryAsync(categoryId));

        public async Task<IEnumerable<ProductDto>> SearchAsync(string term) =>
            _mapper.Map<IEnumerable<ProductDto>>(await _prodRepo.SearchAsync(term));

        public async Task CreateAsync(ProductDto dto)
        {
            var prod = _mapper.Map<Product>(dto);
            await _prodRepo.AddAsync(prod);
        }

        public async Task UpdateAsync(ProductDto dto)
        {
            var prod = _mapper.Map<Product>(dto);
            await _prodRepo.UpdateAsync(prod);
        }

        public async Task DeleteAsync(int id) =>
            await _prodRepo.DeleteAsync(id);
    }
}
