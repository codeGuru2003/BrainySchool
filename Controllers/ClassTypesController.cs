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
    public class ClassTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClassTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ClassTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClassTypes.Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClassTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classType = await _context.ClassTypes
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classType == null)
            {
                return NotFound();
            }

            return View(classType);
        }

        // GET: ClassTypes/Create
        public IActionResult Create()
        {
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ClassTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RecordedBy,DateRecorded")] ClassType classType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                classType.DateRecorded = DateTime.Now;
                classType.RecordedBy = user.Id;
                _context.Add(classType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", classType.RecordedBy);
            return View(classType);
        }

        // GET: ClassTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classType = await _context.ClassTypes.FindAsync(id);
            if (classType == null)
            {
                return NotFound();
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", classType.RecordedBy);
            return View(classType);
        }

        // POST: ClassTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,RecordedBy,DateRecorded")] ClassType classType)
        {
            if (id != classType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassTypeExists(classType.Id))
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
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", classType.RecordedBy);
            return View(classType);
        }

        // GET: ClassTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classType = await _context.ClassTypes
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classType == null)
            {
                return NotFound();
            }

            return View(classType);
        }

        // POST: ClassTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classType = await _context.ClassTypes.FindAsync(id);
            _context.ClassTypes.Remove(classType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassTypeExists(int id)
        {
            return _context.ClassTypes.Any(e => e.Id == id);
        }
    }
}
