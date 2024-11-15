using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;

namespace Skillshare.Application.Features.UserProfile.UserProfileQuery.Models
{
    public record GetUserImageProfileModelQuery(string UserId) : IRequest<ResponseModel<string>>;
}