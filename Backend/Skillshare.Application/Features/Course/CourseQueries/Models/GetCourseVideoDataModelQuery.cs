using MediatR;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.CourseDTOs;

namespace Skillshare.Application.Features.Course.CourseQueries.Models
{
    public record GetCourseVideoDataModelQuery(string userId, int courseId, int lectureId) : IRequest<ResponseModel<CourseVideoData>>;
}