using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Szkola.Data;
using Szkola.Models;
using Szkola.Models.ViewModels;

namespace Szkola.Controllers
{
    public class ClassViewController : Controller
    {
        private readonly SzkolaDataContext _context;

        public ClassViewController(SzkolaDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var classes = await _context.Classes
                .Select(k => new ClassViewModel
                {
                    Id = k.Id,
                    Name = k.Name,
                    Students = k.Students.Select(u => u.FirstName + " " + u.LastName).ToList(),
                    Teachers = k.ClassTeachers.Select(kn => kn.Teacher.FirstName + " " + kn.Teacher.LastName).ToList()
                }).ToListAsync();

            return View(classes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var schoolClass = await _context.Classes
                .Where(k => k.Id == id)
                .Select(k => new ClassViewModel
                {
                    Id = k.Id,
                    Name = k.Name,
                    Students = k.Students.Select(u => u.FirstName + " " + u.LastName).ToList(),
                    Teachers = k.ClassTeachers.Select(kn => kn.Teacher.FirstName + " " + kn.Teacher.LastName).ToList()
                }).FirstOrDefaultAsync();

            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Class schoolClass)
        {
            if (ModelState.IsValid)
            {
                _context.Classes.Add(schoolClass);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(schoolClass);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var schoolClass = await _context.Classes.FindAsync(id);

            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Class updatedClass)
        {
            if (id != updatedClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(updatedClass);

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

                return RedirectToAction("Index");
            }

            return View(updatedClass);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var schoolClass = await _context.Classes.FindAsync(id);

            if (schoolClass == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(schoolClass);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}