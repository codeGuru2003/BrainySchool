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
    public class AcademicSchoolYearTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public AcademicSchoolYearTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AcademicSchoolYearTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AcademicSchoolYearTypes.Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AcademicSchoolYearTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSchoolYearType = await _context.AcademicSchoolYearTypes
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicSchoolYearType == null)
            {
                return NotFound();
            }

            return View(academicSchoolYearType);
        }

        // GET: AcademicSchoolYearTypes/Create
        public IActionResult Create()
        {
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: AcademicSchoolYearTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RecordedBy,DateRecorded")] AcademicSchoolYearType academicSchoolYearType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            { 
                academicSchoolYearType.RecordedBy = user.Id;
                academicSchoolYearType.DateRecorded = DateTime.Now;
                _context.Add(academicSchoolYearType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSchoolYearType.RecordedBy);
            return View(academicSchoolYearType);
        }

        // GET: AcademicSchoolYearTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSchoolYearType = await _context.AcademicSchoolYearTypes.FindAsync(id);
            if (academicSchoolYearType == null)
            {
                return NotFound();
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSchoolYearType.RecordedBy);
            return View(academicSchoolYearType);
        }

        // POST: AcademicSchoolYearTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,RecordedBy,DateRecorded")] AcademicSchoolYearType academicSchoolYearType)
        {
            if (id != academicSchoolYearType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicSchoolYearType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicSchoolYearTypeExists(academicSchoolYearType.Id))
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
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSchoolYearType.RecordedBy);
            return View(academicSchoolYearType);
        }

        // GET: AcademicSchoolYearTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSchoolYearType = await _context.AcademicSchoolYearTypes
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicSchoolYearType == null)
            {
                return NotFound();
            }

            return View(academicSchoolYearType);
        }

        // POST: AcademicSchoolYearTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicSchoolYearType = await _context.AcademicSchoolYearTypes.FindAsync(id);
            _context.AcademicSchoolYearTypes.Remove(academicSchoolYearType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicSchoolYearTypeExists(int id)
        {
            return _context.AcademicSchoolYearTypes.Any(e => e.Id == id);
        }
    }
}
