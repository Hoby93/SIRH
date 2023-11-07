using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH_Client.Models;
using System.Collections.Generic;

namespace RH_Client.Controllers;

public class CvController : Controller
{

    public IActionResult formulaireAjoutCv(string idbesoin,int idannonce)
    {
        Critere refs = new Critere();
        List<Critere> critere =  refs.getCritereByBesoin(null,int.Parse(idbesoin));

        if(HttpContext.Session.GetInt32("userid") == null){
            TempData["ErrorMessage"] = "Vous devez vous connecter";
            return RedirectToAction("InfoAnnonce", "Home", new {idannonce = idannonce});
        }
        int idcandidat = (int) HttpContext.Session.GetInt32("userid");

        // verifier si a deja postuler
        Object[] bc = new BesoinCandidat().select($"WHERE idbesoin = {idbesoin} AND idcandidat = {idcandidat}",null);
        if(bc.Length > 0)
        {
            TempData["ErrorMessage"] = "Vous avez deja postule";
            return RedirectToAction("InfoAnnonce", "Home", new {idannonce = idannonce});
        }


        // <id critere,critereoption rehetra amle critere>
        Dictionary<int, CritereOption[]> dico = new Dictionary<int, CritereOption[]>();
        for (int i = 0; i < critere.Count(); i++)
        {
            dico.Add(critere[i].Id,critere[i].getCritereOption(null));
        }

        
        ViewBag.idbesoin = idbesoin;
        ViewBag.allCritere = critere;
        ViewBag.dicoCritereOption = dico;
        ViewBag.idcandidat = idcandidat;
        
        return View("AjoutCv");
    }

    public IActionResult ajoutCv()
    {
        
        Critere refs = new Critere();
        List<Critere> critere =  refs.getCritereByBesoin(null,int.Parse(Request.Form["idbesoin"]));

        string idbesoin = Request.Form["idbesoin"]; 
        int idcandidat = int.Parse(Request.Form["idcandidat"]);
        Candidate c = this.GetCandidate(idbesoin,idcandidat);

        // list iray hoana critere iray
        // list iray mi contenir ny critere_option nosafidiana tamle anle critere iray
        List<int>[] list = new List<int>[critere.Count()];
        List<int> idCritereOption = new List<int>();
        for(int i=0;i<critere.Count();i++){
            list[i] = new List<int>();

            foreach (var item in Request.Form["critereOption-"+critere[i].Id])
            {
                list[i].Add(int.Parse(item));
                idCritereOption.Add(int.Parse(item));
            }
            
        }
        c.Postuler(null, int.Parse(Request.Form["idbesoin"]), idCritereOption.ToArray());
        return RedirectToAction("voirResultat", "CvCheck",new{idbesoin = idbesoin,idcandidat = idcandidat});
    }

    public Candidate GetCandidate(string idbesoin,int idcandidat)
    {
        Candidate c = new Candidate(idcandidat);
        return c;
    }    
}
