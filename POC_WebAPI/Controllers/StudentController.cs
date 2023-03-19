using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC_WebAPI_BusinessLogicLayer.Repositories.IServices;

using POC_WebAPI_DataAccessLayer.Models;

namespace POC_WebAPI_UserInterface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _istudentService;
        public StudentController(IStudentService studentService) => _istudentService = studentService;


        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetAll() => Ok(await _istudentService.GetAll());


        [HttpGet("GetStudentsById")]
        public async Task<IActionResult> GetStudents(int id) => Ok(await _istudentService.GetStudentById(id));


        [HttpGet("GetStudentsByName")]
        public async Task<IActionResult> GetStudentsByName(string name) => Ok(await _istudentService.GetStudentByName(name));


        [HttpPost("AddStudents")]
        public IActionResult Create([FromBody] Student student)
        {
            try
            {
                var std = _istudentService.PostStudent(student);
                if (std == null)
                {
                    return BadRequest();
                }
                return Ok(std);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
            return BadRequest();
        }

        [HttpPut("UpdateStudents")]
        public async Task<IActionResult> UpdateStudent(Student student) => Ok(await _istudentService.UpdateStudent(student));



    }
}
