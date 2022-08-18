using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthRecordsPro.Data;
using HealthRecordsPro.Models;

namespace HealthRecordsPro.Controllers
{
    public class AcademicSemesterPeriodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AcademicSemesterPeriodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AcademicSemesterPeriods
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AcademicSemesterPeriods.Include(a => a.AcademicSemester).Include(a => a.PeriodType).Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AcademicSemesterPeriods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSemesterPeriod = await _context.AcademicSemesterPeriods
                .Include(a => a.AcademicSemester)
                .Include(a => a.PeriodType)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicSemesterPeriod == null)
            {
                return NotFound();
            }

            return View(academicSemesterPeriod);
        }

        // GET: AcademicSemesterPeriods/Create
        public IActionResult Create()
        {
            ViewData["AcademicSemesterId"] = new SelectList(_context.AcademicSemesters, "Id", "Id");
            ViewData["PeriodTypeId"] = new SelectList(_context.PeriodTypes, "Id", "Name");
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: AcademicSemesterPeriods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PeriodTypeId,AcademicSemesterId,RecordedBy,DateRecorded")] AcademicSemesterPeriod academicSemesterPeriod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicSemesterPeriod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicSemesterId"] = new SelectList(_context.AcademicSemesters, "Id", "Id", academicSemesterPeriod.AcademicSemesterId);
            ViewData["PeriodTypeId"] = new SelectList(_context.PeriodTypes, "Id", "Name", academicSemesterPeriod.PeriodTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSemesterPeriod.RecordedBy);
            return View(academicSemesterPeriod);
        }

        // GET: AcademicSemesterPeriods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSemesterPeriod = await _context.AcademicSemesterPeriods.FindAsync(id);
            if (academicSemesterPeriod == null)
            {
                return NotFound();
            }
            ViewData["AcademicSemesterId"] = new SelectList(_context.AcademicSemesters, "Id", "Id", academicSemesterPeriod.AcademicSemesterId);
            ViewData["PeriodTypeId"] = new SelectList(_context.PeriodTypes, "Id", "Name", academicSemesterPeriod.PeriodTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSemesterPeriod.RecordedBy);
            return View(academicSemesterPeriod);
        }

        // POST: AcademicSemesterPeriods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PeriodTypeId,AcademicSemesterId,RecordedBy,DateRecorded")] AcademicSemesterPeriod academicSemesterPeriod)
        {
            if (id != academicSemesterPeriod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicSemesterPeriod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicSemesterPeriodExists(academicSemesterPeriod.Id))
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
            ViewData["AcademicSemesterId"] = new SelectList(_context.AcademicSemesters, "Id", "Id", academicSemesterPeriod.AcademicSemesterId);
            ViewData["PeriodTypeId"] = new SelectList(_context.PeriodTypes, "Id", "Name", academicSemesterPeriod.PeriodTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSemesterPeriod.RecordedBy);
            return View(academicSemesterPeriod);
        }

        // GET: AcademicSemesterPeriods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSemesterPeriod = await _context.AcademicSemesterPeriods
                .Include(a => a.AcademicSemester)
                .Include(a => a.PeriodType)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicSemesterPeriod == null)
            {
                return NotFound();
            }

            return View(academicSemesterPeriod);
        }

        // POST: AcademicSemesterPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicSemesterPeriod = await _context.AcademicSemesterPeriods.FindAsync(id);
            _context.AcademicSemesterPeriods.Remove(academicSemesterPeriod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicSemesterPeriodExists(int id)
        {
            return _context.AcademicSemesterPeriods.Any(e => e.Id == id);
        }
    }
}
