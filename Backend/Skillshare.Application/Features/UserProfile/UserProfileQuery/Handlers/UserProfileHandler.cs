using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.Features.UserProfile.UserProfileQuery.Models;
using Skillshare.Application.ResponseHandler;
using Skillshare.Application.Shared;
using Skillshare.Contracts.DTOs.UserProfileDTOs;
using Skillshare.Contracts.ServicesContracts;

namespace Skillshare.Application.Features.UserProfile.UserProfileQuery.Handlers
{
    internal class UserProfileHandler : ResponseHandlerModel,
        IRequestHandler<GetuserProfileQueryModel, ResponseModel<UserProfileDTO>>,
        IRequestHandler<GetUserImageProfileModelQuery, ResponseModel<string>>
    {
        private readonly IUserProfileServices _UserProfileServices;

        public UserProfileHandler(IStringLocalizer<Sharedresources> stringLocalizer, IUserProfileServices userProfileServices) : base(stringLocalizer)
        {
            _UserProfileServices = userProfileServices;
        }

        public async Task<ResponseModel<UserProfileDTO>> Handle(GetuserProfileQueryModel request, CancellationToken cancellationToken)
        {
            var Response = await _UserProfileServices.GetUserprofile(request.UserId);

            if (Response == null)
                return BadRequest<UserProfileDTO>();

            return Success(Response);
        }

        public async Task<ResponseModel<string>> Handle(GetUserImageProfileModelQuery request, CancellationToken cancellationToken)
        {
            var Response = await _UserProfileServices.GetUserProfileImage(request.UserId);

            if (Response is null)
            {
                return BadRequest<string>();
            }
            return Success(Response);
        }
    }
}