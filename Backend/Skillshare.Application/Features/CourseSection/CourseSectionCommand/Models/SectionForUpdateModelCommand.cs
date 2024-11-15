using MediatR;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.SectionDTOs;

namespace Skillshare.Application.Features.CourseSection.CourseSectionCommand.Models
{
    public record SectionForUpdateModelCommand(SectionForUpdateDTO Section) : IRequest<ResponseModel<bool>>;
}