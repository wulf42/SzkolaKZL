using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Szkola.Data;
using Szkola.Models;
using Szkola.Models.ViewModels;

namespace Szkola.Controllers
{
    public class StudentViewController : Controller
    {
        private readonly SzkolaDataContext _context;

        public StudentViewController(SzkolaDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? firstNameFilter)
        {
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

            return View(students);
        }

        public async Task<IActionResult> Details(int id)
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
                }).FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Student updatedStudent)
        {
            if (id != updatedStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(updatedStudent);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Students.Any(u => u.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction("Index");
            }

            return View(updatedStudent);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}