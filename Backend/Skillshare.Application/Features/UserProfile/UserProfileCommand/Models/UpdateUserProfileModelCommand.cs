using MediatR;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.UserProfileDTOs;

namespace Skillshare.Application.Features.UserProfile.UserProfileCommand.Models
{
    public record UpdateUserProfileModelCommand(UserProfileDTO userProfile) : IRequest<ResponseModel<bool>>;
}