using MediatR;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.CourseDTOs;

namespace Skillshare.Application.Features.Course.CourseCommands.Models
{
    public record UpdateCourseMessagesModelCommand(CourseMessageForUpdateDTO CourseMessage) : IRequest<ResponseModel<bool>>;
}