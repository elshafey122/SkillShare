using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.CartItem;

namespace Skillshare.Application.Features.Cart.CartQuery.Models
{
    public record GetCartModelQuery(string userId) : IRequest<ResponseModel<CartForReturn>>;
}