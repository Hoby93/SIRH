using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH_Client.Models;
using System.Collections.Generic;

namespace RH_Client.Controllers;

public class CvCheckController : Controller
{

    public IActionResult voirResultat(string idbesoin,string idcandidat)
    {
        Besoin besoin = (Besoin) new Besoin().Select($"where id = {idbesoin}",null);
        Candidat candidat = (Candidat) new Candidat().Select($"where id = {idcandidat}",null);

        Boolean estValider = besoin.IsAdmis(candidat,null);

        ViewBag.flag = estValider;
        ViewBag.idposte = besoin.Idposte;
        ViewBag.idbesoin = besoin.Id;

        return View("Resultat");
    }

}