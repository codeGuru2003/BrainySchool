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
    public class AcademicSemesterTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AcademicSemesterTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AcademicSemesterTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AcademicSemesterTypes.Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AcademicSemesterTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSemesterType = await _context.AcademicSemesterTypes
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicSemesterType == null)
            {
                return NotFound();
            }

            return View(academicSemesterType);
        }

        // GET: AcademicSemesterTypes/Create
        public IActionResult Create()
        {
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: AcademicSemesterTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RecordedBy,DateRecorded")] AcademicSemesterType academicSemesterType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                academicSemesterType.DateRecorded = DateTime.Now;
                academicSemesterType.RecordedBy = user.Id;
                _context.Add(academicSemesterType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSemesterType.RecordedBy);
            return View(academicSemesterType);
        }

        // GET: AcademicSemesterTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSemesterType = await _context.AcademicSemesterTypes.FindAsync(id);
            if (academicSemesterType == null)
            {
                return NotFound();
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSemesterType.RecordedBy);
            return View(academicSemesterType);
        }

        // POST: AcademicSemesterTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,RecordedBy,DateRecorded")] AcademicSemesterType academicSemesterType)
        {
            if (id != academicSemesterType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicSemesterType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicSemesterTypeExists(academicSemesterType.Id))
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
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", academicSemesterType.RecordedBy);
            return View(academicSemesterType);
        }

        // GET: AcademicSemesterTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicSemesterType = await _context.AcademicSemesterTypes
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicSemesterType == null)
            {
                return NotFound();
            }

            return View(academicSemesterType);
        }

        // POST: AcademicSemesterTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicSemesterType = await _context.AcademicSemesterTypes.FindAsync(id);
            _context.AcademicSemesterTypes.Remove(academicSemesterType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicSemesterTypeExists(int id)
        {
            return _context.AcademicSemesterTypes.Any(e => e.Id == id);
        }
    }
}
