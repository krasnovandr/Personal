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

        public ActionResult SessionTreeView()
        {
            return View();
        }

        public ActionResult NodeStructureSuggestion()
        {
            return View();
        }

        public ActionResult NodeStructureSuggestionWait()
        {
            return View();
        }

        public ActionResult NodeStructureSuggestionVote()
        {
            return View();
        }

        public ActionResult RoundWinnerVote()
        {
            return View();
        }


        public ActionResult SuggestionModal()
        {
            return View();
        }


    }
}