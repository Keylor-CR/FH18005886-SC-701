using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(TheModel model)
    {
        ViewBag.Valid = ModelState.IsValid;
        if (ViewBag.Valid)
        {
            var charArray = model.Phrase!.ToCharArray().Where(c => c != ' ').ToList();//https://stackoverflow.com/questions/5137514/string-replace-using-linq-in-c-sharp
            charArray.ForEach(c =>
            {
                if (!model.Counts!.ContainsKey(c))
                {
                    model.Counts[c] = 0;
                }
                model.Counts[c]++;
                model.Lower += c.ToString().ToLower();
                model.Upper += c.ToString().ToUpper();
            });
        }
        return View(model);
    }
}
