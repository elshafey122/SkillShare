﻿using Microsoft.AspNetCore.Http;
using Skillshare.Contracts.DTOs.Course;
using Skillshare.Contracts.DTOs.CourseDTOs;
using Skillshare.Contracts.DTOs.SectionDTOs;
using Skillshare.Contracts.Helpers;
using Skillshare.Domain.Entities;

namespace Skillshare.Contracts.ServicesContracts
{
    public interface ICourseService
    {
        Task<int> CreateBasicCourse(CourseBasicDataDTO courseBasic);

        Task CreateRequimentCourse(CoursePrerequisiteDTO prerequisiteDTO);

        Task<CourseDetailsForReturnDto> GetCourse(int Id);

        Task<bool> SaveCourseLanding(CourseLandingDTO courseLanding);

        Task<CourseLandingPageForReturnDTO> GetCourseLandingPage(int Id);

        Task<string> GetVideoPromotionCourse(int Id);

        Task<List<InstructorMinimalCourses>> GetInstructorCourse(string InstructorId);

        Task<bool> UpdateCourseMessage(CourseMessageForUpdateDTO courseMessageForUpdateDTO);

        Task<CourseMessageForReturnDTo> GetCourseMessage(int Id);

        Task<bool> UpdateCourseprice(CoursePriceForUpdate coursePriceForUpdate);

        Task<CoursePriceForReturnDTO> GetCoursePrice(int Id);

        public IQueryable<CourseForReturnDTO> GetCoursesQuerable(PaginationQuery paginationQuery);

        Task<bool> DeleteCourse(int CourseId, string InstructorId);

        Task<Course_With_Instructor_Details> GetFullCourseDetails(int CourseId, string userId);

        Task<bool> InrollFreeCourse(int courseId, string userId);

        Task<List<MyLearningCourseForReturnDto>> GetMyLearnings(string userId);

        Task<ContantStartDToForReturn> LoadCourseContent(string userId, int courseId);

        Task<CourseVideoData> CourseVideoData(string userId, int courseId, int lectureId);


        Task<bool> TryPublishCourse(string userId,int courseId);
    }
}