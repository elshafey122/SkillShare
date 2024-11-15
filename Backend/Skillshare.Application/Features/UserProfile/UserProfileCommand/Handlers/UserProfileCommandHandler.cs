using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.Features.UserProfile.UserProfileCommand.Models;
using Skillshare.Application.ResponseHandler;
using Skillshare.Application.ServicesImplementation.UserProfileimplementation;
using Skillshare.Application.Shared;
using Skillshare.Contracts.DTOs.UserProfileDTOs;
using Skillshare.Contracts.ServicesContracts;

namespace Skillshare.Application.Features.UserProfile.UserProfileCommand.Handlers
{
    internal class UserProfileCommandHandler : ResponseHandlerModel,
        IRequestHandler<UpdateUserProfileModelCommand, ResponseModel<bool>>,
        IRequestHandler<UploadUserImageProfileModelCommand, ResponseModel<bool>>,
        IRequestHandler<ChangePasswordModelCommand, ResponseModel<ResultOfChangePassword>>

    {
        private readonly IUserProfileServices _UserProfileServices;

        public UserProfileCommandHandler(IStringLocalizer<Sharedresources> stringLocalizer, IUserProfileServices userProfileServices) : base(stringLocalizer)
        {
            _UserProfileServices = userProfileServices;
        }

        public async Task<ResponseModel<bool>> Handle(UpdateUserProfileModelCommand request, CancellationToken cancellationToken)
        {
            var IsUpdated = await _UserProfileServices.UpdateUserprofile(request.userProfile);

            if (!IsUpdated)
                return BadRequest<bool>();

            return Success(IsUpdated);
        }

        public async Task<ResponseModel<bool>> Handle(UploadUserImageProfileModelCommand request, CancellationToken cancellationToken)
        {
            var IsUpdated = await _UserProfileServices.UploadProfileImage(request.Userprof);
            if (!IsUpdated)
                return BadRequest<bool>();

            return Success(IsUpdated);
        }

        public async Task<ResponseModel<ResultOfChangePassword>> Handle(ChangePasswordModelCommand request, CancellationToken cancellationToken)
        {
            var Response = await _UserProfileServices.ChangePassword(request.changePasssword);
            if (Response is null)
            {
                return NotFound<ResultOfChangePassword>();
            }

            if (!Response.isSucceded)
            {
                return BadRequest<ResultOfChangePassword>(Response.Message);
            }
            return Success(Response);
        }
    }
}