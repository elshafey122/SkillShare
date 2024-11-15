using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.AuthDTOs;

namespace Skillshare.Application.Features.Authentication.AuthenticationCommands.Models
{
    public record RefreshTokenModelCommand(string userId) : IRequest<ResponseModel<AuthModel>>;
}