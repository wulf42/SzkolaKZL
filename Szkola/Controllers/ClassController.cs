using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Szkola.Data;
using Szkola.Models;
using Szkola.Models.ViewModels;

namespace Szkola.Controllers
{
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly SzkolaDataContext _context;

        public ClassController(SzkolaDataContext context)
        {
            _context = context;
        }

        // Get Classes
        [HttpGet("classes")]
        public async Task<IActionResult> GetClasses()
        {
            var classes = await _context.Classes
                .Select(k => new ClassViewModel
                {
                    Id = k.Id,
                    Name = k.Name,
                    Students = k.Students.Select(u => u.FirstName + " " + u.LastName).ToList(),
                    Teachers = k.ClassTeachers.Select(kn => kn.Teacher.FirstName + " " + kn.Teacher.LastName).ToList()
                }).ToListAsync();
            return Ok(classes);
        }

        [HttpGet("classes/{id}")]
        public async Task<IActionResult> GetClass(int id)
        {
            var schoolClass = await _context.Classes
                .Where(k => k.Id == id)
                .Select(k => new ClassViewModel
                {
                    Id = k.Id,
                    Name = k.Name,
                    Students = k.Students.Select(u => u.FirstName + " " + u.LastName).ToList(),
                    Teachers = k.ClassTeachers.Select(kn => kn.Teacher.FirstName + " " + kn.Teacher.LastName).ToList()
                }).ToListAsync();

            if (schoolClass.Count == 0)
            {
                return NotFound();
            }

            return Ok(schoolClass);
        }

        // Create Class
        [HttpPost("classes")]
        public async Task<IActionResult> CreateClass([FromBody] Class schoolClass)
        {
            _context.Classes.Add(schoolClass);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetClasses", new { id = schoolClass.Id }, schoolClass);
        }

        // Update Class
        [HttpPut("classes/{id}")]
        public async Task<IActionResult> UpdateClass(int id, [FromBody] Class updatedClass)
        {
            var schoolClass = await _context.Classes.FindAsync(id);

            if (schoolClass == null)
            {
                return NotFound();
            }

            if (updatedClass.Name != null)
            {
                schoolClass.Name = updatedClass.Name;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Classes.Any(c => c.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // Delete Class
        [HttpDelete("classes/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var schoolClass = await _context.Classes.FindAsync(id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(schoolClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}