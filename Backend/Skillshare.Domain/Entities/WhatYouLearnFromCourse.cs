﻿namespace Skillshare.Domain.Entities
{
    public class WhatYouLearnFromCourse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; }
    }
}