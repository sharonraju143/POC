using Microsoft.AspNetCore.Http;
using POC_WebAPI_DataAccessLayer.DTO;
using POC_WebAPI_DataAccessLayer.Models;

namespace POC_WebAPI_BusinessLogicLayer.Services
{
    public interface IFileService
    {
     Task PostFile(IFormFile fileData, FileType fileType);
       // Task<FileDetails> AddSingleFile(FileDto file, IFormFile fileData, FileType fileType);
    Task PostMultiFile(List<FileUploadModel> fileData);

     Task DownloadFileById(int fileName);
        
    }
}
