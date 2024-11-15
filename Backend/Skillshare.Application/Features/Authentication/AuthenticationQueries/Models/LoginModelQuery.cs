using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.AuthDTOs;

namespace Skillshare.Application.Features.Authentication.AuthenticationQueries.Models
{
    public record LoginModelQuery(LogInDTo LogInDto) : IRequest<ResponseModel<AuthModel>>;
}