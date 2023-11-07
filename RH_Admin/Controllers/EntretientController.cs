using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SIRH.Models;

namespace SIRH.Controllers;

public class EntretientController : Controller
{
    public IActionResult listeBesoin()
    {
        Besoin[] allBesoin = new Besoin().select(null).OfType<Besoin>().ToArray();

        ViewBag.all = allBesoin;

        return View("listeBesoin");
    }
    public IActionResult listeCandidat(int idbesoin)
    {
        double notemin = 10; // note minimun tam test voaray
        List<NCBesoin> ncb = new NoteCandidat().getAllCandidatBesoin(null,notemin,idbesoin);

        ViewBag.AllCandidat = ncb;

        return View("listeCandidat");
    }

    public IActionResult embaucher(int idbesoin,int idcandidat,int idposte,int idtypecontrat,double valeurbrute,double valeurnet,DateTime datedebut,DateTime datefin)
    {
        int idbesoincandidat = new BddObjet().getInteger($"select id from besoin_candidat where idbesoin = {idbesoin} and idcandidat = {idcandidat}", null);
        Embauche embauche = new Embauche(idbesoincandidat, idcandidat, idposte, idtypecontrat, DateTime.Now, 1);
        embauche.insert(null);

        // Console.WriteLine($"{datedebut} / {datefin}");

        int idembauche = new BddObjet().getInteger($"SELECT max(id) FROM embauche", null);

        embauche = new Embauche(idembauche);
        embauche.update($"id = {idembauche}", null);

        Salaire salaire = new Salaire(-1,idembauche,valeurbrute,valeurnet,datedebut,datefin);
        salaire.insert(null);

        return RedirectToAction("listeCandidat",new {idbesoin = idbesoin});
    }
}