using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using chefsNdishes.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace chefsNdishes.Controllers;

//user controller
//user Models & login
// user views and folders
public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;

    private MyContext db;

    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }

    // ============================set this controler to root it now handles all root requests========================================

    [HttpGet("/dishes")]
    public IActionResult Index()
    {
        // one to many relationship/connection
        List<Dish> allDishes = db.Dishes.Include(p => p.Creator).ToList();
        return View("AllDishes", allDishes);
    }




    [HttpGet("dish/new")]
    public IActionResult NewDish()
    {

        ViewBag.allChefs = db.Chefs.ToList();


        return View("NewDish");
    }




    //=========================== post/ create method===================================

    [HttpPost("dish/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.allChefs = db.Chefs.ToList();
            return View("NewDish");
        }
        db.Dishes.Add(newDish);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    // =========================post method=====================================
    [HttpGet("dish/{dishId}")]
    public IActionResult ViewDish(int dishId)
    {
        Dish OneDish = db.Dishes.FirstOrDefault(a => a.DishId == dishId);

        if (OneDish == null)
        {
            return RedirectToAction("Index");
        }
        return View("ViewDish", OneDish);


    }

    // ==============================edit Method================================
    [HttpGet("dish/{dishId}/edit")]
    public IActionResult Edit(int dishId)
    {
        Dish? OneDish = db.Dishes.FirstOrDefault(a => a.DishId == dishId);
        // edit only possilble bt creator
        if (OneDish == null || OneDish.ChefId != HttpContext.Session.GetInt32("UUID"))
        {
            return RedirectToAction("Index");
        }
        return View("Edit", OneDish);
    }


    // =======================Update Method=================================
    [HttpPost("dish/{dishId}/update")]
    public IActionResult Update(Dish editDish, int dishId)
    {
        if (!ModelState.IsValid)
        {
            return Edit(dishId);
        }
        Dish? OneDish = db.Dishes.FirstOrDefault(a => a.DishId == dishId);
        // edit only possilble bt creator
        if (OneDish == null || OneDish.ChefId != HttpContext.Session.GetInt32("UUID"))
        {
            return RedirectToAction("Index");
        }
        OneDish.DishName = editDish.DishName;
        OneDish.Tastiness = editDish.Tastiness;
        OneDish.Calories = editDish.Calories;
        OneDish.UpdatedAt = DateTime.Now;
        db.Dishes.Update(OneDish);
        db.SaveChanges();
        return RedirectToAction("ViewDish", new { dishId = dishId });
    }

    // ==========================delete method========================================
    [HttpPost("dish/{dishId}/delete")]
    public IActionResult Delete(int dishId)
    {
        // olny lets creator delete
        Dish? OneDish = db.Dishes.FirstOrDefault(a => a.DishId == dishId);
        if (OneDish == null || OneDish.ChefId != HttpContext.Session.GetInt32("UUID"))
        {
            return RedirectToAction("Index");
        }
        db.Dishes.Remove(OneDish);
        db.SaveChanges();
        return RedirectToAction("Index");

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


