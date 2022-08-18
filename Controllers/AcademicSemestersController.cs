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
    public class AcademicSemestersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AcademicSemestersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AcademicSemesters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AcademicSemesters.Include(a => a.AcademicSchoolYear).Include(a => a.AcademicSemesterType).Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AcademicSemesters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSemester = await _context.AcademicSemesters
                .Include(a => a.AcademicSchoolYear)
                .Include(a => a.AcademicSemesterType)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicSemester == null)
            {
                return NotFound();
            }

            return View(academicSemester);
        }

        // GET: AcademicSemesters/Create
        public IActionResult Create()
        {
            ViewData["AcademicSchoolYearId"] = new SelectList(_context.AcademicSchoolYears, "Id", "Id");
            ViewData["AcademicSemesterTypeId"] = new SelectList(_context.AcademicSemesterTypes, "Id", "Name");
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: AcademicSemesters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AcademicSchoolYearId,AcademicSemesterTypeId,StartDate,EndDate,IsActive,RecordedBy,DateRecorded")] AcademicSemester academicSemester)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                academicSemester.DateRecorded = DateTime.Now;
                academicSemester.RecordedBy = user.Id;
                _context.Add(academicSemester);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicSchoolYearId"] = new SelectList(_context.AcademicSchoolYears, "Id", "Id", academicSemester.AcademicSchoolYearId);
            ViewData["AcademicSemesterTypeId"] = new SelectList(_context.AcademicSemesterTypes, "Id", "Name", academicSemester.AcademicSemesterTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSemester.RecordedBy);
            return View(academicSemester);
        }

        // GET: AcademicSemesters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSemester = await _context.AcademicSemesters.FindAsync(id);
            if (academicSemester == null)
            {
                return NotFound();
            }
            ViewData["AcademicSchoolYearId"] = new SelectList(_context.AcademicSchoolYears, "Id", "Id", academicSemester.AcademicSchoolYearId);
            ViewData["AcademicSemesterTypeId"] = new SelectList(_context.AcademicSemesterTypes, "Id", "Name", academicSemester.AcademicSemesterTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSemester.RecordedBy);
            return View(academicSemester);
        }

        // POST: AcademicSemesters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AcademicSchoolYearId,AcademicSemesterTypeId,StartDate,EndDate,IsActive,RecordedBy,DateRecorded")] AcademicSemester academicSemester)
        {
            if (id != academicSemester.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicSemester);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicSemesterExists(academicSemester.Id))
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
            ViewData["AcademicSchoolYearId"] = new SelectList(_context.AcademicSchoolYears, "Id", "Id", academicSemester.AcademicSchoolYearId);
            ViewData["AcademicSemesterTypeId"] = new SelectList(_context.AcademicSemesterTypes, "Id", "Name", academicSemester.AcademicSemesterTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSemester.RecordedBy);
            return View(academicSemester);
        }

        // GET: AcademicSemesters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSemester = await _context.AcademicSemesters
                .Include(a => a.AcademicSchoolYear)
                .Include(a => a.AcademicSemesterType)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicSemester == null)
            {
                return NotFound();
            }

            return View(academicSemester);
        }

        // POST: AcademicSemesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicSemester = await _context.AcademicSemesters.FindAsync(id);
            _context.AcademicSemesters.Remove(academicSemester);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicSemesterExists(int id)
        {
            return _context.AcademicSemesters.Any(e => e.Id == id);
        }
    }
}
