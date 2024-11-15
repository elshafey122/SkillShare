using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.UserProfileDTOs;

namespace Skillshare.Application.Features.UserProfile.UserProfileCommand.Models
{
    public record UploadUserImageProfileModelCommand(UploadUserprofileDTO Userprof) : IRequest<ResponseModel<bool>>;
}