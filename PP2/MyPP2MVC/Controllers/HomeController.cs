using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyPP2MVC.Models;
using MyPP2MVC.Services;

namespace MyPP2MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new InputModel());
    }

    [HttpPost]    
    public IActionResult Index(InputModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        model.Results = CalculatorService.BuildResults(model.A, model.B);
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
