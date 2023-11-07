using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SIRH.Models;
using Npgsql;

namespace SIRH.Controllers;

public class ServicesController : Controller
{
  
    public IActionResult listeService()
    {
        Services serv = new Services();
        Services[] allServices = serv.getAllServices(null);
        return View("Liste",allServices);
    }

    public IActionResult ajoutService(string libelle)
    {
        try
        {
            Services service = new Services(libelle);
            service.insert(null);
            return RedirectToAction("listeService");
        }
        catch (System.Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            Services serv = new Services();
            Services[] allServices = serv.getAllServices(null);
            return View("Liste",allServices);
        }
    }

    public IActionResult supprimerService(int idservice)
    {
        try
        {
            Services supprimer = new Services(idservice);
            supprimer.supprimerService(null);
            return RedirectToAction("listeService");
        }
        catch (System.Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            Services serv = new Services();
            Services[] allServices = serv.getAllServices(null);
            return View("Liste",allServices);
        }
    }

    public IActionResult prepaModifierService(int idservice)
    {
        Services serv = new Services();
        Services serviceToUpdate = serv.getServiceById(null,idservice);
        return View("Update",serviceToUpdate);
    }

    public IActionResult modifierService(int idservice,string libelle)
    {
        try
        {
            Services serv = new Services(idservice,libelle);
            serv.update(null);
            return RedirectToAction("listeService");
        }
        catch (System.Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            Services serv = new Services();
            Services serviceToUpdate = serv.getServiceById(null,idservice);
            return View("Update",serviceToUpdate);
        }
        
    }


}
