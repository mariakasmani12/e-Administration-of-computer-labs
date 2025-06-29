using e_Administration_of_computer_labs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_Administration_of_computer_labs.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ✅ List of All Registered Users
        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .ToListAsync();

            return View(users);
        }

        // ✅ Show Form to Assign Role/Department
        public async Task<IActionResult> AssignRoleDepartment(string userId)
        {
            var user = await _userManager.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound();

            var viewModel = new AssignRoleDepartmentViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                SelectedRoleId = user.RoleId,
                SelectedDepartmentId = user.DepartmentId,
                Roles = await _context.Roles.ToListAsync(),
                Departments = await _context.Departments.ToListAsync()
            };

            return View(viewModel);
        }

        // ✅ Save Assigned Role & Department
        [HttpPost]
        public async Task<IActionResult> AssignRoleDepartment(AssignRoleDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();

            user.RoleId = model.SelectedRoleId;
            user.DepartmentId = model.SelectedDepartmentId;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Role and Department assigned successfully.";
                return RedirectToAction("UserList");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            model.Roles = await _context.Roles.ToListAsync();
            model.Departments = await _context.Departments.ToListAsync();
            return View(model);
        }

        // ✅ Optional: View details of a single user
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            return View(user);
        }
        public IActionResult Dashboard()
        {
            int totalInstructors = _context.Users.Count(u => u.Role.Name == "Instructor");

            ViewBag.TotalInstructors = totalInstructors;

            return View();
        }
        public IActionResult Index()
        {
             return View();
            }

    }
}
