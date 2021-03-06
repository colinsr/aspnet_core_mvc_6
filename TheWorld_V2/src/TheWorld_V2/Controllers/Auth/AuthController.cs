﻿using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using TheWorld_V2.Models;
using TheWorld_V2.ViewModels;

namespace TheWorld_V2.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly SignInManager<WorldUser> _signInManager;

        public AuthController(SignInManager<WorldUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public ActionResult Login()
        {
            //if (User.Identity.IsAuthenticated)
            //    return RedirectToAction("Trips", "App");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager
                    .PasswordSignInAsync(vm.UserName, vm.Password,true,false);

                if (signInResult.Succeeded)
                    if (string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectToAction("Trips", "App");
                    else
                        return Redirect(returnUrl);

                ModelState.AddModelError("", "Username or password incorrect");
            }

            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }
    }
}