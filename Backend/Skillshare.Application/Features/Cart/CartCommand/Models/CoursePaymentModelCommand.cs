using MediatR;

using Skillshare.Application.ResponseHandler;

namespace Skillshare.Application.Features.Cart.CartCommand.Models
{
    public record CoursePaymentModelCommand(string userId) : IRequest<ResponseModel<bool>>;
}