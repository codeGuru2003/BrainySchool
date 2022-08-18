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
    public class ClassBillingItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassBillingItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassBillingItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClassBillingItems.Include(c => c.Class).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClassBillingItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classBillingItems = await _context.ClassBillingItems
                .Include(c => c.Class)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classBillingItems == null)
            {
                return NotFound();
            }

            return View(classBillingItems);
        }

        // GET: ClassBillingItems/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name");
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ClassBillingItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassId,Name,AmountInUSD,AmountInLRD,Description,RecordedBy,DateRecorded")] ClassBillingItems classBillingItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classBillingItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", classBillingItems.ClassId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", classBillingItems.RecordedBy);
            return View(classBillingItems);
        }

        // GET: ClassBillingItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classBillingItems = await _context.ClassBillingItems.FindAsync(id);
            if (classBillingItems == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", classBillingItems.ClassId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", classBillingItems.RecordedBy);
            return View(classBillingItems);
        }

        // POST: ClassBillingItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassId,Name,AmountInUSD,AmountInLRD,Description,RecordedBy,DateRecorded")] ClassBillingItems classBillingItems)
        {
            if (id != classBillingItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classBillingItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassBillingItemsExists(classBillingItems.Id))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", classBillingItems.ClassId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", classBillingItems.RecordedBy);
            return View(classBillingItems);
        }

        // GET: ClassBillingItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classBillingItems = await _context.ClassBillingItems
                .Include(c => c.Class)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classBillingItems == null)
            {
                return NotFound();
            }

            return View(classBillingItems);
        }

        // POST: ClassBillingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classBillingItems = await _context.ClassBillingItems.FindAsync(id);
            _context.ClassBillingItems.Remove(classBillingItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassBillingItemsExists(int id)
        {
            return _context.ClassBillingItems.Any(e => e.Id == id);
        }
    }
}
