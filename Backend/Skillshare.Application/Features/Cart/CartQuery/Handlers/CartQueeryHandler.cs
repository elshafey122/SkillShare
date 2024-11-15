using MediatR;
using Microsoft.Extensions.Localization;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.Features.Cart.CartCommand.Models;
using Skillshare.Application.Features.Cart.CartQuery.Models;
using Skillshare.Application.ResponseHandler;
using Skillshare.Application.Shared;
using Skillshare.Contracts.DTOs.CartItem;
using Skillshare.Contracts.ServicesContracts;

namespace Skillshare.Application.Features.Cart.CartQuery.Handlers
{
    public class CartQueeryHandler : ResponseHandlerModel,
        IRequestHandler<GetCartModelQuery, ResponseModel<CartForReturn>>

    {
        private readonly ICartService _CartService;
        private readonly ICartItemService _CartItemService;

        public CartQueeryHandler(
            IStringLocalizer<Sharedresources> stringLocalizer,
            ICartService cartService,
            ICartItemService cartItemService
            ) : base(stringLocalizer)
        {
            _CartService = cartService;
            _CartItemService = cartItemService;
        }

        public async Task<ResponseModel<CartForReturn>> Handle(GetCartModelQuery request, CancellationToken cancellationToken)
        {
            var Result = await _CartService.GetCartsByUser(request.userId);

            if (Result is null)
                return BadRequest<CartForReturn>();

            return Success(Result);
        }
    }
}