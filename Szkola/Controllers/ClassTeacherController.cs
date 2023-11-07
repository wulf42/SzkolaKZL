using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Szkola.Data;
using Szkola.Models;
using Szkola.Models.ViewModels;

namespace Szkola.Controllers
{
    [ApiController]
    public class ClassTeacherController : ControllerBase
    {
        private readonly SzkolaDataContext _context;

        public ClassTeacherController(SzkolaDataContext context)
        {
            _context = context;
        }

        // Get ClassesTeacher
        [HttpGet("classTeacher")]
        public async Task<IActionResult> GetClassTeacher()
        {
            var classesTeachers = await _context.ClassTeachers
                .Select(ct => new ClassTeacherViewModel
                {
                    Id = ct.Id,
                    ClassId = ct.ClassId,
                    TeacherId = ct.TeacherId
                }).ToListAsync();
            return Ok(classesTeachers);
        }

        [HttpGet("classTeacher/{id}")]
        public async Task<IActionResult> GetClassTeacher(int id)
        {
            var classesTeachers = await _context.ClassTeachers
                .Where(c => c.Id == id)
                .Select(ct => new ClassTeacherViewModel
                {
                    Id = ct.Id,
                    ClassId = ct.ClassId,
                    TeacherId = ct.TeacherId
                }).ToListAsync();

            if (classesTeachers.Count == 0)
            {
                return NotFound();
            }

            return Ok(classesTeachers);
        }

        // Create Class
        [HttpPost("classTeacher")]
        public async Task<IActionResult> CreateClassTeacher([FromBody] ClassTeacher classTeacher)
        {
            _context.ClassTeachers.Add(classTeacher);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetClasses", new { id = classTeacher }, classTeacher);
        }

        // Update Class
        [HttpPut("classTeacher/{id}")]
        public async Task<IActionResult> UpdateClassTeacher(int id, [FromBody] ClassTeacher updatedClass)
        {
            var classTeacher = await _context.ClassTeachers.FindAsync(id);

            if (classTeacher == null)
            {
                return NotFound();
            }

            if (updatedClass.ClassId != null)
            {
                classTeacher.ClassId = updatedClass.ClassId;
            }
            if (updatedClass.TeacherId != null)
            {
                classTeacher.TeacherId = updatedClass.TeacherId;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ClassTeachers.Any(c => c.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // Delete Class
        [HttpDelete("classTeacher/{id}")]
        public async Task<IActionResult> DeleteClassTeacher(int id)
        {
            var schoolClass = await _context.ClassTeachers.FindAsync(id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            _context.ClassTeachers.Remove(schoolClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}