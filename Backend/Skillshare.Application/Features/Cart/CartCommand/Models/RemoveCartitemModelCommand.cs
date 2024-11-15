using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;

namespace Skillshare.Application.Features.Cart.CartCommand.Models
{
    public record RemoveCartitemModelCommand(int CartitemId, string userId) : IRequest<ResponseModel<bool>>;
}