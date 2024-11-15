using Skillshare.Contracts.DTOs.LectureDTOs;

namespace Skillshare.Contracts.ServicesContracts
{
    public interface ICourseLectureService
    {
        Task<bool> CreateLecture(int sectionId);

        Task<bool> UpdateLecture(LectureForUpdateDTO lectureForUpdateDTO);

        Task<bool> DeleteLecture(int lectureId);
    }
}