﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillshare.Contracts.DTOs.CourseDTOs
{
    public class CourseDetailsForReturnDto
    {
        public int CourseId { get; set; }
        public List<GenralModelCourseDetailsDTo> Requirments { get; set; }
        public List<GenralModelCourseDetailsDTo> WhateWillYouLearnFromCourse { get; set; }
        public List<GenralModelCourseDetailsDTo> WhoIsThisCourseFor { get; set; }
    }
}