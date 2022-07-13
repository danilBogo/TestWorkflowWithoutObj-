using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Homework7.Models;

namespace Homework7.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult UserProfile()
    {
        return View();
    }

    [HttpPost]
    public IActionResult UserProfile(UserProfile profile)
    {
        return View(profile);
    }
}