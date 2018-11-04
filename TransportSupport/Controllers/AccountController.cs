﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityExample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityExample.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);

                if (loginResult.Succeeded)
                {
                    TempData["Message"] = "Zalogowano pomyślnie";

                    return RedirectToAction("Index", "Calendar");
                }
                else
                {
                    ModelState.AddModelError("", "Nie można zalogować. Sprawdź poprawność hasła i loginu");
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identity = new IdentityUser(model.Login) { Email = model.Email };

                var result = await _userManager.CreateAsync(identity, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);

                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            TempData["Message"] = "Zostałeś wylogowany pomyślnie";

            return RedirectToAction("Login", "Account");
        }
    }
}