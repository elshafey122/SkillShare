﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillshare.Application.Features.UserProfile.UserProfileCommand.Models;
using Skillshare.Application.Features.UserProfile.UserProfileQuery.Models;
using Skillshare.Contracts.DTOs.UserProfileDTOs;

namespace Skillshare.Api.Controllers
{
    [Authorize]
    public class UserProfileController : AppBaseController
    {
        private readonly IMediator _Mediator;
        public UserProfileController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpPut("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserprofile(UserProfileDTO userProfile)
        {
            var Response = await _Mediator.Send(new UpdateUserProfileModelCommand(userProfile));

            return NewResult(Response);
        }

        [HttpPut("UploadUserImage")]
        public async Task<IActionResult> UploadUserImage([FromForm] UploadUserprofileDTO userProfile)
        {
            var Response = await _Mediator.Send(new UploadUserImageProfileModelCommand(userProfile));

            return NewResult(Response);
        }

        [HttpGet("GetuserProfile")]
        public async Task<IActionResult> GetUserprofile(string userId)
        {
            var Response = await _Mediator.Send(new GetuserProfileQueryModel(userId));

            return NewResult(Response);
        }

        [HttpGet("GetImageProfile")]
        public async Task<IActionResult> GetImageProfile(string userId)
        {
            var Response = await _Mediator.Send(new GetUserImageProfileModelQuery(userId));

            return NewResult(Response);
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswrodDTO changePasswrodDTO)
        {
            var Response = await _Mediator.Send(new ChangePasswordModelCommand(changePasswrodDTO));

            return NewResult(Response);
        }
    }
}