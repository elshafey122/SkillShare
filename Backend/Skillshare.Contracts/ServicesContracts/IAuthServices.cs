using Skillshare.Contracts.DTOs.AuthDTOs;
using Skillshare.Domain.Entities;

namespace Skillshare.Contracts.ServicesContracts
{
    public interface IAuthServices
    {
        Task<AuthModel> RegisterAsync(RegisterDto model);

        Task<AuthModel> LoginAsync(LogInDTo model);

        Task<string> GenerateToken(ApplicationUser user);

        Task<AuthModel> RefreshTokenAsync(string userId);
    }
}