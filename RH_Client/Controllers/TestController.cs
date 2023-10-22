using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RH_Client.Models;

namespace RH_Client.Controllers;

public class TestController : Controller
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string idposte,string idbesoin)
    {
        Object[] questions = new Questions().select($"where idposte = {idposte}", null);

        ViewBag.Questions = questions;
        ViewBag.idcandidat = 1;
        ViewBag.idbesoin = idbesoin;
        ViewBag.idposte = idposte;

        return View("Test");
    }

    public IActionResult Answers(int idbesoin, int idcandidat,int idposte ,[FromForm] int[] idpropositions)
    {
        //ze idbesoincandidat voalohany anle candidat amle besoin io ao no azo amnito donc tsy afaka mi postule imbedebebe fa ze voloahany ihany no hita(max ra atao hita foana)
        int idbesoincandidat = new BddObjet().getInteger($"select id from besoin_candidat where idbesoin = {idbesoin} and idcandidat = {idcandidat}", null);
        Object[] questions = new Questions().select($"where idposte = {idposte}", null);

        Console.WriteLine("idbesoin"+idbesoin);
        Console.WriteLine("idcandidat"+idcandidat);

        ViewBag.Questions = questions;

        foreach(int idproposition in idpropositions) {
            CandidatReponse candidatReponse = new CandidatReponse(-1, idbesoincandidat, idproposition);
            candidatReponse.insert(null);
            //Console.WriteLine($"[{idcandidat}]: {id}");
        }

        double note = new Questions().noteTest(idposte,idbesoincandidat);

        ViewBag.note = note;

        return View("Note");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // 19/10/23
}
