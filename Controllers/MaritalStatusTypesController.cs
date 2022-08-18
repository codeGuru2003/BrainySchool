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
    public class MaritalStatusTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MaritalStatusTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MaritalStatusTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MaritalStatuses.Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MaritalStatusTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritalStatusType = await _context.MaritalStatuses
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maritalStatusType == null)
            {
                return NotFound();
            }

            return View(maritalStatusType);
        }

        // GET: MaritalStatusTypes/Create
        public IActionResult Create()
        {
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: MaritalStatusTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RecordedBy,DateRecorded")] MaritalStatusType maritalStatusType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maritalStatusType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", maritalStatusType.RecordedBy);
            return View(maritalStatusType);
        }

        // GET: MaritalStatusTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritalStatusType = await _context.MaritalStatuses.FindAsync(id);
            if (maritalStatusType == null)
            {
                return NotFound();
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", maritalStatusType.RecordedBy);
            return View(maritalStatusType);
        }

        // POST: MaritalStatusTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,RecordedBy,DateRecorded")] MaritalStatusType maritalStatusType)
        {
            if (id != maritalStatusType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maritalStatusType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaritalStatusTypeExists(maritalStatusType.Id))
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
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", maritalStatusType.RecordedBy);
            return View(maritalStatusType);
        }

        // GET: MaritalStatusTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritalStatusType = await _context.MaritalStatuses
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maritalStatusType == null)
            {
                return NotFound();
            }

            return View(maritalStatusType);
        }

        // POST: MaritalStatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maritalStatusType = await _context.MaritalStatuses.FindAsync(id);
            _context.MaritalStatuses.Remove(maritalStatusType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaritalStatusTypeExists(int id)
        {
            return _context.MaritalStatuses.Any(e => e.Id == id);
        }
    }
}
