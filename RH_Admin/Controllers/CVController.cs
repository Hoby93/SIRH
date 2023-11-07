using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIRH.Models;

namespace SIRH.Controllers
{
    public class CVController : Controller
    {
        public IActionResult Index()
        {
            Candidate[] postulants = new Candidate[0];
            return View(postulants  );
        }

        public IActionResult GetCVCandidat(int[] id_critere_option, int[] option_choisie)
        {
            
            return View();
        }
    }
}