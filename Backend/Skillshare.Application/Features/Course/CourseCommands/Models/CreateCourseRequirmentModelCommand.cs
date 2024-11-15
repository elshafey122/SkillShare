using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.CourseDTOs;

namespace Skillshare.Application.Features.Course.CourseCommands.Models
{
    public record CreateCourseRequirmentModelCommand(CoursePrerequisiteDTO CoursePrerequisiteDTO) : IRequest<ResponseModel<bool>>;
}