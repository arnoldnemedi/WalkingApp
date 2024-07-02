using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WalkingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentsName = new string[] { "stud1", "stud2", "stud3"};

            return Ok(studentsName);
        }
    }
}
