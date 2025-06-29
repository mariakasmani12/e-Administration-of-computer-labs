using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_Administration_of_computer_labs.Models;

namespace e_Administration_of_computer_labs.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplaintsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Complaints
        public async Task<IActionResult> Index()
        {
            var complaints = _context.Complaints
                .Include(c => c.User)
                .Include(c => c.Lab)
                .Include(c => c.Equipment);

            return View(await complaints.ToListAsync());
        }

        // GET: Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var complaint = await _context.Complaints
                .Include(c => c.User)
                .Include(c => c.Lab)
                .Include(c => c.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (complaint == null) return NotFound();

            return View(complaint);
        }

        // GET: Complaints/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "Name");
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name");
            return View();
        }

        // POST: Complaints/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,DateSubmitted,Status,UserId,LabId,EquipmentId")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", complaint.UserId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "Name", complaint.LabId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", complaint.EquipmentId);
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null) return NotFound();

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", complaint.UserId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "Name", complaint.LabId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", complaint.EquipmentId);
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,DateSubmitted,Status,UserId,LabId,EquipmentId")] Complaint complaint)
        {
            if (id != complaint.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", complaint.UserId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "Name", complaint.LabId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", complaint.EquipmentId);
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var complaint = await _context.Complaints
                .Include(c => c.User)
                .Include(c => c.Lab)
                .Include(c => c.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (complaint == null) return NotFound();

            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.Id == id);
        }
    }
}
