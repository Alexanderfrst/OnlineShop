using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repo;
        private readonly IMapper _mapper;
        public ReviewService(IReviewRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

        public async Task<IEnumerable<ReviewDto>> GetByProductAsync(int productId, CancellationToken cancellationToken)
        {
            var list = await _repo.GetByProductAsync((int)productId, cancellationToken);
            return _mapper.Map<IEnumerable<ReviewDto>>(list);
        }

        public async Task AddAsync(int productId, ReviewDto review, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<DAL.Models.Review>(review) with { ProductId = (int)productId };
            await _repo.AddAsync(entity, cancellationToken);
        }
    }
}