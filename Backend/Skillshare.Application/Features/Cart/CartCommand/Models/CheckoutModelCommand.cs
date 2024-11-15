using MediatR;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.CartItem;

namespace Skillshare.Application.Features.Cart.CartCommand.Models
{
    public record CheckoutModelCommand(CheckOutProperties checkOutProperties) : IRequest<ResponseModel<Session>>;
}