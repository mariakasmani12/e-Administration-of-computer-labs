using e_Administration_of_computer_labs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace e_Administration_of_computer_labs.Controllers
{
    [Authorize(Roles = "HOD")]
    public class HODController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HODController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var hod = await _userManager.GetUserAsync(User);
            var deptId = hod.DepartmentId;

            var complaints = await _context.Complaints
                .Include(c => c.User)
                .Include(c => c.Lab)
                .Where(c => c.User.DepartmentId == deptId)
                .OrderByDescending(c => c.DateSubmitted)
                .ToListAsync();

            var labRequests = await _context.ExtraLabRequests
                .Include(r => r.Lab)
                .Where(r => r.HODId == hod.Id)
                .OrderByDescending(r => r.RequestDate)
                .ToListAsync();

            var model = new HODDashboardViewModel
            {
                DepartmentComplaints = complaints,
                MyLabRequests = labRequests
            };

            return View(model);
        }

        public IActionResult RequestExtraLab()
        {
            ViewBag.Labs = _context.Labs.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestExtraLab(ExtraLabRequest request)
        {
            var hod = await _userManager.GetUserAsync(User);

            // Validation
            if (request.StartTime >= request.EndTime)
            {
                ModelState.AddModelError("", "Start time must be before End time.");
                ViewBag.Labs = _context.Labs.ToList();
                return View(request);
            }

            // Assigning system values
            request.HODId = hod.Id;
            request.DepartmentId = hod.DepartmentId ?? 0;
            request.Status = "Pending";

            _context.ExtraLabRequests.Add(request);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Your extra lab request has been submitted successfully.";
            return RedirectToAction("RequestExtraLab");
        }

    }
}

