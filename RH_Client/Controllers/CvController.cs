using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH_Client.Models;
using System.Collections.Generic;

namespace RH_Client.Controllers;

public class CvController : Controller
{

    public IActionResult formulaireAjoutCv(string idbesoin)
    {
        Critere refs = new Critere();
        List<Critere> critere =  refs.getCritereByBesoin(null,int.Parse(idbesoin));


        // <id critere,critereoption rehetra amle critere>
        Dictionary<int, CritereOption[]> dico = new Dictionary<int, CritereOption[]>();
        for (int i = 0; i < critere.Count(); i++)
        {
            dico.Add(critere[i].Id,critere[i].getCritereOption(null));
        }

        
        ViewBag.idbesoin = idbesoin;
        ViewBag.allCritere = critere;
        ViewBag.dicoCritereOption = dico;
        
        return View("AjoutCv");
    }

    public IActionResult ajoutCv()
    {
    
        // Request.Form izy raha POST
        Critere refs = new Critere();
        List<Critere> critere =  refs.getCritereByBesoin(null,int.Parse(Request.Form["idbesoin"]));

        // info candidat mbola mila alaina
        string idbesoin = Request.Form["idbesoin"]; 
        int idcandidat = 1; //alaina anaty session
        Candidate c = this.GetCandidate(idbesoin,idcandidat);

        // list iray hoana critere iray
        // list iray mi contenir ny critere_option nosafidiana tamle anle critere iray
        List<int>[] list = new List<int>[critere.Count()];
        List<int> idCritereOption = new List<int>();
        for(int i=0;i<critere.Count();i++){
            list[i] = new List<int>();

            // Console.WriteLine("id critere :"+critere[i].Id+", idcriteres options nosafidiana("+Request.Form["critereOption-"+critere[i].Id].Count+") :");

            foreach (var item in Request.Form["critereOption-"+critere[i].Id])
            {
                // Console.WriteLine("----- :"+item);
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
