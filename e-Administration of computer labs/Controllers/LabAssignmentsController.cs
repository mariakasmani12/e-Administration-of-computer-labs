using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_Administration_of_computer_labs.Models;

namespace e_Administration_of_computer_labs.Controllers
{
    public class LabAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LabAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LabAssignments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LabAssignments.Include(l => l.Instructor).Include(l => l.Lab);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LabAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labAssignment = await _context.LabAssignments
                .Include(l => l.Instructor)
                .Include(l => l.Lab)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labAssignment == null)
            {
                return NotFound();
            }

            return View(labAssignment);
        }

        // GET: LabAssignments/Create
        public IActionResult Create()
        {
            ViewData["InstructorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "Id");
            return View();
        }

        // POST: LabAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LabId,InstructorId,DayOfWeek,StartTime,EndTime")] LabAssignment labAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorId"] = new SelectList(_context.Users, "Id", "Id", labAssignment.InstructorId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "Id", labAssignment.LabId);
            return View(labAssignment);
        }

        // GET: LabAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labAssignment = await _context.LabAssignments.FindAsync(id);
            if (labAssignment == null)
            {
                return NotFound();
            }
            ViewData["InstructorId"] = new SelectList(_context.Users, "Id", "Id", labAssignment.InstructorId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "Id", labAssignment.LabId);
            return View(labAssignment);
        }

        // POST: LabAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LabId,InstructorId,DayOfWeek,StartTime,EndTime")] LabAssignment labAssignment)
        {
            if (id != labAssignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabAssignmentExists(labAssignment.Id))
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
            ViewData["InstructorId"] = new SelectList(_context.Users, "Id", "Id", labAssignment.InstructorId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "Id", labAssignment.LabId);
            return View(labAssignment);
        }

        // GET: LabAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labAssignment = await _context.LabAssignments
                .Include(l => l.Instructor)
                .Include(l => l.Lab)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labAssignment == null)
            {
                return NotFound();
            }

            return View(labAssignment);
        }

        // POST: LabAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labAssignment = await _context.LabAssignments.FindAsync(id);
            if (labAssignment != null)
            {
                _context.LabAssignments.Remove(labAssignment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabAssignmentExists(int id)
        {
            return _context.LabAssignments.Any(e => e.Id == id);
        }
    }
}
