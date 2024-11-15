using MediatR;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Contracts.DTOs.CourseDTOs;
using Skillshare.Contracts.Helpers;
using X.PagedList;

namespace Skillshare.Application.Features.Course.CourseQueries.Models
{
    public record GetCoursesPaginatedModelQuery(PaginationQuery query) : IRequest<PaginatedResult<CourseForReturnDTO>>;
}