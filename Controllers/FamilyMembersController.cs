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
    public class FamilyMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FamilyMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FamilyMembers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FamilyMembers.Include(f => f.FamilyMemberType).Include(f => f.GenderType).Include(f => f.MaritalStatusType).Include(f => f.Student).Include(f => f.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FamilyMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyMember = await _context.FamilyMembers
                .Include(f => f.FamilyMemberType)
                .Include(f => f.GenderType)
                .Include(f => f.MaritalStatusType)
                .Include(f => f.Student)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (familyMember == null)
            {
                return NotFound();
            }

            return View(familyMember);
        }

        // GET: FamilyMembers/Create
        public IActionResult Create()
        {
            ViewData["FamilyMemberTypeId"] = new SelectList(_context.FamilyMemberTypes, "Id", "Name");
            ViewData["GenderTypeId"] = new SelectList(_context.GenderTypes, "Id", "Name");
            ViewData["MaritalStatusTypeId"] = new SelectList(_context.MaritalStatuses, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Firstname");
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: FamilyMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,GenderTypeId,Image,ImageName,FamilyMemberTypeId,MaritalStatusTypeId,Firstname,Middlename,Lastname,PhoneNumber,Email,Address,RecordedBy,DateRecorded")] FamilyMember familyMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(familyMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilyMemberTypeId"] = new SelectList(_context.FamilyMemberTypes, "Id", "Name", familyMember.FamilyMemberTypeId);
            ViewData["GenderTypeId"] = new SelectList(_context.GenderTypes, "Id", "Name", familyMember.GenderTypeId);
            ViewData["MaritalStatusTypeId"] = new SelectList(_context.MaritalStatuses, "Id", "Name", familyMember.MaritalStatusTypeId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Firstname", familyMember.StudentId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", familyMember.RecordedBy);
            return View(familyMember);
        }

        // GET: FamilyMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyMember = await _context.FamilyMembers.FindAsync(id);
            if (familyMember == null)
            {
                return NotFound();
            }
            ViewData["FamilyMemberTypeId"] = new SelectList(_context.FamilyMemberTypes, "Id", "Name", familyMember.FamilyMemberTypeId);
            ViewData["GenderTypeId"] = new SelectList(_context.GenderTypes, "Id", "Name", familyMember.GenderTypeId);
            ViewData["MaritalStatusTypeId"] = new SelectList(_context.MaritalStatuses, "Id", "Name", familyMember.MaritalStatusTypeId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Firstname", familyMember.StudentId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", familyMember.RecordedBy);
            return View(familyMember);
        }

        // POST: FamilyMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,GenderTypeId,Image,ImageName,FamilyMemberTypeId,MaritalStatusTypeId,Firstname,Middlename,Lastname,PhoneNumber,Email,Address,RecordedBy,DateRecorded")] FamilyMember familyMember)
        {
            if (id != familyMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(familyMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyMemberExists(familyMember.Id))
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
            ViewData["FamilyMemberTypeId"] = new SelectList(_context.FamilyMemberTypes, "Id", "Name", familyMember.FamilyMemberTypeId);
            ViewData["GenderTypeId"] = new SelectList(_context.GenderTypes, "Id", "Name", familyMember.GenderTypeId);
            ViewData["MaritalStatusTypeId"] = new SelectList(_context.MaritalStatuses, "Id", "Name", familyMember.MaritalStatusTypeId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Firstname", familyMember.StudentId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", familyMember.RecordedBy);
            return View(familyMember);
        }

        // GET: FamilyMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyMember = await _context.FamilyMembers
                .Include(f => f.FamilyMemberType)
                .Include(f => f.GenderType)
                .Include(f => f.MaritalStatusType)
                .Include(f => f.Student)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (familyMember == null)
            {
                return NotFound();
            }

            return View(familyMember);
        }

        // POST: FamilyMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var familyMember = await _context.FamilyMembers.FindAsync(id);
            _context.FamilyMembers.Remove(familyMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyMemberExists(int id)
        {
            return _context.FamilyMembers.Any(e => e.Id == id);
        }
    }
}
