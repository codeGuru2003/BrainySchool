using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthRecordsPro.Data;
using HealthRecordsPro.Models;
using Microsoft.AspNetCore.Identity;

namespace HealthRecordsPro.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Students.Include(s => s.GenderType).Include(s => s.NationalityType).Include(s => s.StudentType).Include(s => s.StudentUser).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.GenderType)
                .Include(s => s.NationalityType)
                .Include(s => s.StudentType)
                .Include(s => s.StudentUser)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["GenderTypeId"] = new SelectList(_context.GenderTypes, "Id", "Name");
            ViewData["NationalityTypeId"] = new SelectList(_context.NationalityTypes, "Id", "Name");
            ViewData["StudentTypeId"] = new SelectList(_context.StudentTypes, "Id", "Name");
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Middlename,Lastname,DateofBirth,NationalityTypeId,StudentTypeId,PhoneNumber,Address,ApplicationUserId,Image,ImageName,IsActive,GenderTypeId,RecordedBy,DateRecorded")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Student was created successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderTypeId"] = new SelectList(_context.GenderTypes, "Id", "Name", student.GenderTypeId);
            ViewData["NationalityTypeId"] = new SelectList(_context.NationalityTypes, "Id", "Name", student.NationalityTypeId);
            ViewData["StudentTypeId"] = new SelectList(_context.StudentTypes, "Id", "Name", student.StudentTypeId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", student.ApplicationUserId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", student.RecordedBy);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["GenderTypeId"] = new SelectList(_context.GenderTypes, "Id", "Name", student.GenderTypeId);
            ViewData["NationalityTypeId"] = new SelectList(_context.NationalityTypes, "Id", "Name", student.NationalityTypeId);
            ViewData["StudentTypeId"] = new SelectList(_context.StudentTypes, "Id", "Name", student.StudentTypeId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", student.ApplicationUserId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", student.RecordedBy);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Middlename,Lastname,DateofBirth,NationalityTypeId,StudentTypeId,PhoneNumber,Address,ApplicationUserId,Image,ImageName,IsActive,GenderTypeId,RecordedBy,DateRecorded")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderTypeId"] = new SelectList(_context.GenderTypes, "Id", "Name", student.GenderTypeId);
            ViewData["NationalityTypeId"] = new SelectList(_context.NationalityTypes, "Id", "Name", student.NationalityTypeId);
            ViewData["StudentTypeId"] = new SelectList(_context.StudentTypes, "Id", "Name", student.StudentTypeId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", student.ApplicationUserId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", student.RecordedBy);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.GenderType)
                .Include(s => s.NationalityType)
                .Include(s => s.StudentType)
                .Include(s => s.StudentUser)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
