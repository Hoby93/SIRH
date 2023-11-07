using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIRH.Models;

namespace SIRH.Controllers
{
    public class TrialContractController : Controller
    {
        public IActionResult Index()
        {
            Embauche[] embauches = new Embauche().GetEmbauchesWithoutContract(null);
            return View(embauches);
        }

        public IActionResult Form(int idEmbauche)
        {
            Embauche e = new Embauche();
            Company c = new Company();
            Salary s = new Salary();
            BddTitre[] avantages = null;
            e.Id = idEmbauche;
            TrialContract contratEssai = new TrialContract();
            contratEssai.GetInfoForContract(null, ref e, ref c, ref s, ref avantages);
            ViewBag.embauche = e;
            ViewBag.entreprise = c;
            ViewBag.salaire = s;
            ViewBag.avantages = avantages;
            return View();
        }

        public IActionResult Create(int[] avantage)
        {
            TrialContract contratEssai = new TrialContract();
            contratEssai.IdEmbauche = Int16.Parse(Request.Form["idembauche"]);
            contratEssai.TempsTravail = Double.Parse(Request.Form["tempsTravail"]);
            contratEssai.DateDebutContrat = DateTime.Parse(Request.Form["dateDebutContrat"]);
            contratEssai.DureeEssai = Double.Parse(Request.Form["dureeEssai"]);
            contratEssai.JoursTravailles = Double.Parse(Request.Form["joursTravailles"]);
            contratEssai.Create(null, avantage);
            return RedirectToAction("Index", "TrialContract");
        }
    }
}