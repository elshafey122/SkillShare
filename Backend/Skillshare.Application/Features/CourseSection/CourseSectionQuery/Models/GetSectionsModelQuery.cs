using MediatR;

using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.SectionDTOs;

namespace Skillshare.Application.Features.CourseSection.CourseSectionQuery.Models
{
    public record GetSectionsModelQuery(int CourseId) : IRequest<ResponseModel<List<SectionForReturnDTO>>>;
}