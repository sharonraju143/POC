using Microsoft.EntityFrameworkCore;
using POC_WebAPI_BusinessLogicLayer.Repositories.IServices;
using POC_WebAPI_DataAccessLayer.DBContext;
using POC_WebAPI_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_WebAPI_BusinessLogicLayer.Repositories.Services
{
    public class StudentService : IStudentService
    {
        private readonly PocDbContext _dbContext;

        public StudentService(PocDbContext dbContext) => this._dbContext = dbContext;


        public async Task<List<Student>> GetAll()
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                return await _dbContext.Students.FromSqlRaw<Student>("select * from [POC_Db].[A00].[Student]").ToListAsync();
            }
            catch (ApiException ex)
            {
                transaction.Commit();
                throw new ApiException(ex.Message);
            }
            finally { _dbContext.Dispose(); }

        }

        public async Task<Student> GetStudentById(int id)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var std = _dbContext.Students.Find(id);
                if (std == null)
                {
                    throw new ApiException("Not found");
                }
                return std;
            }
            catch (ApiException ex)
            {
                transaction.Commit();
                throw new ApiException(ex.Message);
            }
            finally { _dbContext.Dispose(); }

        }
        public async Task<Student> GetStudentByName(string name)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var student = _dbContext.Students.FirstOrDefault(c => c.SfirstName == name);
                if (student == null)
                {
                    throw new KeyNotFoundException("Student not found");

                }
                return student;

            }
            catch (Exception ex)
            {
                transaction.Commit();
                throw new ArgumentException(ex.Message);
            }
            finally { _dbContext.Dispose(); }
        }

        public async Task<Student> PostStudent(Student student)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                await _dbContext.Students.AddAsync(student);
                await _dbContext.SaveChangesAsync();
                return student;
            }
            catch (ApiException ex)
            {
                transaction.Commit();
                throw new ApiException(ex.Message);
            }
            finally { _dbContext.Dispose(); }
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _dbContext.Students.Update(student);
                _dbContext.SaveChangesAsync();
                return student;
            }
            catch (ApiException ex)
            {
                transaction.Commit();
                throw new ApiException(ex.Message);
            }
            finally { _dbContext.Dispose(); }

        }

        //public void DeleteStudent(int id)
        //{
        //    try
        //    {
        //        var getStudentDetailsById = GetStudentById(id);
        //        _dbContext.Students.Remove(getStudentDetailsById);
        //        _dbContext.SaveChangesAsync();

        //    }
        //    catch (Exception ex)
        //    {

        //        throw new ArgumentException(ex.Message);
        //    }
        //    finally { _dbContext.Dispose(); }


        //}
    }
}
