﻿using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
