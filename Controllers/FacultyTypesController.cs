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
    public class FacultyTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FacultyTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: FacultyTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FacultyTypes.Include(f => f.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FacultyTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyType = await _context.FacultyTypes
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultyType == null)
            {
                return NotFound();
            }

            return View(facultyType);
        }

        // GET: FacultyTypes/Create
        public IActionResult Create()
        {
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: FacultyTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RecordedBy,DateRecorded")] FacultyType facultyType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                facultyType.DateRecorded = DateTime.Now;
                facultyType.RecordedBy = user.Id;
                _context.Add(facultyType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", facultyType.RecordedBy);
            return View(facultyType);
        }

        // GET: FacultyTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyType = await _context.FacultyTypes.FindAsync(id);
            if (facultyType == null)
            {
                return NotFound();
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", facultyType.RecordedBy);
            return View(facultyType);
        }

        // POST: FacultyTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,RecordedBy,DateRecorded")] FacultyType facultyType)
        {
            if (id != facultyType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultyType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyTypeExists(facultyType.Id))
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
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", facultyType.RecordedBy);
            return View(facultyType);
        }

        // GET: FacultyTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyType = await _context.FacultyTypes
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultyType == null)
            {
                return NotFound();
            }

            return View(facultyType);
        }

        // POST: FacultyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facultyType = await _context.FacultyTypes.FindAsync(id);
            _context.FacultyTypes.Remove(facultyType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyTypeExists(int id)
        {
            return _context.FacultyTypes.Any(e => e.Id == id);
        }
    }
}
