using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIRH.Models;

namespace SIRH.Controllers
{
    public class EmployeController : Controller
    {
        public IActionResult Index()
        {
            Embauche[] em = new Embauche().GetAll(null);
            ViewBag.embauches = em;
            return View();
        }

        public IActionResult Absences()
        {
            Conge[] c = new Conge().GetDemandesConges(null);
            ViewBag.conges = c;
            return View();
        }

        public IActionResult Detail(int id)
        {
            Console.WriteLine(id);
            Object employe = new Employe().Init($"where id = {id}", null);
            Object[] faits = new Faits().select("", null);

            ViewBag.Employe = employe;
            ViewBag.Faits = faits;

            return View("Info");
        }

        public IActionResult AddFait(int idembauche, int count, int action, DateTime datefait)
        {
            if (count > 3)
            {
                BddObjet.ExecuteNonQuery($"update embauche set etat = 0 where id = {idembauche}", null);
            }
            Faits fait = new Faits(-1, idembauche, action, datefait);
            fait.insert(null);

            return Detail(1);
        }
    }
}