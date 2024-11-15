using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.Features.CourseSection.CourseSectionQuery.Models;
using Skillshare.Application.ResponseHandler;
using Skillshare.Application.Shared;
using Skillshare.Contracts.DTOs.SectionDTOs;
using Skillshare.Contracts.ServicesContracts;

namespace Skillshare.Application.Features.CourseSection.CourseSectionQuery.Handlers
{
    internal class SectionQueryHandler : ResponseHandlerModel,
        IRequestHandler<GetSectionsModelQuery, ResponseModel<List<SectionForReturnDTO>>>
    {
        private readonly ICourseSectionService _CourseSectionService;

        public SectionQueryHandler(IStringLocalizer<Sharedresources> stringLocalizer, ICourseSectionService courseSectionService) : base(stringLocalizer)
        {
            _CourseSectionService = courseSectionService;
        }

        public async Task<ResponseModel<List<SectionForReturnDTO>>> Handle(GetSectionsModelQuery request, CancellationToken cancellationToken)
        {
            var Sections = await _CourseSectionService.GetSections(request.CourseId);
            if (Sections is null)
            {
                return BadRequest<List<SectionForReturnDTO>>();
            }
            return Success(Sections);
        }
    }
}