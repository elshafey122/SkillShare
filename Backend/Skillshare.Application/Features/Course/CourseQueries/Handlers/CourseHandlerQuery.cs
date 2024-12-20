﻿using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Wrappers;
using System;
using System.Linq.Expressions;
using Skillshare.Application.Features.Course.CourseQueries.Models;
using Skillshare.Application.ResponseHandler;
using Skillshare.Application.Shared;
using Skillshare.Contracts.DTOs.CourseDTOs;
using Skillshare.Contracts.Helpers;
using Skillshare.Contracts.ServicesContracts;
using X.PagedList;

namespace Skillshare.Application.Features.Course.CourseQueries.Handlers
{
    public class CourseHandlerQuery : ResponseHandlerModel,
        IRequestHandler<GetCourseDetailsModelQuery, ResponseModel<CourseDetailsForReturnDto>>,
        IRequestHandler<GetCourseLandingPageQuery, ResponseModel<CourseLandingPageForReturnDTO>>,
        IRequestHandler<GetCourseVideoPromotionpathQuery, string>,
        IRequestHandler<GetCoursesForInstructorModelQuery, ResponseModel<List<InstructorMinimalCourses>>>,
        IRequestHandler<GetCourseMessageModelQuery, ResponseModel<CourseMessageForReturnDTo>>,
        IRequestHandler<GetCoursePriceModelQuery, ResponseModel<CoursePriceForReturnDTO>>,
        IRequestHandler<GetCoursesPaginatedModelQuery, PaginatedResult<CourseForReturnDTO>>,
        IRequestHandler<GetCourseFullDetailsQuery, ResponseModel<Course_With_Instructor_Details>>,
        IRequestHandler<GetMyLearningModelQuery, ResponseModel<List<MyLearningCourseForReturnDto>>>,
        IRequestHandler<GetCourseContantModelQuery, ResponseModel<ContantStartDToForReturn>>,
        IRequestHandler<GetCourseVideoDataModelQuery, ResponseModel<CourseVideoData>>

    {
        private readonly ICourseService _CourseService;
        private readonly IWebHostEnvironment _WebHost;

        public CourseHandlerQuery(IStringLocalizer<Sharedresources> stringLocalizer, ICourseService courseService, IWebHostEnvironment webHost) : base(stringLocalizer)
        {
            _CourseService = courseService;
            _WebHost = webHost;
        }

        public async Task<ResponseModel<CourseDetailsForReturnDto>> Handle(GetCourseDetailsModelQuery request, CancellationToken cancellationToken)
        {
            var CourseDetails = await _CourseService.GetCourse(request.CourseId);
            if (CourseDetails == null)
            {
                return NotFound<CourseDetailsForReturnDto>();
            }

            return Success(CourseDetails);
        }

        public async Task<ResponseModel<CourseLandingPageForReturnDTO>> Handle(GetCourseLandingPageQuery request, CancellationToken cancellationToken)
        {
            var Course = await _CourseService.GetCourseLandingPage(request.CourseId);
            if (Course == null)
            {
                return NotFound<CourseLandingPageForReturnDTO>();
            }

            return Success(Course);
        }

        public async Task<string> Handle(GetCourseVideoPromotionpathQuery request, CancellationToken cancellationToken)
        {
            var VideoPath = await _CourseService.GetVideoPromotionCourse(request.Id);

            if (VideoPath is null)
            {
                return null;
            }

            return VideoPath;
        }

        public async Task<ResponseModel<List<InstructorMinimalCourses>>> Handle(GetCoursesForInstructorModelQuery request, CancellationToken cancellationToken)
        {
            var Response = await _CourseService.GetInstructorCourse(request.Id);

            return Success(Response);
        }

        public async Task<ResponseModel<CourseMessageForReturnDTo>> Handle(GetCourseMessageModelQuery request, CancellationToken cancellationToken)
        {
            var CourseForeturn = await _CourseService.GetCourseMessage(request.Id);
            if (CourseForeturn is null)
                return NotFound<CourseMessageForReturnDTo>();

            return Success(CourseForeturn);
        }

        public async Task<ResponseModel<CoursePriceForReturnDTO>> Handle(GetCoursePriceModelQuery request, CancellationToken cancellationToken)
        {
            var Course = await _CourseService.GetCoursePrice(request.CourseId);

            if (Course is null)
            {
                return NotFound<CoursePriceForReturnDTO>();
            }

            return Success(Course);
        }

        public async Task<PaginatedResult<CourseForReturnDTO>> Handle(GetCoursesPaginatedModelQuery request, CancellationToken cancellationToken)
        {
            var Querable = _CourseService.GetCoursesQuerable(request.query);

            //var PaginatedList = await Querable.ToPaginatedListAsync(request.query.pageNumber, request.query.pageSize);
            var PaginatedList = await Querable.ToPaginatedListAsync(request.query.pageNumber, request.query.pageSize);

            return PaginatedList;
        }

        public async Task<ResponseModel<Course_With_Instructor_Details>> Handle(GetCourseFullDetailsQuery request, CancellationToken cancellationToken)
        {
            var Result = await _CourseService.GetFullCourseDetails(request.courseId, request.userId);
            if (Result is null)
                return BadRequest<Course_With_Instructor_Details>();

            return Success(Result);
        }

        public async Task<ResponseModel<List<MyLearningCourseForReturnDto>>> Handle(GetMyLearningModelQuery request, CancellationToken cancellationToken)
        {
            var Result = await _CourseService.GetMyLearnings(request.userId);

            if (Result is null)
            {
                return BadRequest<List<MyLearningCourseForReturnDto>>();
            }
            return Success(Result);
        }

        public async Task<ResponseModel<ContantStartDToForReturn>> Handle(GetCourseContantModelQuery request, CancellationToken cancellationToken)
        {
            var Result = await _CourseService.LoadCourseContent(request.userId, request.courseId);

            if (Result is null)
                return BadRequest<ContantStartDToForReturn>();

            return Success(Result);
        }

        public async Task<ResponseModel<CourseVideoData>> Handle(GetCourseVideoDataModelQuery request, CancellationToken cancellationToken)
        {
            var Result = await _CourseService.CourseVideoData(request.userId, request.courseId, request.lectureId);

            if (Result is null)
                return BadRequest<CourseVideoData>();

            return Success(Result);
        }

        /*

            public async Task<IPagedList<CourseForReturnDTO>> Handle(GetCoursesPaginatedModelQuery request, CancellationToken cancellationToken)
        {
            var Querable = _CourseService.GetCoursesQuerable(request.query);

            //var PaginatedList = await Querable.ToPaginatedListAsync(request.query.pageNumber, request.query.pageSize);
            var PaginatedList = await Querable.ToPagedListAsync(request.query.pageNumber, request.query.pageSize);

            return PaginatedList;
        }
         */
    }
}