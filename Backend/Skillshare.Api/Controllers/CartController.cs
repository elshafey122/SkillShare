using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skillshare.Application.Features.Cart.CartCommand.Models;
using Skillshare.Application.Features.Cart.CartQuery.Models;
using Skillshare.Contracts.DTOs.CartItem;

namespace Skillshare.Api.Controllers
{
    [Authorize]
    public class CartController : AppBaseController
    {
        private readonly IMediator _Mediator;

        public CartController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetCartByUserId(string userId)
        {
            var Result = await _Mediator.Send(new GetCartModelQuery(userId));

            return NewResult(Result);
        }

        [HttpGet("CoursePaymentConfirmation")]
        public async Task<ActionResult> CoursePaymentConfirmation(string userId)
        {
            var Result = await _Mediator.Send(new CoursePaymentModelCommand(userId));

            return NewResult(Result);
        }

        [HttpPost("CheckOut")]
        public async Task<ActionResult> Checkout([FromBody] CheckOutProperties checkOutProperties)
        {
            var Result = await _Mediator.Send(new CheckoutModelCommand(checkOutProperties));

            return NewResult(Result);
        }
    }
}