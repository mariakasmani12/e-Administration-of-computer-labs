﻿using e_Administration_of_computer_labs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_Administration_of_computer_labs.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register() => View();
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
           
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
               
                DepartmentId = null
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);

                TempData["Role"] = string.Join(", ", roles);

                if (roles.Contains("Admin"))
                    return RedirectToAction("UserList", "Admin");
                else if (roles.Contains("Instructor"))
                    return RedirectToAction("Instructor", "Dashboard"); // Dashboard/Instructor.cshtml
                else if (roles.Contains("HOD"))
                    return RedirectToAction("HOD", "Dashboard");
                else if (roles.Contains("TechnicalStaff"))
                    return RedirectToAction("TechnicalStaff", "Dashboard");

                // No valid role found
                return RedirectToAction("AccessDenied", "Dashboard");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
