using Homework7.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework7.Controllers;

public class TestController : Controller
{
    [HttpGet]
    public IActionResult TestModel()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult TestModel(TestModel model)
    {
        return View(model);
    }
}