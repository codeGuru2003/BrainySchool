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
    public class FacultiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacultiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Faculties
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Faculties.Include(f => f.FacultyType).Include(f => f.FacultyUser).Include(f => f.GenderType).Include(f => f.MaritalStatusType).Include(f => f.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Faculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties
                .Include(f => f.FacultyType)
                .Include(f => f.FacultyUser)
                .Include(f => f.GenderType)
                .Include(f => f.MaritalStatusType)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // GET: Faculties/Create
        public IActionResult Create()
        {
            ViewData["FacultyTypeId"] = new SelectList(_context.FacultyTypes, "Id", "Name");
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["GenderTypeId"] = new SelectList(_context.GenderTypes, "Id", "Name");
            ViewData["MaritalStatusTypeId"] = new SelectList(_context.MaritalStatuses, "Id", "Name");
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FacultyTypeId,ApplicationUserId,GenderTypeId,Image,ImageName,MaritalStatusTypeId,Firstname,Middlename,Lastname,PhoneNumber,Email,Address,RecordedBy,DateRecorded")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyTypeId"] = new SelectList(_context.FacultyTypes, "Id", "Name", faculty.FacultyTypeId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", faculty.ApplicationUserId);
            ViewData["GenderTypeId"] = new SelectList(_context.GenderTypes, "Id", "Name", faculty.GenderTypeId);
            ViewData["MaritalStatusTypeId"] = new SelectList(_context.MaritalStatuses, "Id", "Name", faculty.MaritalStatusTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", faculty.RecordedBy);
            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }
            ViewData["FacultyTypeId"] = new SelectList(_context.FacultyTypes, "Id", "Name", faculty.FacultyTypeId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", faculty.ApplicationUserId);
            ViewData["GenderTypeId"] = new SelectList(_context.GenderTypes, "Id", "Name", faculty.GenderTypeId);
            ViewData["MaritalStatusTypeId"] = new SelectList(_context.MaritalStatuses, "Id", "Name", faculty.MaritalStatusTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", faculty.RecordedBy);
            return View(faculty);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FacultyTypeId,ApplicationUserId,GenderTypeId,Image,ImageName,MaritalStatusTypeId,Firstname,Middlename,Lastname,PhoneNumber,Email,Address,RecordedBy,DateRecorded")] Faculty faculty)
        {
            if (id != faculty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faculty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(faculty.Id))
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
            ViewData["FacultyTypeId"] = new SelectList(_context.FacultyTypes, "Id", "Name", faculty.FacultyTypeId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", faculty.ApplicationUserId);
            ViewData["GenderTypeId"] = new SelectList(_context.GenderTypes, "Id", "Name", faculty.GenderTypeId);
            ViewData["MaritalStatusTypeId"] = new SelectList(_context.MaritalStatuses, "Id", "Name", faculty.MaritalStatusTypeId);
            ViewData["RecordedBy"] = new SelectList(_context.Users, "Id", "Id", faculty.RecordedBy);
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties
                .Include(f => f.FacultyType)
                .Include(f => f.FacultyUser)
                .Include(f => f.GenderType)
                .Include(f => f.MaritalStatusType)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyExists(int id)
        {
            return _context.Faculties.Any(e => e.Id == id);
        }
    }
}
