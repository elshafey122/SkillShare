﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillshare.Contracts.DTOs.CourseDTOs
{
    public class CourseLandingDTO
    {
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }

        public int? LangugeId { get; set; }

        public int? CategoryId { get; set; }

        [AllowedExtensionsAttributes(".jpg,.jpeg,.png")]
        public IFormFile? Image { get; set; }

        [AllowedExtensionsAttributes(".mp4,.webm")]
        public IFormFile? PromotionVideo { get; set; }
    }
}