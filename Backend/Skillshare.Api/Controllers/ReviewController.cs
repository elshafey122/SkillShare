using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skillshare.Application.Features.Review.ReviewCommand.Models;
using Skillshare.Contracts.DTOs.ReviewDTOs;

namespace Skillshare.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class ReviewController : AppBaseController
    {
        private readonly IMediator _Mediator;

        public ReviewController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview(ReviewForCreateDto reviewForCreateDto)
        {
            var Response = await _Mediator.Send(new ReviewForCreateModelCommand(reviewForCreateDto));

            return NewResult(Response);
        }
    }
}