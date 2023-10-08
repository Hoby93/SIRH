using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using clientGRH.Models;
using System.Collections.Generic;

namespace clientGRH.Controllers;

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

    public IActionResult test()
    {
        // info candidat mbola mila alaina
        /* 
            tsy maitsy natao samyhafa ny name an'ireo input critere option
            ireo ao amle vue io satria lasa tsy mety ilay checkbox sy radio raha hoe mitovy
            name daholo leizy de navahako par id critere ilay critere option reo
        */
        // Request.Form izy raha POST
        Critere refs = new Critere();
        List<Critere> critere =  refs.getCritereByBesoin(null,int.Parse(Request.Query["idbesoin"]));

        // list iray hoana critere iray
        // list iray mi contenir ny critere_option nosafidiana tamle anle critere iray
        List<int>[] list = new List<int>[critere.Count()];
        for(int i=0;i<critere.Count();i++){
            list[i] = new List<int>();

            Console.WriteLine("id critere :"+critere[i].Id+", idcriteres options nosafidiana("+Request.Query["critereOption-"+critere[i].Id].Count+") :");

            foreach (var item in Request.Query["critereOption-"+critere[i].Id])
            {
                Console.WriteLine("----- :"+item);
                list[i].Add(int.Parse(item));
            }
            
        }


        return View("Yes");
    }

    
}
