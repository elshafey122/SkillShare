﻿using Stripe;
using Skillshare.Contract.RepositoryContracts;
using Skillshare.Contracts.DTOs.LectureDTOs;
using Skillshare.Contracts.DTOs.SectionDTOs;
using Skillshare.Contracts.RepositoryContracts;
using Skillshare.Contracts.ServicesContracts;
using Skillshare.Domain.Entities;

namespace Skillshare.Application.ServicesImplementation.CourseSectionServiceImplementation
{
    public class CourseSectionService : ICourseSectionService
    {
        private readonly ICourseSectionRepository _CourseSectionRepository;
        private readonly ICourseRepository _CourseRepository;
        private readonly IFileServices _FileServices;

        public CourseSectionService(
            ICourseSectionRepository courseSectionRepository,
            ICourseRepository courseRepository,
            IFileServices fileServices
            )
        {
            _CourseSectionRepository = courseSectionRepository;
            _CourseRepository = courseRepository;
            _FileServices = fileServices;
        }

        public async Task<bool> CreateSection(int CourseId)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == CourseId);

            if (Course is null)
                return false;

            await UpdateCourseDate(Course);

            var Section = new Section()
            {
                CourseId = CourseId,
            };

            await _CourseSectionRepository.Add(Section);
            return await _CourseSectionRepository.SaveChanges();
        }

        public async Task<bool> DeleteSection(int SectionId)
        {
            var Section = await _CourseSectionRepository.GetFirstOrDefault(c => c.Id == SectionId, new[] { "Lecture" });

            if (Section is null) return false;

            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == Section.CourseId);
            await UpdateCourseDate(Course);

            Section.Lecture.ForEach((l) =>
            {
                if (l.VideoLectureUrl != null)
                {
                    _FileServices.DeleteFile("CoursesVideos", l.VideoLectureUrl);
                }
            });
            Section.Lecture = null;

            _CourseSectionRepository.Remove(Section);
            return await _CourseSectionRepository.SaveChanges();
        }

        public async Task<List<SectionForReturnDTO>> GetSections(int CourseId)
        {
            var Sections = await _CourseSectionRepository.GetAllAsNoTracking(c => c.CourseId == CourseId, new[] { "Lecture" });

            if (Sections is null)
            {
                return null;
            }

            var SectionsForReturnDto = new List<SectionForReturnDTO>();
            foreach (var Section in Sections)
            {
                SectionsForReturnDto.Add(new SectionForReturnDTO()
                {
                    CourseId = Section.CourseId,
                    Id = Section.Id,
                    Title = Section.Title,
                    WhatStudentLearnFromthisSection = Section.WhatStudentLearnFromthisSection,
                    Lectures = Section.Lecture.Select(c => new LectureForReturnDTO()
                    {
                        Id = c.Id,
                        SectionId = c.SectionId,
                        Title = c.Title,
                        VideoSectionUrl = c.VideoLectureUrl,
                        Description = c.Description,
                        Menutes = c.VideoMinuteLength
                    }).ToList(),
                });
            }
            return SectionsForReturnDto;
        }

        public async Task<bool> UpdateSection(SectionForUpdateDTO forUpdateDTO)
        {
            var Section = await _CourseSectionRepository.GetFirstOrDefault(c => c.Id == forUpdateDTO.SectionId && c.CourseId == forUpdateDTO.CourseId);

            if (Section is null)
                return false;

            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == Section.CourseId);
            await UpdateCourseDate(Course);

            Section.Title = forUpdateDTO.SectionTitle;
            Section.WhatStudentLearnFromthisSection = forUpdateDTO.SectionDescription;

            _CourseSectionRepository.Update(Section);
            return await _CourseSectionRepository.SaveChanges();
        }

        private async Task UpdateCourseDate(Course course)
        {
            course.lastUpdate = DateTime.UtcNow;
            _CourseRepository.Update(course);
            await _CourseRepository.SaveChanges();
        }
    }
}