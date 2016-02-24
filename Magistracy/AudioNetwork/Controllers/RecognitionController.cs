using System.Web.Mvc;

namespace AudioNetwork.Web.Controllers
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