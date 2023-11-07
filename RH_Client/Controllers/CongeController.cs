using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH_Client.Models;

namespace RH_Client.Controllers;

public class CongeController : Controller
{
    public IActionResult demanderConge()
    {

        if (HttpContext.Session.GetInt32("userid") == null)
        {
            TempData["ErrorMessage"] = "Vous devez vous connecter";

            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            return View("erreurConfidentielle");
        }

        int idcandidat = (int)HttpContext.Session.GetInt32("userid");

        int idembauche = new BddObjet().getInteger($"SELECT id from embauche WHERE idcandidat = {idcandidat}", null);

        if (Embauche.estEmploye(null, idcandidat) == false)
        {
            TempData["ErrorMessage"] = "Cette page est reserve au employe";

            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            return View("erreurConfidentielle");
        }


        TypeConge[] tc = new TypeConge().select(null).OfType<TypeConge>().ToArray();

        ViewBag.typeConge = tc;
        ViewBag.idembauche = idembauche;

        Console.WriteLine(TempData["ErrorMessage"]);
        if (TempData.ContainsKey("ErrorMessage"))
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        }

        return View("demanderConge");
    }

    public IActionResult insererDemande(int idembauche, int idtypeconge, DateTime debut, DateTime fin)
    {
        try
        {
            DemandeConge dc = new DemandeConge(-1, idembauche, idtypeconge, debut, fin, 0);
            dc.insererDemande(null);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            // Console.WriteLine("eto izy "+ex.Message);
        }

        return RedirectToAction("demanderConge", "Conge");
    }
}
