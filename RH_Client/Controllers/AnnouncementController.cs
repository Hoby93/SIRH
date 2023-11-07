using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RH_Client.Models;

namespace RH_Client.Controllers
{
    public class AnnouncementController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Liste()
        {
            Announcement[] annonces = new Announcement().GetAll(null);
            return View(annonces);
        }
    }
}