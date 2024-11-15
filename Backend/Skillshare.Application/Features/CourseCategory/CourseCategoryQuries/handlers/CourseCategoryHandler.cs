using MediatR;
using Microsoft.Extensions.Localization;
using Skillshare.Application.Features.CourseCategory.CourseCategoryQuries.Models;
using Skillshare.Application.ResponseHandler;
using Skillshare.Application.Shared;
using Skillshare.Contracts.DTOs.CourseCategoryDTOs;
using Skillshare.Contracts.DTOs.CourseLangugeDTOs;
using Skillshare.Contracts.ServicesContracts;

namespace Skillshare.Application.Features.CourseCategory.CourseCategoryQuries.handlers
{
    internal class CourseCategoryHandler : ResponseHandlerModel,
        IRequestHandler<GetCourseCategoriesModelQuery, ResponseModel<List<CourseCategoryDTO>>>,
        IRequestHandler<GetLangugesModelQuery, ResponseModel<List<CourselangugeDTO>>>
    {
        private readonly ICourseCategoryService _CourseCategoryService;
        private readonly ICourseLangugeService _CourseLangugeService;

        public CourseCategoryHandler(IStringLocalizer<Sharedresources> stringLocalizer,
            ICourseCategoryService courseCategoryService, ICourseLangugeService courseLangugeService) : base(stringLocalizer)
        {
            _CourseCategoryService = courseCategoryService;
            _CourseLangugeService = courseLangugeService;
        }

        public async Task<ResponseModel<List<CourseCategoryDTO>>> Handle(GetCourseCategoriesModelQuery request, CancellationToken cancellationToken)
        {
            var Categories = await _CourseCategoryService.GetCourseCategories();

            return Success(Categories);
        }

        public async Task<ResponseModel<List<CourselangugeDTO>>> Handle(GetLangugesModelQuery request, CancellationToken cancellationToken)
        {
            var courselangugeDTOs = await _CourseLangugeService.GetAlllanguge();

            return Success(courselangugeDTOs);
        }
    }
}