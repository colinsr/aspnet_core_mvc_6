﻿using Microsoft.AspNet.Mvc;

namespace TheWorld_V2.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips","App");
            }

            return View();
        }
    }
}