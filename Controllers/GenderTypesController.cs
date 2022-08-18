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
    public class GenderTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GenderTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: GenderTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GenderTypes.Include(g => g.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GenderTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genderType = await _context.GenderTypes
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genderType == null)
            {
                return NotFound();
            }

            return View(genderType);
        }

        // GET: GenderTypes/Create
        public IActionResult Create()
        {
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: GenderTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RecordedBy,DateRecorded")] GenderType genderType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                genderType.DateRecorded = DateTime.Now;
                genderType.RecordedBy = user.Id;
                _context.Add(genderType);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Record was created successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", genderType.RecordedBy);
            return View(genderType);
        }

        // GET: GenderTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genderType = await _context.GenderTypes.FindAsync(id);
            if (genderType == null)
            {
                return NotFound();
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", genderType.RecordedBy);
            return View(genderType);
        }

        // POST: GenderTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,RecordedBy,DateRecorded")] GenderType genderType)
        {
            if (id != genderType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genderType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenderTypeExists(genderType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "Record was updated successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", genderType.RecordedBy);
            return View(genderType);
        }

        // GET: GenderTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genderType = await _context.GenderTypes
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genderType == null)
            {
                return NotFound();
            }

            return View(genderType);
        }

        // POST: GenderTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genderType = await _context.GenderTypes.FindAsync(id);
            _context.GenderTypes.Remove(genderType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Record was deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool GenderTypeExists(int id)
        {
            return _context.GenderTypes.Any(e => e.Id == id);
        }
    }
}
