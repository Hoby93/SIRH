using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH_Client.Models;

namespace RH_Client.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("Login");
    }

    public IActionResult Create()
    {
        return View("AddCandidat");
    }

    public IActionResult Check(string email, string mdp)
    {
        // Accéder à la session
        var session = HttpContext.Session;

        Object[] candidats = new Candidate().select(null);
        Boolean isexist = false;

        foreach (Candidate candidat in candidats)
        {
            if (candidat.Email.Equals(email))
            {
                session.SetString("username", candidat.Prenom);
                session.SetInt32("userid", candidat.Id);
                Console.WriteLine($"User: {candidat.Prenom}");
                isexist = true;
            }
        }

        if (!isexist)
        {
            ViewBag.Exception = "Erreur, Echec de connexion";
            return View("Login");
        }

        return RedirectToAction("Index", "Home");
    }

    public IActionResult AddCandidat(string nom, string prenom, DateTime dtn, int genre, string tel, string email, string adresse)
    {
        Candidate candidat = new Candidate(-1, nom, prenom, genre, dtn, tel, email, adresse);
        candidat.insert(null);

        return View("Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
