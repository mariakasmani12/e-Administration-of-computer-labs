using e_Administration_of_computer_labs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace e_Administration_of_computer_labs.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InstructorController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Dashboard
        public async Task<ActionResult> Dashboard()
        {
            var instructorId = User.Identity.Name; // or your own logic to get instructor's ID

            var assignedLabs = _context.LabAssignments
                .Where(x => x.InstructorId == instructorId)
                .ToList();

            var recentComplaints = _context.Complaints
                .Include(c => c.User) // if you want user details
                .Where(x => x.UserId == instructorId)
                .OrderByDescending(x => x.DateSubmitted)

                .Take(5)
                .ToList();


            var model = new InstructorDashboardViewModel
            {
                AssignedLabs = assignedLabs,
                RecentComplaints = recentComplaints
            };

            return View(model);
        }

        // GET: Submit Lab Report
        public IActionResult SubmitLabReport()
        {
            ViewBag.Labs = new SelectList(_context.Labs.ToList(), "Id", "Name");
            return View();
        }

        // POST: Submit Lab Report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitLabReport(LabReport report)
        {
            report.InstructorId = _userManager.GetUserId(User);
            report.SubmittedAt = DateTime.Now;

            _context.LabReports.Add(report);
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }

        // (Optional) GET: View My Reports
        public async Task<IActionResult> MyReports()
        {
            var instructorId = _userManager.GetUserId(User);
            var reports = await _context.LabReports
                .Include(r => r.Lab)
                .Where(r => r.InstructorId == instructorId)
                .OrderByDescending(r => r.SubmittedAt)
                .ToListAsync();

            return View(reports);
        }
    }
}
