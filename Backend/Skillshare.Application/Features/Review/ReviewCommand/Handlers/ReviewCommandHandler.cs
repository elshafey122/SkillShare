using MediatR;
using Microsoft.Extensions.Localization;
using Skillshare.Application.Features.Review.ReviewCommand.Models;
using Skillshare.Application.ResponseHandler;
using Skillshare.Application.Shared;
using Skillshare.Contracts.ServicesContracts;

namespace Skillshare.Application.Features.Review.ReviewCommand.Handlers
{
    public class ReviewCommandHandler : ResponseHandlerModel,
          IRequestHandler<ReviewForCreateModelCommand, ResponseModel<bool>>
    {
        private readonly IReviewService _ReviewService;

        public ReviewCommandHandler(IStringLocalizer<Sharedresources> stringLocalizer,
            IReviewService reviewService) : base(stringLocalizer)
        {
            _ReviewService = reviewService;
        }

        public async Task<ResponseModel<bool>> Handle(ReviewForCreateModelCommand request, CancellationToken cancellationToken)
        {
            var isAdded = await _ReviewService.CreateReview(request.reviewForCreateDto);

            if (!isAdded)
            {
                return BadRequest<bool>();
            }
            return Success(isAdded);
        }
    }
}