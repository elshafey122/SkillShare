using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.ReviewDTOs;

namespace Skillshare.Application.Features.Review.ReviewCommand.Models
{
    public record ReviewForCreateModelCommand(ReviewForCreateDto reviewForCreateDto) : IRequest<ResponseModel<bool>>;
}