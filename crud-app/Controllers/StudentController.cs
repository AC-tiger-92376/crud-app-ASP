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
        public async Task<IActionResult> GetStudent(
            string? search,
            string? sort = "name",
            string? order = "asc",
            int page = 1,
            int pageSize = 10)
        {
            var query = _studentContext.Students.AsQueryable();

            // Search
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.FirstName.Contains(search));
            }

            // Sorting
            query = (sort.ToLower(), order.ToLower()) switch
            {
                ("firstname", "asc") => query.OrderBy(p => p.FirstName),
                ("firstname", "desc") => query.OrderByDescending(p => p.FirstName),
                ("age", "asc") => query.OrderBy(p => p.Age),
                ("age", "desc") => query.OrderByDescending(p => p.Age),
                ("email", "asc") => query.OrderBy(p => p.Email),
                ("email", "desc") => query.OrderByDescending(p => p.Email),
                ("address", "asc") => query.OrderBy(p => p.Address),
                ("address", "desc") => query.OrderByDescending(p => p.Address),
                ("city", "asc") => query.OrderBy(p => p.City),
                ("city", "desc") => query.OrderByDescending(p => p.City),
                _ => query.OrderBy(p => p.Id)
            };

            // Pagination
            var total = await query.CountAsync();
            var products = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new
            {
                total,
                page,
                pageSize,
                data = products
            });
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
