using crud_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace crud_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _studentContext;
        public StudentController(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudent()
        {
            var student = await _studentContext.Students.ToListAsync();
            return Ok(student);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var dbStudent = await _studentContext.Students.FindAsync(id);
            return Ok(dbStudent);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            if (student == null)
            {
                return BadRequest("Not found");
            }

            _studentContext.Students.Add(student);
            await _studentContext.SaveChangesAsync();
            return Ok(await _studentContext.Students.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            var dbStudent = await _studentContext.Students.FindAsync(id);
            if (dbStudent == null)
            {
                return BadRequest("Student not found");
            }

            dbStudent.FirstName = student.FirstName;
            dbStudent.LastName = student.LastName;
            dbStudent.Email = student.Email;
            dbStudent.Address = student.Address;
            dbStudent.City = student.City;
            dbStudent.Age = student.Age;
            dbStudent.IsCheck = student.IsCheck;

            await _studentContext.SaveChangesAsync();
            return Ok(await _studentContext.Students.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var dbStudent = await _studentContext.Students.FindAsync(id);
            if (dbStudent == null)
            {
                return BadRequest("Student not found");
            }

            _studentContext.Students.Remove(dbStudent);
            await _studentContext.SaveChangesAsync();
            return Ok(await _studentContext.Students.ToListAsync());
        }

    }
}
