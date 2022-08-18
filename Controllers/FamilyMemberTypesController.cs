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
    public class FamilyMemberTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FamilyMemberTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: FamilyMemberTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FamilyMemberTypes.Include(f => f.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FamilyMemberTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyMemberType = await _context.FamilyMemberTypes
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (familyMemberType == null)
            {
                return NotFound();
            }

            return View(familyMemberType);
        }

        // GET: FamilyMemberTypes/Create
        public IActionResult Create()
        {
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: FamilyMemberTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RecordedBy,DateRecorded")] FamilyMemberType familyMemberType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                familyMemberType.DateRecorded = DateTime.Now;
                familyMemberType.RecordedBy = user.Id;
                _context.Add(familyMemberType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", familyMemberType.RecordedBy);
            return View(familyMemberType);
        }

        // GET: FamilyMemberTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyMemberType = await _context.FamilyMemberTypes.FindAsync(id);
            if (familyMemberType == null)
            {
                return NotFound();
            }
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", familyMemberType.RecordedBy);
            return View(familyMemberType);
        }

        // POST: FamilyMemberTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,RecordedBy,DateRecorded")] FamilyMemberType familyMemberType)
        {
            if (id != familyMemberType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(familyMemberType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyMemberTypeExists(familyMemberType.Id))
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
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", familyMemberType.RecordedBy);
            return View(familyMemberType);
        }

        // GET: FamilyMemberTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyMemberType = await _context.FamilyMemberTypes
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (familyMemberType == null)
            {
                return NotFound();
            }

            return View(familyMemberType);
        }

        // POST: FamilyMemberTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var familyMemberType = await _context.FamilyMemberTypes.FindAsync(id);
            _context.FamilyMemberTypes.Remove(familyMemberType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyMemberTypeExists(int id)
        {
            return _context.FamilyMemberTypes.Any(e => e.Id == id);
        }
    }
}
