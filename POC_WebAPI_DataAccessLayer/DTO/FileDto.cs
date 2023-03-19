using Microsoft.AspNetCore.Http;
using POC_WebAPI_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_WebAPI_DataAccessLayer.DTO
{
    public class FileDto
    {
        public string SRollNumber { get; set; }

        public string FileName { get; set; } = null!;

        public byte[] FileData { get; set; } = null!;

        public int FileType { get; set; }

        public IFormFile FileDetails { get; set; }
       
    }
}
