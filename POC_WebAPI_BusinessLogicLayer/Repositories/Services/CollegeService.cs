using POC_WebAPI_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POC_WebAPI_DataAccessLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using POC_WebAPI_BusinessLogicLayer.Repositories.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.InteropServices;
using static Azure.Core.HttpHeader;
using Microsoft.Extensions.Logging;

namespace POC_WebAPI_BusinessLogicLayer.Repositories.Services
{
    public class CollegeService : ICollegeService
    {
        private readonly PocDbContext _dbContext;
        //private readonly ILogger _logger;
        public CollegeService(PocDbContext dbContext)
        {
            _dbContext = dbContext;
            //  _logger= logger;
        }


        public async Task<List<College>> GetAll()
        {

            try
            {

                var obj = await _dbContext.Colleges
                 .Include(c => c.Departments)
                 .ThenInclude(c => c.Students)
                 .OrderByDescending(x => x.Cid).AsNoTracking().ToListAsync();
                return obj;
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
            finally
            {
                _dbContext.Dispose();
            }

        }

        //public IEnumerable<College> GetAll()
        //{
        //    return _dbContext.Colleges.ToList();
        //}

        public async Task<College> GetCollegeById(int id)
        {
            // var transaction =  _dbContext.Database.BeginTransactionAsync();
            try
            {
                var clg = _dbContext.Colleges.FirstOrDefault(c => c.Cid == id);
                if (clg == null)
                {

                    throw new ApiException("College Not Found");

                }
                return clg;

            }
            catch (ApiException ex)
            {
                //  transaction.Commit();
                throw new ApiException(ex.Message);
            }
            finally
            {
                _dbContext.Dispose();
            }
        }

        public async Task<College> GetCollegeByName(string name)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var clg = _dbContext.Colleges.FirstOrDefault(c => c.Cname == name);
                if (clg == null)
                {
                    throw new KeyNotFoundException("College not found");

                }
                return clg;

            }
            catch (Exception ex)
            {
                transaction.Commit();
                throw new ArgumentException(ex.Message);
            }
            finally { _dbContext.Dispose(); }
        }

        public async Task<College> PostCollege(College college)
        {
            //  var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                await _dbContext.Colleges.AddAsync(college);
                await _dbContext.SaveChangesAsync();
                return college;
            }
            catch (ApiException ex)
            {
                //   transaction.Commit();
                throw new ApiException(ex.Message);
            }
            finally
            {
                _dbContext.Dispose();
            }

        }

        //public int Update(int clg)
        //{
        //   // var transaction = _dbContext.Database.BeginTransactionAsync();
        //    try
        //    {
        //       // _dbContext.Colleges.Update(clg);
        //        _dbContext.SaveChangesAsync();
        //        return clg;
        //    }
        //    catch (ApiException ex)
        //    {
        //       // transaction.Commit();
        //        throw new ApiException(ex.Message);
        //    }
        //    finally { _dbContext.Dispose(); }

        //}
        public async Task<College> Update(College clg)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _dbContext.Colleges.Update(clg);
                _dbContext.SaveChangesAsync();
                return clg;
            }
            catch (ApiException ex)
            {
                transaction.Commit();
                throw new ApiException(ex.Message);
            }
            finally { _dbContext.Dispose(); }

        }

        //public void DeleteCollegeById(int id)
        //{
        //    try
        //    {
        //        //College  getCollegeByID= _dbContext.Colleges.FirstOrDefault(c => c.Cid ==id);

        //        //if (getCollegeByID != null)
        //        //{
        //        //    College college = new College()
        //        //    {
        //        //        ActiveFlag = getCollegeByID.ActiveFlag,
        //        //    };
        //        //    _dbContext.Colleges.Update(college);
        //        //    _dbContext.SaveChangesAsync();
        //        //}


        //    }
        //    catch (ApiException ex)
        //    {
        //        throw new ApiException(ex.Message);
        //    }
        //    finally { _dbContext.Dispose(); }
        //}
    }
}
