﻿namespace Skillshare.Contracts.DTOs.Course
{
    public class CourseBasicDataDTO
    {
        public string Name { get; set; }
        public int Category { get; set; }
        public string InstructorId { get; set; }
        public decimal Price { get; set; } = 0;
    }
}