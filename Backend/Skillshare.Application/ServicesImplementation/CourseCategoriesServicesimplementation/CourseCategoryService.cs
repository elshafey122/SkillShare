using AutoMapper;
using Skillshare.Contract.RepositoryContracts;
using Skillshare.Contracts.DTOs.CourseCategoryDTOs;
using Skillshare.Contracts.ServicesContracts;

namespace Skillshare.Application.ServicesImplementation.CourseCategoriesServicesimplementation
{
    internal class CourseCategoryService : ICourseCategoryService
    {
        private readonly ICourseCategoryRepository _CourseCategoryRepository;
        private readonly IMapper _Mapper;

        public CourseCategoryService(
            ICourseCategoryRepository courseCategoryRepository,
            IMapper mapper
            )
        {
            _CourseCategoryRepository = courseCategoryRepository;
            _Mapper = mapper;
        }

        public async Task<List<CourseCategoryDTO>> GetCourseCategories()
        {
            var Categories = await _CourseCategoryRepository.GetAllAsNoTracking(new[] { "Courses" } );

            var CategoriesForReturn = _Mapper.Map<List<CourseCategoryDTO>>(Categories);


            return CategoriesForReturn;
        }
    }
}