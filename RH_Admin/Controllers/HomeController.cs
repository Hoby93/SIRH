using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SIRH.Models;

namespace SIRH.Controllers;

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

    public IActionResult ListPost()
    {
        Object[] postes = new Poste().select(null);
        Object[] services = new BddTitre("services").select(null);

        ViewBag.Message = "Hello from ViewBag!";
        ViewBag.Postes = postes;
        ViewBag.Services = services;

        return View("ListPost");
    }

    public IActionResult InsertPost(int idService, string libelle, string description)
    {
        Poste poste = new Poste(-1, idService, libelle, description);
        poste.insert(null);

        Object[] postes = new Poste().select(null);
        Object[] services = new BddTitre("services").select(null);

        ViewBag.Message = $"{idService}/{libelle}/{description}";
        ViewBag.Postes = postes;
        ViewBag.Services = services;

        return View("ListPost");
    }

    public IActionResult UpdatePost(int id, int idService, string libelle, string description)
    {
        Poste poste = new Poste(id, idService, libelle, description);
        poste.update(null);

        Object[] postes = new Poste().select(null);
        Object[] services = new BddTitre("services").select(null);

        ViewBag.Message = $"{id}/{idService}/{libelle}/{description}";
        ViewBag.Postes = postes;
        ViewBag.Services = services;


        return View("ListPost");
    }

    public IActionResult deletePost(int idposte)
    {
        Poste poste = new Poste(idposte, 0, "", "");
        poste.delete(null);

        Object[] postes = new Poste().select(null);
        Object[] services = new BddTitre("services").select(null);

        ViewBag.Message = $"";
        ViewBag.Postes = postes;
        ViewBag.Services = services;

        return View("ListPost");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
