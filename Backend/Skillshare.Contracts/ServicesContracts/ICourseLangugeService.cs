using Skillshare.Contracts.DTOs.CourseLangugeDTOs;

namespace Skillshare.Contracts.ServicesContracts
{
    public interface ICourseLangugeService
    {
        Task<List<CourselangugeDTO>> GetAlllanguge();
    }
}