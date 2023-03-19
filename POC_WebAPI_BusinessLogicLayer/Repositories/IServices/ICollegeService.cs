using POC_WebAPI_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_WebAPI_BusinessLogicLayer.Repositories.IServices
{
    public interface ICollegeService
    {

        Task<List<College>> GetAll();
        Task<College> GetCollegeById(int id);
        Task<College> GetCollegeByName(string name);
        Task<College> PostCollege(College college);
        Task<College> Update(College clg);
        //int Update(int clg);
        // void DeleteCollegeById(int id);

    }
}
