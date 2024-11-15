﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.Course;

namespace Skillshare.Application.Features.Course.CourseCommands.Models
{
    public record CreateBasicCourseModelCommand(CourseBasicDataDTO CourseDTO) : IRequest<ResponseModel<int>>;
}