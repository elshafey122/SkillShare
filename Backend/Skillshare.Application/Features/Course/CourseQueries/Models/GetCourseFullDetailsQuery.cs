using MediatR;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.CourseDTOs;

namespace Skillshare.Application.Features.Course.CourseQueries.Models
{
    public record GetCourseFullDetailsQuery(int courseId,string userId) : IRequest<ResponseModel<Course_With_Instructor_Details>>;
}