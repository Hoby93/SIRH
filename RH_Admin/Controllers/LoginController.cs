using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SIRH.Models;

namespace SIRH.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        ViewBag.Title = "Login";

        return View("Connexion");
    }

    public IActionResult Check(string email, string mdp)
    {
        // Accéder à la session
        var session = HttpContext.Session;

        Object[] employes = new Employe().select(null);
        Boolean isexist = false;

        foreach(Employe employe in employes) {
            if(employe.Email.Equals(email)) {
                session.SetString("username", employe.Prenom);
                Console.WriteLine($"User: {employe.Prenom}");
                isexist = true;
            }
        }

        if(!isexist) {
            ViewBag.Exception = "Erreur, Echec de connexion";
            return View("Connexion");
        }

        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
