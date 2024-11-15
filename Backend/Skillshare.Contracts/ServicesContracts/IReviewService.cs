using Skillshare.Contracts.DTOs.ReviewDTOs;

namespace Skillshare.Contracts.ServicesContracts
{
    public interface IReviewService
    {
        Task<bool> CreateReview(ReviewForCreateDto reviewForCreateDto);
    }
}