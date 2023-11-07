using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH_Client.Models;

namespace RH_Client.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Annonce()
    {
        Announcement[] annonces = new Announcement().GetAll(null);
        ViewBag.annonces = annonces;
        return View("Annonce");
    }

    public IActionResult InfoAnnonce(int idannonce)
    {
        Announcement annonce = new Announcement();
        annonce.Id = idannonce;
        annonce = annonce.Find(null);
        ViewBag.annonce = annonce;

        string errorMessage = TempData["ErrorMessage"] as string;
        if (!string.IsNullOrEmpty(errorMessage))
        {
            ViewBag.ErrorMessage = errorMessage;
        }

        return View("InfoAnnonce");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
