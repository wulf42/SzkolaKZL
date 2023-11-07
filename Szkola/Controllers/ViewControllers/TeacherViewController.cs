using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Szkola.Data;
using Szkola.Models;
using Szkola.Models.ViewModels;

namespace Szkola.Controllers
{
    public class TeacherViewController : Controller
    {
        private readonly SzkolaDataContext _context;

        public TeacherViewController(SzkolaDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
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

            return View(teachers);
        }

        public async Task<IActionResult> Details(int id)
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
                }).FirstOrDefaultAsync();

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Teacher updatedTeacher)
        {
            if (id != updatedTeacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(updatedTeacher);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Teachers.Any(n => n.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction("Index");
            }

            return View(updatedTeacher);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}