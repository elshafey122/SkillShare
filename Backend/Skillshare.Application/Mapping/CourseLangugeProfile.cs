using AutoMapper;
using Skillshare.Contracts.DTOs.CourseLangugeDTOs;
using Skillshare.Domain.Entities;

namespace Skillshare.Application.Mapping
{
    public class CourseLangugeProfile : Profile
    {
        public CourseLangugeProfile()
        {
            MapFrom_CourseLanguge_CourseLangugeDTO();
        }

        public void MapFrom_CourseLanguge_CourseLangugeDTO()
        {
            CreateMap<CourseLanguge, CourselangugeDTO>()
                .ForMember(c=>c.CoursesCount,opt=>opt.MapFrom(c=>c.Courses.Count()))
                .ReverseMap();
        }
    }
}