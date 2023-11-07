using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SIRH.Models;

namespace SIRH.Controllers;

public class BesoinController : Controller
{
    private readonly ILogger<BesoinController> _logger;

    public BesoinController(ILogger<BesoinController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Object[] postes = new Poste().select(null);
        Object[] types = new BddTitre("typecontrat").select(null);
        Object[] criteres = new Critere().select(null);

        ViewBag.Message = "Hello from ViewBag!";
        ViewBag.Postes = postes;
        ViewBag.Types = types;
        ViewBag.Criteres = criteres;

        return View("Ajout");
    }

    public IActionResult Insert(int idposte, int idtype, string description, double vh, [FromForm] int[] idcriteres, [FromForm] int[] isset, [FromForm] int[] coefficients, [FromForm] int[] notes)
    {
        Object[] postes = new Poste().select(null);
        Object[] types = new BddTitre("typecontrat").select(null);
        Object[] criteres = new Critere().select(null);


        ViewBag.Message = $"{idposte}, {idtype}, {description}, {vh}";
        ViewBag.Postes = postes;
        ViewBag.Types = types;
        ViewBag.Criteres = criteres;

        // Console.WriteLine();

        Besoin besoin = new Besoin(-1, idposte, idtype, description, vh);
        besoin.insert(null);

        int idbesoin = besoin.getInteger("select max(id) from besoin", null);
        Console.WriteLine("MAX(idbesoin)" + idbesoin);

        int x = 0;
        int i = 0;
        foreach (Critere critere in criteres)
        {
            if (x >= coefficients.Length || i >= notes.Length) { break; }
            if (critere.Id == idcriteres[x])
            {
                BesoinCritere besoinCritere = new BesoinCritere(-1, idbesoin, critere.Id, coefficients[x]);
                besoinCritere.insert(null);

                // Console.WriteLine($"-----------   {critere.Libelle} ({coefficients[x]})   -----------");
                if (isset[x] == 1)
                {
                    int idbesoincritere = besoin.getInteger($"select id from besoin_critere where idbesoin = {idbesoin} and idcritere = {critere.Id}", null);
                    foreach (BddTitre option in critere.Options())
                    {
                        CritereOptionNote optionNote = new CritereOptionNote(-1, idbesoincritere, option.Id, notes[i]);
                        optionNote.insert(null);
                        i++;
                    }
                }
                else
                {
                    i += critere.Options().Length;
                }
                x++;
            }
            else
            {
                i += critere.Options().Length;
            }
        }

        return RedirectToAction("Create", "Announcement", new { id_besoin = idbesoin, date = DateTime.Now });
    }

    public IActionResult InsertTest()
    {
        return View("AjoutTest");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult InsertBesoin(int idService, string libelle, string description)
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
