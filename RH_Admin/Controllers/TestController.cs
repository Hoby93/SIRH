using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIRH.Models;

namespace Namespace
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ajout(string[] libelle, int[] point, int[] verite, string[] reponses, int[] nombrePropositions)
        {
            Console.WriteLine(libelle.Length + " " + point.Length + " " + verite.Length + " " + reponses.Length + " " + nombrePropositions.Length);
            Questions[] questions = new Questions[libelle.Length];
            //Console.WriteLine(Request.Form["verite"]);
            //Console.WriteLine(verite.Length);
            int k = 0;
            int l = 0;
            for (int i = 0; i < libelle.Length; i++)
            {
                questions[i] = new Questions();
                questions[i].Question = libelle[i];
                questions[i].Points = point[i];
                Proposal[] propositions = new Proposal[nombrePropositions[i]];
                for (int j = 0; j < nombrePropositions[i]; j++)
                {
                    propositions[j] = new Proposal();
                    propositions[j].Etat = 0;
                    Console.WriteLine(reponses[k]);

                    propositions[j].Libelle = reponses[k];
                    if (l < verite.Length - 1)
                    {
                        if (verite[l + 1] == 1)
                        {
                            //Console.WriteLine(reponses[k]);
                            propositions[j].Etat = 1;
                            l++;
                        }
                    }
                    l++;
                    k++;
                }
                questions[i].Propositions = propositions;
            }
            Test t = new Test(questions);
            t.Create(null);
            return RedirectToAction("Index", "Besoin");
        }
    }
}