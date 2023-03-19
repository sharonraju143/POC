
using POC_WebAPI_DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using POC_WebAPI_DataAccessLayer.DBContext;
using Microsoft.AspNetCore.Http;
using POC_WebAPI_DataAccessLayer.DTO;

namespace POC_WebAPI_BusinessLogicLayer.Services
{
    public class FileService:IFileService
    {
        private readonly PocDbContext dbContextClass;

        public FileService(PocDbContext dbContextClass)
        {
            this.dbContextClass = dbContextClass;
        }

        public async Task PostFile(IFormFile fileData, FileType fileType)
        {
            try
            {
                var fileDetails = new FileDetails()
                {

                    Id = 0,
                    
                    FileName = fileData.FileName,
                    FileType = (int)fileType,
                };

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);
                    fileDetails.FileData = stream.ToArray();
                }

                var result = dbContextClass.FileDetails.Add(fileDetails);
                await dbContextClass.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task<FileDetails> AddSingleFile(FileDto file, IFormFile fileData, FileType fileType, FileUploadModel fileUpload)
        //{
        //    try
        //    {
        //        var student = dbContextClass.Students.FirstOrDefault(x => x.SrollNumber == file.SRollNumber);
        //        FileDetails file1 = new FileDetails()
        //        {
        //            //Cid = college.Cid,
        //            //Dname = department.Dname,
        //            //Dblock = department.Dblock,
        //            //Dhod = department.Dhod,
        //            //ActiveFlag = department.ActiveFlag,
        //            //CreatedBy = "Teju",
        //            //CreatedDate = DateTime.Now,
        //            Sid = student.Sid,
        //            FileName = file.FileName,
        //            FileData = file.FileData,
        //            FileType=file.FileType

        //        };
        //        using (var stream = new MemoryStream())
        //        {
        //            fileData.CopyTo(stream);
        //            file1.FileData = stream.ToArray();
        //        }
        //        //dbContextClass.Departments.AddAsync(file1);
        //        //dbContextClass.SaveChanges();
        //        var result = dbContextClass.FileDetails.Add(file1);
        //               await dbContextClass.SaveChangesAsync();
        //        return file1;
        //    }
        //    catch (ApiException ex)
        //    {

        //        throw new ApiException(ex.Message);
        //    }
        //    return null;
        //}

        public async Task PostMultiFile(List<FileUploadModel> fileData)
        {
            try
            {
                foreach (FileUploadModel file in fileData)
                {
                    var fileDetails = new FileDetails()
                    {
                        Id = 0,
                        FileName = file.FileDetails.FileName,
                        FileType = (int)file.FileType
                    };

                    using (var stream = new MemoryStream())
                    {
                        file.FileDetails.CopyTo(stream);
                        fileDetails.FileData = stream.ToArray();
                    }

                    var result = dbContextClass.FileDetails.Add(fileDetails);
                }
                await dbContextClass.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DownloadFileById(int Id)
        {
            try
            {
                var file = dbContextClass.FileDetails.Where(x => x.Id == Id).FirstOrDefaultAsync();

                var content = new System.IO.MemoryStream(file.Result.FileData);
                var path = Path.Combine(
                   Directory.GetCurrentDirectory(), "FileDownloaded",
                   file.Result.FileName);

                await CopyStream(content, path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
