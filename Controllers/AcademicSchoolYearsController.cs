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
    public class AcademicSchoolYearsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AcademicSchoolYearsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AcademicSchoolYears
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AcademicSchoolYears.Include(a => a.AcademicSchoolYearType).Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AcademicSchoolYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSchoolYear = await _context.AcademicSchoolYears
                .Include(a => a.AcademicSchoolYearType)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicSchoolYear == null)
            {
                return NotFound();
            }

            return View(academicSchoolYear);
        }

        // GET: AcademicSchoolYears/Create
        public IActionResult Create()
        {
            ViewData["AcademicSchoolTypeId"] = new SelectList(_context.AcademicSchoolYearTypes, "Id", "Name");
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: AcademicSchoolYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AcademicSchoolTypeId,IsActive,RecordedBy,DateRecorded")] AcademicSchoolYear academicSchoolYear)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                academicSchoolYear.DateRecorded = DateTime.Now;
                academicSchoolYear.RecordedBy = user.Id;
                _context.Add(academicSchoolYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicSchoolTypeId"] = new SelectList(_context.AcademicSchoolYearTypes, "Id", "Name", academicSchoolYear.AcademicSchoolTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSchoolYear.RecordedBy);
            return View(academicSchoolYear);
        }

        // GET: AcademicSchoolYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSchoolYear = await _context.AcademicSchoolYears.FindAsync(id);
            if (academicSchoolYear == null)
            {
                return NotFound();
            }
            ViewData["AcademicSchoolTypeId"] = new SelectList(_context.AcademicSchoolYearTypes, "Id", "Name", academicSchoolYear.AcademicSchoolTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSchoolYear.RecordedBy);
            return View(academicSchoolYear);
        }

        // POST: AcademicSchoolYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AcademicSchoolTypeId,IsActive,RecordedBy,DateRecorded")] AcademicSchoolYear academicSchoolYear)
        {
            if (id != academicSchoolYear.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicSchoolYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicSchoolYearExists(academicSchoolYear.Id))
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
            ViewData["AcademicSchoolTypeId"] = new SelectList(_context.AcademicSchoolYearTypes, "Id", "Name", academicSchoolYear.AcademicSchoolTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSchoolYear.RecordedBy);
            return View(academicSchoolYear);
        }

        // GET: AcademicSchoolYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSchoolYear = await _context.AcademicSchoolYears
                .Include(a => a.AcademicSchoolYearType)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicSchoolYear == null)
            {
                return NotFound();
            }

            return View(academicSchoolYear);
        }

        // POST: AcademicSchoolYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicSchoolYear = await _context.AcademicSchoolYears.FindAsync(id);
            _context.AcademicSchoolYears.Remove(academicSchoolYear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicSchoolYearExists(int id)
        {
            return _context.AcademicSchoolYears.Any(e => e.Id == id);
        }
    }
}
