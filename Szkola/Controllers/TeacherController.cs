using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Szkola.Data;
using Szkola.Models;
using Szkola.Models.ViewModels;

namespace Szkola.Controllers
{
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly SzkolaDataContext _context;

        public TeacherController(SzkolaDataContext context)
        {
            _context = context;
        }

        //Get wszystkich nauczycieli oraz wybranego nauczyciela
        [HttpGet("teachers")]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _context.Teachers
                .Select(n => new TeacherViewModel
                {
                    Id = n.Id,
                    FirstName = n.FirstName,
                    LastName = n.LastName,
                    EmploymentType = n.EmploymentType,
                    DateOfEmployment = n.DateOfEmployment,
                    Classes = n.ClassTeachers.Select(kn => kn.Class.Name).ToList()
                }).ToListAsync();
            return Ok(teachers);
        }

        [HttpGet("teachers/{id}")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await _context.Teachers
                .Where(n => n.Id == id)
                .Select(n => new TeacherViewModel
                {
                    Id = n.Id,
                    FirstName = n.FirstName,
                    LastName = n.LastName,
                    EmploymentType = n.EmploymentType,
                    DateOfEmployment = n.DateOfEmployment,
                    Classes = n.ClassTeachers.Select(kn => kn.Class.Name).ToList()
                }).ToListAsync();

            if (teacher.Count == 0)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        //Create nauczyciela
        [HttpPost("teachers")]
        public async Task<IActionResult> CreateTeacher([FromBody] Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTeachers", new { id = teacher.Id }, teacher);
        }

        //Update nauczyciela
        [HttpPut("teachers/{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] Teacher updatedTeacher)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            // zaktualizuj tylko pola które zostały zmienione w zapytaniu
            if (updatedTeacher.FirstName != null)
            {
                teacher.FirstName = updatedTeacher.FirstName;
            }
            if (updatedTeacher.LastName != null)
            {
                teacher.LastName = updatedTeacher.LastName;
            }
            if (updatedTeacher.EmploymentType != 0)
            {
                teacher.EmploymentType = updatedTeacher.EmploymentType;
            }
            if (updatedTeacher.DateOfEmployment != new DateOnly(1, 1, 1))
            {
                teacher.DateOfEmployment = updatedTeacher.DateOfEmployment;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Teachers.Any(t => t.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        //Delete nauczyciela
        [HttpDelete("teachers/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}