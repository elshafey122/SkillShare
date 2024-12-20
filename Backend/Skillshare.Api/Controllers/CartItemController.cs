﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillshare.Application.Features.Cart.CartCommand.Models;
using Skillshare.Contracts.DTOs.CartItem;

namespace Skillshare.Api.Controllers
{
    [Authorize]
    public class CartItemController : AppBaseController
    {
        private readonly IMediator _Mediator;

        public CartItemController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem(CartItemForCreate cartItemForCreate)
        {
            var Result = await _Mediator.Send(new CreateCartModelCommand(cartItemForCreate));
            return NewResult(Result);
        }

        [HttpDelete("RemoveCartItem")]
        public async Task<IActionResult> RemoveCartItem(int CartItemId, string userId)
        {
            var Result = await _Mediator.Send(new RemoveCartitemModelCommand(CartItemId, userId));
            return NewResult(Result);
        }
    }
}