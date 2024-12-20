﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillshare.Application.Features.Course.CourseQueries.Models
{
    public record GetCourseVideoPromotionpathQuery(int Id) : IRequest<string>;
}