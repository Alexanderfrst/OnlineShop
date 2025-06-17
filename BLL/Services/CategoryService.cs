using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _catRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository catRepo, IMapper mapper)
        {
            _catRepo = catRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<CategoryDto>>(await _catRepo.GetAllAsync());

        public async Task<CategoryDto> GetByIdAsync(int id) =>
            _mapper.Map<CategoryDto>(await _catRepo.GetByIdAsync(id));

        public async Task<IEnumerable<CategoryDto>> GetChildrenAsync(int parentId) =>
            _mapper.Map<IEnumerable<CategoryDto>>(await _catRepo.GetChildrenAsync(parentId));

        public async Task CreateAsync(CategoryDto dto) =>
            await _catRepo.AddAsync(_mapper.Map<Category>(dto));

        public async Task UpdateAsync(CategoryDto dto) =>
            await _catRepo.UpdateAsync(_mapper.Map<Category>(dto));

        public async Task DeleteAsync(int id) =>
            await _catRepo.DeleteAsync(id);
    }
}