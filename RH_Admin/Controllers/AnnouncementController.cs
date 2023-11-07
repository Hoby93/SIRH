using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIRH.Models;

namespace SIRH.Controllers
{
    public class AnnouncementController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create(int id_besoin, DateTime date)
        {
            Besoin n = new Besoin();
            n.Id = id_besoin;
            Announcement a = new Announcement();
            a.Generate(null, n, date);
            return RedirectToAction("Index", "Besoin");
        }

        public IActionResult Liste()
        {
            Announcement[] annonces = new Announcement().GetAll(null);
            return View(annonces);
        }
    }
}