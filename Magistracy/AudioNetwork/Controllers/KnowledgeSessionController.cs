using System.Collections.Generic;
using System.Web.Mvc;
using DataLayer.Models;
using Microsoft.AspNet.Identity;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace AudioNetwork.Web.Controllers
{
    public class KnowledgeSessionController : Controller
    {
        // GET: KnowledgeSession


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SessionView()
        {
            return View();
        }

        public ActionResult Round()
        {
            return View();
        }

        public ActionResult RoundLevelVote()
        {
            return View();
        }

        public ActionResult RoundWinnerVote()
        {
            return View();
        }


    }
}