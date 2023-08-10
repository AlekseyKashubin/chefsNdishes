//user controller DONE
//user Models & login DONE
//user views and folders
//Routues home, Regestier, login, logout


using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using chefsNdishes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace chefsNdishes.Controllers;


public class ChefController : Controller
{
    private readonly ILogger<ChefController> _logger;

    private MyContext db;

    public ChefController(ILogger<ChefController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        List<Chef> allChefs = db.Chefs.Include(x => x.DishesByChefs).ToList();

        return View("AllChefs", allChefs);
    }

[HttpGet("chef/all")]
    public IActionResult AllChefs()
    {
        return RedirectToAction("Index");
    }





    [HttpGet("chef/new")]
    public IActionResult NewChef()
    {
        return View("NewChef");
    }





    // ====================Chef create===============================

    [HttpPost("chef/create")]
    public IActionResult CreateChef(Chef newChef)
    {
        if (!ModelState.IsValid)
        {
            return View("NewChef");
        }
        db.Chefs.Add(newChef);
        db.SaveChanges();

        return RedirectToAction("Index");
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