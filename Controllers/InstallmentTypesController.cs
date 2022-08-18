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
    public class InstallmentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstallmentTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InstallmentTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InstallmentTypes.Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InstallmentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var installmentType = await _context.InstallmentTypes
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (installmentType == null)
            {
                return NotFound();
            }

            return View(installmentType);
        }

        // GET: InstallmentTypes/Create
        public IActionResult Create()
        {
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: InstallmentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RecordedBy,DateRecorded")] InstallmentType installmentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(installmentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", installmentType.RecordedBy);
            return View(installmentType);
        }

        // GET: InstallmentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var installmentType = await _context.InstallmentTypes.FindAsync(id);
            if (installmentType == null)
            {
                return NotFound();
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", installmentType.RecordedBy);
            return View(installmentType);
        }

        // POST: InstallmentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,RecordedBy,DateRecorded")] InstallmentType installmentType)
        {
            if (id != installmentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(installmentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstallmentTypeExists(installmentType.Id))
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
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", installmentType.RecordedBy);
            return View(installmentType);
        }

        // GET: InstallmentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var installmentType = await _context.InstallmentTypes
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (installmentType == null)
            {
                return NotFound();
            }

            return View(installmentType);
        }

        // POST: InstallmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var installmentType = await _context.InstallmentTypes.FindAsync(id);
            _context.InstallmentTypes.Remove(installmentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstallmentTypeExists(int id)
        {
            return _context.InstallmentTypes.Any(e => e.Id == id);
        }
    }
}
