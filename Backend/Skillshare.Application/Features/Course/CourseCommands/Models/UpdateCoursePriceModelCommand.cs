using MediatR;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.CourseDTOs;

namespace Skillshare.Application.Features.Course.CourseCommands.Models
{
    public record UpdateCoursePriceModelCommand(CoursePriceForUpdate Course) : IRequest<ResponseModel<bool>>;
}