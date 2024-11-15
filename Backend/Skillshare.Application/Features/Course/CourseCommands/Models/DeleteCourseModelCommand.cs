using MediatR;
using Skillshare.Application.ResponseHandler;

namespace Skillshare.Application.Features.Course.CourseCommands.Models
{
    public record DeleteCourseModelCommand(int CourseId, string InstructorId) : IRequest<ResponseModel<bool>>;
}