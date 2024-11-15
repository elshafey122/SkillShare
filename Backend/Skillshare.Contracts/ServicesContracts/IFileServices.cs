using Microsoft.AspNetCore.Http;
using Skillshare.Contracts.DTOs.FileDTO;

namespace Skillshare.Contracts.ServicesContracts
{
    public interface IFileServices
    {
        void DeleteFile(string Folderpath, string fileNamewithExtension);

        FileInformation SaveFile(IFormFile file, string FolderPath);
    }
}