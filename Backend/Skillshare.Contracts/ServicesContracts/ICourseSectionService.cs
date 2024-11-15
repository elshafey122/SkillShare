using Skillshare.Contracts.DTOs.SectionDTOs;

namespace Skillshare.Contracts.ServicesContracts
{
    public interface ICourseSectionService
    {
        Task<bool> CreateSection(int CourseId);

        Task<bool> UpdateSection(SectionForUpdateDTO forUpdateDTO);

        Task<List<SectionForReturnDTO>> GetSections(int CourseId);

        Task<bool> DeleteSection(int SectionId);
    }
}