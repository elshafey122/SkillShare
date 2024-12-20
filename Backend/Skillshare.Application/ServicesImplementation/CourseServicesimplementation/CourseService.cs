﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Stripe;
using System.Linq.Expressions;
using Skillshare.Contract.RepositoryContracts;
using Skillshare.Contracts.DTOs.Course;
using Skillshare.Contracts.DTOs.CourseDTOs;
using Skillshare.Contracts.DTOs.SectionDTOs;
using Skillshare.Contracts.Helpers;
using Skillshare.Contracts.RepositoryContracts;
using Skillshare.Contracts.ServicesContracts;
using Skillshare.Domain.Entities;
using static System.Collections.Specialized.BitVector32;

namespace Skillshare.Application.ServicesImplementation.CourseServicesimplementation
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _CourseRepository;
        private readonly ICourseRequimentRepository _CourseRequimentRepository;
        private readonly IWhatYouLearnFromCourseRepository _WhatYouLearnFromCourseRepository;
        private readonly IWhoIsThisCourseForRepository _WhoIsThisCourseForRepository;
        private readonly IUserProfileRepository _UserProfileRepository;
        private readonly ICourseSectionRepository _CourseSectionRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IWebHostEnvironment _Host;
        private readonly IFileServices _FileServices;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly ICartItemRepository _CartItemRepository;
        private readonly IMapper _Mapper;
        private readonly ICourseLectureRepository _CourseLectureRepository;
        private readonly IReviewRepository _ReviewRepository;

        public CourseService(
            ICourseRepository courseRepository, ICourseRequimentRepository courseRequimentRepository,
            IWhatYouLearnFromCourseRepository whatYouLearnFromCourseRepository,
            IWhoIsThisCourseForRepository whoIsThisCourseForRepository,
            IUserProfileRepository userProfileRepository,
            ICourseSectionRepository courseSectionRepository,
            IUserRepository userRepository,
            IWebHostEnvironment host,
            IFileServices fileServices,
            IHttpContextAccessor httpContextAccessor,
            ICartItemRepository cartItemRepository,
            IMapper mapper,
            ICourseLectureRepository courseLectureRepository,
            IReviewRepository reviewRepository
            )
        {
            _CourseRepository = courseRepository;
            _CourseRequimentRepository = courseRequimentRepository;
            _WhatYouLearnFromCourseRepository = whatYouLearnFromCourseRepository;
            _WhoIsThisCourseForRepository = whoIsThisCourseForRepository;
            _UserProfileRepository = userProfileRepository;
            _CourseSectionRepository = courseSectionRepository;
            _UserRepository = userRepository;
            _Host = host;
            _FileServices = fileServices;
            _HttpContextAccessor = httpContextAccessor;
            _CartItemRepository = cartItemRepository;
            _Mapper = mapper;
            _CourseLectureRepository = courseLectureRepository;
            _ReviewRepository = reviewRepository;
        }

        public async Task<int> CreateBasicCourse(CourseBasicDataDTO courseBasic)
        {
            if (courseBasic is null)
            {
                return -1;
            }

            var user = await _UserRepository.GetFirstOrDefault(c => c.Id == courseBasic.InstructorId, new[] { "coursesInrollments" });
            if (user is null)
            {
                return -1;
            }

            var Course = new Course();
            _Mapper.Map(courseBasic, Course);
            await _CourseRepository.Add(Course);
            await _CourseRepository.SaveChanges();
            await UpdateCourseDate(Course);
            user.coursesInrollments.Add(Course);
            _UserRepository.Update(user);
            await _UserRepository.SaveChanges();

            return Course.Id;
        }

        public async Task CreateRequimentCourse(CoursePrerequisiteDTO prerequisiteDTO)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == prerequisiteDTO.Id, new[] { "Requirments", "whoIsthisCoursefors", "whatYouLearnFromCourse" });

            if (Course is null)
            {
                return;
            }

            var Req = await _CourseRequimentRepository.GetAllAsTracking(c => c.CourseId == Course.Id);
            _CourseRequimentRepository.RemoveRange(Req);

            var what = await _WhatYouLearnFromCourseRepository.GetAllAsTracking(c => c.CourseId == Course.Id);
            _WhatYouLearnFromCourseRepository.RemoveRange(what);

            var who = await _WhoIsThisCourseForRepository.GetAllAsTracking(c => c.CourseId == Course.Id);
            _WhoIsThisCourseForRepository.RemoveRange(who);
            await _WhoIsThisCourseForRepository.SaveChanges();

            var Requirment = new List<CourseRequirment>();

            _Mapper.Map(prerequisiteDTO, Requirment);

            Course.Requirments.AddRange(Requirment);

            var WhoisTHisCoureFor = new List<WhoIsthisCoursefor>();

            _Mapper.Map(prerequisiteDTO, WhoisTHisCoureFor);

            Course.whoIsthisCoursefors.AddRange(WhoisTHisCoureFor);

            var whatYouLearnFromCourse = new List<WhatYouLearnFromCourse>();

            _Mapper.Map(prerequisiteDTO, whatYouLearnFromCourse);

            Course.whatYouLearnFromCourse.AddRange(whatYouLearnFromCourse);

            await UpdateCourseDate(Course);

            _CourseRepository.Update(Course);
            await _CourseRepository.SaveChanges();
        }

        public async Task<CourseDetailsForReturnDto> GetCourse(int Id)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == Id, new[] { "Requirments", "whoIsthisCoursefors", "whatYouLearnFromCourse" });
            if (Course is null)
                return null;

            var CourseForReturn = _Mapper.Map<CourseDetailsForReturnDto>(Course);

            return CourseForReturn;
        }

        public async Task<CourseLandingPageForReturnDTO> GetCourseLandingPage(int Id)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == Id);
            if (Course is null)
            {
                return null;
            }
            var CourseLandingPageForReturnDTO = new CourseLandingPageForReturnDTO()
            {
                CourseId = Id,
                CategoryId = Course.CategoryId,
                CourseImage = Course.Image == null ? null : Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "CourseImages", Course.Image),
                Description = Course?.Description,
                LangugeId = Course.langugeId.HasValue ? Course.langugeId.Value : default,
                SubTitle = Course?.SubTitle,
                Title = Course?.Title,
            };
            return CourseLandingPageForReturnDTO;
        }

        public async Task<CourseMessageForReturnDTo> GetCourseMessage(int Id)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == Id);

            if (Course is null)
            {
                return null;
            }

            var CourseForReturn = _Mapper.Map<CourseMessageForReturnDTo>(Course);
            return CourseForReturn;
        }

        public async Task<CoursePriceForReturnDTO> GetCoursePrice(int Id)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == Id);

            if (Course is null)
                return null;

            var CoursePriceForReturn = _Mapper.Map<CoursePriceForReturnDTO>(Course);

            return CoursePriceForReturn;
        }

        public async Task<bool> DeleteCourse(int CourseId, string InstructorId)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == CourseId && c.InstructorId == InstructorId);

            if (Course is null)
                return false;

            var AllSections = await _CourseSectionRepository.GetAllAsNoTracking(c => c.CourseId == CourseId, new[] { "Lecture" });
            foreach (var Section in AllSections)
            {
                Section.Lecture.ForEach((l) =>
                {
                    if (l.VideoLectureUrl != null)
                    {
                        _FileServices.DeleteFile("CoursesVideos", l.VideoLectureUrl);
                    }
                });
            }
            _CourseSectionRepository.RemoveRange(AllSections);

            var AllItems = await _CartItemRepository.GetAllAsNoTracking(c => c.courseId == Course.Id);
            _CartItemRepository.RemoveRange(AllItems);
            await _CartItemRepository.SaveChanges();
            _CourseRepository.Remove(Course);
            var isDeleted = await _CourseRepository.SaveChanges();

            if (isDeleted && Course.Image != null)
            {
                _FileServices.DeleteFile("CourseImages", Course.Image);
            }

            if (isDeleted && Course.CoursePromotionalVideo != null)
            {
                _FileServices.DeleteFile("PromotionalVideo", Course.CoursePromotionalVideo);
            }

            return isDeleted;
        }

        public async Task<List<InstructorMinimalCourses>> GetInstructorCourse(string InstructorId)
        {
            var Courses = await _CourseRepository.GetAllAsNoTracking(c => c.InstructorId == InstructorId, new[] { "Requirments", "whoIsthisCoursefors", "whatYouLearnFromCourse", "Instructor", "category", "languge" });

            var CourseToReturn = Courses.Select(c => new InstructorMinimalCourses()
            {
                Id = c.Id,
                Name = c.Title,
                SubTitle = c.SubTitle,
                Image = c.Image == null ? null : Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "CourseImages", c.Image),
                Progress = (int)(100 * (c.CountofNotNullValues() / 20))
            }).ToList();

            return CourseToReturn;
        }

        public async Task<string> GetVideoPromotionCourse(int Id)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == Id);

            if (Course is null || Course.CoursePromotionalVideo is null)
            {
                return null;
            }
            //var CourseVideoPath = Course.CoursePromotionalVideo;
            var CourseVideoPath = Path.Combine(_Host.WebRootPath, "PromotionalVideo", Course.CoursePromotionalVideo);
            return CourseVideoPath;
        }

        public async Task<bool> SaveCourseLanding(CourseLandingDTO courseLanding)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == courseLanding.CourseId);

            if (Course is null) return false;

            if (courseLanding.Image is not null)
            {
                if (Course.Image != null)
                {
                    _FileServices.DeleteFile("CourseImages", Course.Image);
                }
                Course.Image = _FileServices.SaveFile(courseLanding.Image, Path.Combine(_Host.WebRootPath, "CourseImages")).Path;
            }

            if (courseLanding.PromotionVideo is not null)
            {
                if (Course.CoursePromotionalVideo != null)
                {
                    _FileServices.DeleteFile("PromotionalVideo", Course.CoursePromotionalVideo);
                }
                Course.CoursePromotionalVideo = _FileServices.SaveFile(courseLanding.PromotionVideo, Path.Combine(_Host.WebRootPath, "PromotionalVideo")).Path;
            }
            await UpdateCourseDate(Course);

            Course.Title = courseLanding.Title;

            Course.SubTitle = courseLanding.SubTitle;
            Course.Description = courseLanding.Description;
            Course.langugeId = courseLanding.LangugeId;
            Course.CategoryId = courseLanding.CategoryId.Value;
            _CourseRepository.Update(Course);
            return await _CourseRepository.SaveChanges();
        }

        public async Task<bool> UpdateCourseMessage(CourseMessageForUpdateDTO courseMessageForUpdateDTO)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == courseMessageForUpdateDTO.CourseId && c.InstructorId == courseMessageForUpdateDTO.InstructorId);

            if (Course is null)
            {
                return false;
            }
            await UpdateCourseDate(Course);
            _Mapper.Map(courseMessageForUpdateDTO, Course);

            _CourseRepository.Update(Course);
            return await _CourseRepository.SaveChanges();
        }

        public async Task<bool> UpdateCourseprice(CoursePriceForUpdate coursePriceForUpdate)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == coursePriceForUpdate.CourseId && c.InstructorId == coursePriceForUpdate.InstructorId);

            if (Course is null)
            {
                return false;
            }
            await UpdateCourseDate(Course);

            _Mapper.Map(coursePriceForUpdate, Course);

            _CourseRepository.Update(Course);
            return await _CourseRepository.SaveChanges();
        }

        public IQueryable<CourseForReturnDTO> GetCoursesQuerable(PaginationQuery paginationQuery)
        {
            Expression<Func<Course, CourseForReturnDTO>> expression = e => new CourseForReturnDTO(
            e.Id,
            e.Title,
            e.SubTitle,
            e.Price.HasValue ? e.Price.Value : 0,
            e.Instructor.Name,
            e.InstructorId,
            Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "CourseImages", e.Image ?? ""),
            e.Sections.SelectMany(s => s.Lecture).Count(),
             e.Sections.SelectMany(s => s.Lecture).Sum(l => l.VideoMinuteLength.HasValue ? l.VideoMinuteLength.Value : 0)
            );

            var Query = _CourseRepository.GetAllQuerableAsNoTracking(new[] { "Instructor" }).Where(c => c.isPublished).AsQueryable();

            if (paginationQuery.search != null)
            {
                Query = Query.Where(c => c.Title.ToLower().Contains(paginationQuery.search.ToLower()));
            }
            if (paginationQuery.langugeId != null)
            {
                Query = Query.Where(c => c.langugeId == paginationQuery.langugeId);
            }
            if (paginationQuery.categoryId != null)
            {
                Query = Query.Where(c => c.CategoryId == paginationQuery.categoryId);
            }

            if (paginationQuery.minPrice != null && paginationQuery.maxPrice != null)
            {
                Query = Query.Where(c => c.Price >= paginationQuery.minPrice && c.Price <= paginationQuery.maxPrice);
            }

            if (paginationQuery.minHours != null && paginationQuery.maxHours != null)
            {
                Query = Query.Where(c => c.Sections.SelectMany(
                    s => s.Lecture).Sum(l => l.VideoMinuteLength.HasValue ? l.VideoMinuteLength.Value : 0) >= paginationQuery.minHours
                    && c.Sections.SelectMany(s => s.Lecture).Sum(l => l.VideoMinuteLength.HasValue ? l.VideoMinuteLength.Value : 0) <= paginationQuery.maxHours);
            }

            //e.Sections.SelectMany(s => s.Lecture).Sum(l => l.VideoMinuteLength.HasValue ? l.VideoMinuteLength.Value : 0)

            return Query.Select(expression);
        }

        public async Task<Course_With_Instructor_Details> GetFullCourseDetails(int CourseId, string userId)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == CourseId, new[] { "languge", "category", "Requirments", "whoIsthisCoursefors", "whatYouLearnFromCourse", "Students", });
            if (Course is null)
            {
                return null;
            }

            var UdemyAccount = await _UserProfileRepository.GetFirstOrDefault(c => c.applicationUserId == Course.InstructorId);
            var Instructor = await _UserRepository.GetFirstOrDefault(c => c.Id == Course.InstructorId, new[] { "CoursesICreated" });

            var CoursesThisInstractorCreate = await _CourseRepository.GetAllAsNoTracking(c => c.InstructorId == Instructor.Id, new[] { "Students", "reviews" });

            var totalReviewsCount = CoursesThisInstractorCreate
                .SelectMany(course => course.reviews)
                .Count();

            var Sections = await _CourseSectionRepository.GetAllAsNoTracking(c => c.CourseId == CourseId, new[] { "Lecture" });
            var isInCart = await _CartItemRepository.GetFirstOrDefault(c => c.courseId == CourseId && c.ApplicationUserId == userId);

            var user = await _UserRepository.GetFirstOrDefault(c => c.Id == userId, new[] { "coursesInrollments" });
            var Result = new Course_With_Instructor_Details()
            {
                courseId = CourseId,
                courseImage = Course.Image == null ? null : Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "CourseImages", Course.Image),
                courseRating = GetReview(_ReviewRepository.GetAllAsNoTracking(r => r.courseId == Course.Id).Result.ToList()),
                courseSubTitle = Course.SubTitle,
                courseTitle = Course.Title,
                description = Course.Description,
                studentInThisCourseCount = Course.Students.Count(),
                languge = Course.languge.Name,
                lastUpdated = Course.lastUpdate,
                coursePrice = Course.Price.HasValue ? Course.Price.Value : 0,
                isInCart = isInCart == null ? false : true,
                instructoreDetaisl = new InstructoreDetaisl()
                {
                    instructorId = Instructor.Id,
                    biography = UdemyAccount.Biography,
                    totalReviews = totalReviewsCount,
                    headline = UdemyAccount.Headline,
                    courseCount = Instructor.CoursesICreated.Count(),
                    name = Instructor.Name,
                    instructorImage = UdemyAccount.ImageUrl == null ? null : Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "UsersImagesProfile", UdemyAccount.ImageUrl),
                },
                isInMylearning = user.coursesInrollments.Any(c => c.Id == CourseId),
                courrseRequirments = Course.Requirments.Select(c => c.Text).ToList(),
                whateyoulearn = Course.whatYouLearnFromCourse.Select(c => c.Text).ToList(),
                whoIsCourseFor = Course.whoIsthisCoursefors.Select(c => c.Text).ToList(),
                duration = Sections.Sum(s => s.Lecture.Sum(l => l.VideoMinuteLength.HasValue ? l.VideoMinuteLength.Value : 0)),
                isPaidforCurrentUser = true,
                contentSections = Sections.Select(c => new courseContentSectionDto()
                {
                    sectionId = c.Id,
                    lectureCount = c.Lecture.Count(),
                    sectionTitle = c.Title,
                    totalMinutes = c.Lecture.Sum(c => c.VideoMinuteLength.HasValue ? c.VideoMinuteLength.Value : 0),
                    lectureContent = c.Lecture.Select(c => new courseLectureContentDto()
                    {
                        lectureId = c.Id,
                        Lecturetitle = c.Title,
                        totalMinutes = c.VideoMinuteLength.HasValue ? c.VideoMinuteLength.Value : 0
                    }).ToList()
                }).ToList()
            };

            return Result;
        }

        public async Task<bool> InrollFreeCourse(int courseId, string userId)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == courseId);
            if (Course is null)
                return false;

            var user = await _UserRepository.GetFirstOrDefault(c => c.Id == userId, new[] { "coursesInrollments" });
            if (user is null)
                return false;

            if (user.coursesInrollments.Any(c => c.Id == courseId))
                return false;

            user.coursesInrollments.Add(Course);
            _UserRepository.Update(user);

            return await _UserRepository.SaveChanges();
        }

        private async Task UpdateCourseDate(Course course)
        {
            course.lastUpdate = DateTime.UtcNow;
            _CourseRepository.Update(course);
            await _CourseRepository.SaveChanges();
        }

        public async Task<List<MyLearningCourseForReturnDto>> GetMyLearnings(string userId)
        {
            var user = await _UserRepository.GetFirstOrDefault(c => c.Id == userId, new[] { "coursesInrollments" });

            if (user is null)
            {
                return null;
            }
            if (user.coursesInrollments is null || user.coursesInrollments.Count == 0)
            {
                return Enumerable.Empty<MyLearningCourseForReturnDto>().ToList();
            }
            var Result = user.coursesInrollments.Where(c => c.isPublished).Select(c => new MyLearningCourseForReturnDto()
            {
                courseId = c.Id,
                instructorId = c.InstructorId,
                courseName = c.Title,
                rating = GetReview(_ReviewRepository.GetAllAsNoTracking(r => r.courseId == c.Id).Result.ToList()),
                courseImage = c.Image == null ? null : Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "CourseImages", c.Image),
                instructorName = c.Instructor.Name
            }).ToList();

            return Result;
        }

        private int GetReview(List<Domain.Entities.Review> reviews)
        {
            var AllReviewsSum = reviews.Sum(c => c.stars);
            var Count = reviews.Count();

            if (AllReviewsSum == 0 || Count == 0)
                return 0;

            return AllReviewsSum / Count;

        }

        public async Task<ContantStartDToForReturn> LoadCourseContent(string userId, int courseId)
        {
            var user = await _UserRepository.GetFirstOrDefault(c => c.Id == userId, new[] { "coursesInrollments" });

            if (user is null)
                return null;

            var isExistinInrolments = user.coursesInrollments.Any(c => c.Id == courseId);

            if (!isExistinInrolments)
                return null;

            var Sections = await _CourseSectionRepository.GetAllAsNoTracking(c => c.CourseId == courseId, new[] { "Lecture" });
            var Reviews = await _ReviewRepository.GetAllAsNoTracking(c => c.courseId == courseId, new[] { "user" });


            var course = await _CourseRepository.GetFirstOrDefault(c => c.Id == courseId, new[] { "languge", "Students", "Instructor", "whatYouLearnFromCourse" });
            var Profile = await _UserProfileRepository.GetFirstOrDefault(c => c.applicationUserId == course.InstructorId);
            var CourseSection = Sections.Select(c => new courseContentSectionDto()
            {
                sectionId = c.Id,
                lectureCount = c.Lecture.Count(),
                sectionTitle = c.Title,

                totalMinutes = c.Lecture.Sum(c => c.VideoMinuteLength.HasValue ? c.VideoMinuteLength.Value : 0),
                lectureContent = c.Lecture.Select(c => new courseLectureContentDto()
                {
                    lectureId = c.Id,
                    Lecturetitle = c.Title,
                    totalMinutes = c.VideoMinuteLength.HasValue ? c.VideoMinuteLength.Value : 0
                }).ToList(),
            }).ToList();

            var Result = new ContantStartDToForReturn()
            {
                totalMinutes = Sections.SelectMany(section => section.Lecture).Sum(lecture => lecture.VideoMinuteLength.HasValue ? lecture.VideoMinuteLength.Value : 0),
                courseContentSection = CourseSection,
                lectureCount = Sections.SelectMany(s => s.Lecture).Count(),
                rating = GetReview(_ReviewRepository.GetAllAsNoTracking(r => r.courseId == courseId).Result.ToList()),
                courseReviews = Reviews.Select(r => new CourseReviewDtoForReturn()
                {
                    stars = r.stars,
                    text = r.Text,
                    userImagePath = GetUserImage(r.userId).userProfileImage,
                    userName = GetUserImage(r.userId).userName
                }).ToList(),
                aboutCourseDto = new AboutCourseDto()
                {
                    discription = course.Description,
                    languge = course.languge.Name,
                    studentsCount = course.Students.Count(),
                    whatWillYouLearn = course.whatYouLearnFromCourse.Select(c => c.Text).ToList(),
                    headline = Profile.Headline,
                    instructoreDetails = new InstructoreDetaisl()
                    {
                        biography = Profile.Biography,
                        courseCount = course.Students.Count(),
                        instructorId = course.InstructorId,
                        name = course.Instructor.Name,
                        instructorImage = Profile.ImageUrl == null ? null : Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "UsersImagesProfile", Profile.ImageUrl),
                        socialAccount = new SocialAccount()
                        {
                            userId = Profile.applicationUserId,
                            facebook = Profile.FacebookUrl,
                            linkedIn = Profile.LinkedInUrl,
                            twiter = Profile.TwitterUrl,
                            youtube = Profile.LinkedInUrl
                        }
                    }
                }
            };
            return Result;
        }

        private MyDataResult GetUserImage(string userId)
        {
            var userprofile = _UserProfileRepository.GetFirstOrDefault(c => c.applicationUserId == userId).Result;
            if (userprofile is null)
                return null;

            return new MyDataResult()
            {
                userProfileImage = userprofile.ImageUrl == null ? null : Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "UsersImagesProfile", userprofile.ImageUrl),

                userName = userprofile.FullName
            };
        }

        public async Task<CourseVideoData> CourseVideoData(string userId, int courseId, int lectureId)
        {
            var user = await _UserRepository.GetFirstOrDefault(c => c.Id == userId, new[] { "coursesInrollments" });

            if (user is null)
                return null;

            var isExistinInrolments = user.coursesInrollments.Any(c => c.Id == courseId);

            if (!isExistinInrolments)
                return null;

            var Lecture = await _CourseLectureRepository.GetFirstOrDefault(c => c.Id == lectureId);

            if (Lecture is null)
                return null;

            var path = Path.Combine(_Host.WebRootPath, "CoursesVideos", Lecture.VideoLectureUrl ?? "");

            if (!Path.Exists(path))
                return null;

            return new CourseVideoData()
            {
                extension = Path.GetExtension(path),
                path = path
            };
        }

        public async Task<bool> TryPublishCourse(string userId, int courseId)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == courseId && c.InstructorId == userId, new[] { "Requirments", "whoIsthisCoursefors", "whatYouLearnFromCourse" });

            if (Course is null)
                return false;

            if (Course.isPublished)
                return true;

            if (!Course.isValidCourseForPublish())
                return false;

            if (!ChecklistOfStringsisNotNull(Course.Requirments.Select(c => c.Text).ToList()))
                return false;

            if (!ChecklistOfStringsisNotNull(Course.whatYouLearnFromCourse.Select(c => c.Text).ToList()))
                return false;

            if (!ChecklistOfStringsisNotNull(Course.whoIsthisCoursefors.Select(c => c.Text).ToList()))
                return false;

            var Instructor = await _UserProfileRepository.GetFirstOrDefault(c => c.applicationUserId == userId);

            if (!Instructor.isValidProfile())
                return false;

            var AllSection = await _CourseSectionRepository.GetAllAsNoTracking(c => c.CourseId == courseId, new[] { "Lecture" });

            if (AllSection.Any(c => c.Title is null))
                return false;

            if (AllSection.SelectMany(s => s.Lecture).Count() < 5)
            {
                return false;
            }
            foreach (var Section in AllSection)
            {
                foreach (var Lecture in Section.Lecture)
                {
                    if (Lecture.VideoLectureUrl is null)
                        return false;
                }
            }

            if (AllSection.SelectMany(section => section.Lecture).Sum(lecture => lecture.VideoMinuteLength.HasValue ? lecture.VideoMinuteLength.Value : 0) < 30)
            {
                return false;
            }

            Course.isPublished = true;
            _CourseRepository.Update(Course);

            return await _CourseRepository.SaveChanges();
        }

        private bool ChecklistOfStringsisNotNull(List<string> texts)
        {
            if (texts.Count() == 0)
                return false;

            foreach (var text in texts)
            {
                if (string.IsNullOrWhiteSpace(text))
                    return false;
            }

            return true;
        }
    }
}

public class MyDataResult
{
    public string userName { get; set; }
    public string userProfileImage { get; set; }
}





