using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Szkola.Data;
using Szkola.Models;
using Szkola.Models.ViewModels;

namespace Szkola.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly SzkolaDataContext _context;

        public StudentController(SzkolaDataContext context)
        {
            _context = context;
        }

        // Get wszystkich uczniów oraz wybranego ucznia
        [HttpGet("students")]
        public async Task<IActionResult> GetStudents([FromQuery] string? firstNameFilter)
        {
            //Filtrowanie po imieniu
            var studentQuery = _context.Students.AsQueryable();
            if (!string.IsNullOrEmpty(firstNameFilter))
            {
                studentQuery = studentQuery.Where(u => u.FirstName.Contains(firstNameFilter));
            }

            var students = await studentQuery.Select(u => new StudentViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DateOfBirth = u.DateOfBirth,
                LastGradePointAverage = u.LastGradePointAverage,
                Class = u.Class.Name
            }).ToListAsync();
            return Ok(students);
        }

        [HttpGet("students/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _context.Students
                .Where(u => u.Id == id)
                .Select(u => new StudentViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    DateOfBirth = u.DateOfBirth,
                    LastGradePointAverage = u.LastGradePointAverage,
                    Class = u.Class.Name
                }).ToListAsync();

            if (student.Count == 0)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // Create ucznia
        [HttpPost("students")]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            var klasa = await _context.Classes.FindAsync(student.IdClass);

            if (klasa == null)
            {
                return NotFound("Klasa o podanym identyfikatorze nie istnieje.");
            }
            student.IdClass = klasa.Id;

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetStudents", new { id = student.Id }, student);
        }

        // Update ucznia
        [HttpPut("students/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            // zaktualizuj tylko pola które zostały zmienione w zapytaniu
            if (updatedStudent.FirstName != null)
            {
                student.FirstName = updatedStudent.FirstName;
            }
            if (updatedStudent.LastName != null)
            {
                student.LastName = updatedStudent.LastName;
            }
            if (updatedStudent.DateOfBirth != new DateOnly(1, 1, 1))
            {
                student.DateOfBirth = updatedStudent.DateOfBirth;
            }
            if (updatedStudent.LastGradePointAverage != 0)
            {
                student.LastGradePointAverage = updatedStudent.LastGradePointAverage;
            }
            if (updatedStudent.IdClass != 0)
            {
                student.IdClass = updatedStudent.IdClass;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Students.Any(s => s.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // Delete ucznia
        [HttpDelete("students/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}