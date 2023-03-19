using Microsoft.AspNetCore.Http;

namespace POC_WebAPI_DataAccessLayer.Models
{
    public class FileUploadModel
    {
        public IFormFile FileDetails { get; set; }
        public FileType FileType { get; set; }
    }
}
