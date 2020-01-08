﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Localication_GlobalizationApp.Models;

using Microsoft.Extensions.Localization;
using Localication_GlobalizationApp.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace Localication_GlobalizationApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewData["msg"] = _localizer["Your Application Description Page"];
            return View();
        }


        [HttpGet]
       public IActionResult Contactus()
        {
            ContactViewModel model = new ContactViewModel();
            return View(model);
        }


        [HttpPost]
        public IActionResult Contactus(ContactViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            ViewData["Result"] = _localizer["Success"];
            return View(model);

        }

        public IActionResult Privacy()
        {

            ViewData["msg"] = _localizer["Your Application Description Page"];
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
