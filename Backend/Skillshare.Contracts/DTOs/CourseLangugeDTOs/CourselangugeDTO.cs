using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillshare.Contracts.DTOs.CourseLangugeDTOs
{
    public class CourselangugeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CoursesCount { get; set; }
    }
}