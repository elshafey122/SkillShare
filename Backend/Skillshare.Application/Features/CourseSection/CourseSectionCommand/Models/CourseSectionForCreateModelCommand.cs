using MediatR;

using Skillshare.Application.ResponseHandler;

namespace Skillshare.Application.Features.CourseSection.CourseSectionCommand.Models
{
    public record CourseSectionForCreateModelCommand(int CourseId) : IRequest<ResponseModel<bool>>;
}