using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SIRH.Models;

namespace SIRH.Controllers;

public class CongeController : Controller
{
    public IActionResult listeDemande(){

        AffDemandeConge[] lesDemandes = DemandeConge.getAllDemandeConge(null);

        ViewBag.demande = lesDemandes;

        if (TempData.ContainsKey("ErrorMessage"))
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        }

        return View();
    }

    public IActionResult accepterDemande(int iddemande){
        /*
            verifier si un conge est deja en cours ou entre les deux dates pour
            l'employe en question
        */
        DemandeConge ancien = (DemandeConge) new DemandeConge().select($"WHERE id = {iddemande}",null)[0];

        if(ancien.estPossible(null) == false){
            TempData["ErrorMessage"] = "Efa misy en cours nenio olona io";
        } else {
            DemandeConge nouveau = ancien;
            nouveau.Etat = 2;

            ReelConge rc = new ReelConge(-1,ancien.Idembauche,ancien.Idtypeconge,ancien.Debutconge,null);
            
            rc.insert(null);
            nouveau.update(null);
        }

        return RedirectToAction("listeDemande","Conge");
    }

    public IActionResult refuserDemande(int iddemande){
        DemandeConge ancien = (DemandeConge) new DemandeConge().select($"WHERE id = {iddemande}",null)[0];
        DemandeConge nouveau = ancien;
        nouveau.Etat = -2;
        nouveau.update(null);

        return RedirectToAction("listeDemande","Conge");
    }

    public IActionResult finirConge(){

        AffReelConge[] reelConge = ReelConge.getAllCongeEnCours(null);

        ViewBag.reel = reelConge;
        
        if (TempData.ContainsKey("ErrorMessage"))
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        }

        
        return View();
    }

    public IActionResult confirmerFinirConge(int idreelconge,DateTime datefin){

        if(datefin == DateTime.MinValue){
            datefin = DateTime.Now;
        }

        Console.WriteLine($"id = {idreelconge} date = {datefin}");

        ReelConge ancien = (ReelConge) new ReelConge().select($"WHERE id = {idreelconge}",null)[0];
        ReelConge nouveau = ancien;
        nouveau.Finconge = datefin;

        if(nouveau.Debutconge > nouveau.Finconge){
            TempData["ErrorMessage"] = "lasa fin no alohan'ny debut";
        } else {
            nouveau.update(null);
        }

        return RedirectToAction("finirConge","Conge");
    }
}