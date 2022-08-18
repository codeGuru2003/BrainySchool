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
    public class ClassInstallmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassInstallmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassInstallments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClassInstallment.Include(c => c.Class).Include(c => c.InstallmentType).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClassInstallments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classInstallment = await _context.ClassInstallment
                .Include(c => c.Class)
                .Include(c => c.InstallmentType)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classInstallment == null)
            {
                return NotFound();
            }

            return View(classInstallment);
        }

        // GET: ClassInstallments/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name");
            ViewData["InstallmentTypeId"] = new SelectList(_context.InstallmentTypes, "Id", "Name");
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ClassInstallments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassId,InstallmentTypeId,AmountinUSD,AmountinLRD,StartDate,EndDate,RecordedBy,DateRecorded")] ClassInstallment classInstallment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classInstallment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", classInstallment.ClassId);
            ViewData["InstallmentTypeId"] = new SelectList(_context.InstallmentTypes, "Id", "Name", classInstallment.InstallmentTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", classInstallment.RecordedBy);
            return View(classInstallment);
        }

        // GET: ClassInstallments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classInstallment = await _context.ClassInstallment.FindAsync(id);
            if (classInstallment == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", classInstallment.ClassId);
            ViewData["InstallmentTypeId"] = new SelectList(_context.InstallmentTypes, "Id", "Name", classInstallment.InstallmentTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", classInstallment.RecordedBy);
            return View(classInstallment);
        }

        // POST: ClassInstallments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassId,InstallmentTypeId,AmountinUSD,AmountinLRD,StartDate,EndDate,RecordedBy,DateRecorded")] ClassInstallment classInstallment)
        {
            if (id != classInstallment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classInstallment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassInstallmentExists(classInstallment.Id))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", classInstallment.ClassId);
            ViewData["InstallmentTypeId"] = new SelectList(_context.InstallmentTypes, "Id", "Name", classInstallment.InstallmentTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", classInstallment.RecordedBy);
            return View(classInstallment);
        }

        // GET: ClassInstallments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classInstallment = await _context.ClassInstallment
                .Include(c => c.Class)
                .Include(c => c.InstallmentType)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classInstallment == null)
            {
                return NotFound();
            }

            return View(classInstallment);
        }

        // POST: ClassInstallments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classInstallment = await _context.ClassInstallment.FindAsync(id);
            _context.ClassInstallment.Remove(classInstallment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassInstallmentExists(int id)
        {
            return _context.ClassInstallment.Any(e => e.Id == id);
        }
    }
}
