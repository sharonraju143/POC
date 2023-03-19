using POC_WebAPI_DataAccessLayer.DTO;
using POC_WebAPI_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_WebAPI_BusinessLogicLayer.Repositories.IServices
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAll();
        Task<Department> GetDepartmentsById(int id);
        Task<Department> GetDepartmentsByName(string name);
        // Task<Department> PostDepartment(Department department);
        Department PostDepartment(Dto department);
        Task<Department> UpdateDept(Department department);
        //  Task<string> DeleteDepartment(string name);
    }
}
