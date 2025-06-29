using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_Administration_of_computer_labs.Models;

namespace e_Administration_of_computer_labs.Controllers
{
    public class ExtraLabRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExtraLabRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExtraLabRequests
        public async Task<IActionResult> Index()
        {
            var requests = await _context.ExtraLabRequests
                .Include(r => r.Lab)
                .Include(r => r.Department)
                .Include(r => r.User)
                .ToListAsync();

            return View(requests);
        }

        // GET: ExtraLabRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var request = await _context.ExtraLabRequests
                .Include(r => r.Lab)
                .Include(r => r.Department)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (request == null) return NotFound();

            return View(request);
        }

        // POST: ExtraLabRequests/Approve/5
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var request = await _context.ExtraLabRequests.FindAsync(id);
            if (request == null) return NotFound();

            request.Status = "Approved";
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: ExtraLabRequests/Reject/5
        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var request = await _context.ExtraLabRequests.FindAsync(id);
            if (request == null) return NotFound();

            request.Status = "Rejected";
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
