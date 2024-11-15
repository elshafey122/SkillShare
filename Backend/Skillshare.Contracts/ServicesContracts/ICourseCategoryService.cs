using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Contracts.DTOs.CourseCategoryDTOs;

namespace Skillshare.Contracts.ServicesContracts
{
    public interface ICourseCategoryService
    {
        Task<List<CourseCategoryDTO>> GetCourseCategories();
    }
}