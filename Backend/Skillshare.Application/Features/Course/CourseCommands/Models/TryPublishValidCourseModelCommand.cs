using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;

namespace Skillshare.Application.Features.Course.CourseCommands.Models
{
    public record TryPublishValidCourseModelCommand(string userId, int courseId) : IRequest<ResponseModel<bool>>;
}