using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetChildrenAsync(int parentId, CancellationToken cancellationToken)
        {
            var children = await _categoryRepository.GetChildrenAsync(parentId, cancellationToken);
            return _mapper.Map<IEnumerable<CategoryDto>>(children);
        }

        public async Task CreateAsync(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddAsync(category, cancellationToken);
        }

        public async Task UpdateAsync(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.UpdateAsync(category, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _categoryRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
