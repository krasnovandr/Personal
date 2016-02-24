using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AudioNetwork.Controllers
{
    public class RecognitionController : Controller
    {
        // GET: Recognition
        public ActionResult ViewRecognition()
        {
            return View("ViewRecognition");
        }
    }
}