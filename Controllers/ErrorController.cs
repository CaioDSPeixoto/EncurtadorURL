﻿using Microsoft.AspNetCore.Mvc;

namespace EncurtadorURL.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View("NotFound");
        }

        public IActionResult Payment()
        {
            return View("Payment");
        }
    }
}
