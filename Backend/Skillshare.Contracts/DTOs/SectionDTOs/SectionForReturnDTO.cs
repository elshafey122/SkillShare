using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Contracts.DTOs.LectureDTOs;

namespace Skillshare.Contracts.DTOs.SectionDTOs
{
    public class SectionForReturnDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string WhatStudentLearnFromthisSection { get; set; }
        public int CourseId { get; set; }
        public List<LectureForReturnDTO> Lectures { get; set; }
    }
}