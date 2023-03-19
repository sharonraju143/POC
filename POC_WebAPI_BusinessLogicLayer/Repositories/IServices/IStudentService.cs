using POC_WebAPI_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_WebAPI_BusinessLogicLayer.Repositories.IServices
{
    public interface IStudentService
    {
        Task<List<Student>> GetAll();
        Task<Student> GetStudentById(int id);
        Task<Student> GetStudentByName(string name);
        Task<Student> PostStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        // public void DeleteStudent(int id);
    }
}
