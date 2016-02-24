using System.Web.Mvc;
using AudioNetwork.Services;
using Microsoft.AspNet.Identity;

namespace AudioNetwork.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public ActionResult ViewStatistics()
        {
            return View("Statistics");
        }
    

        public JsonResult GetMyFavoriteSongs()
        {
            var userId = User.Identity.GetUserId();
            return Json(_statisticsService.GetFavouriteSongs(userId), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetFavoriteSongs()
        {
            return Json(_statisticsService.GetFavouriteSongs(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetLastListenedSongs()
        {
            var userId = User.Identity.GetUserId();
            return Json(_statisticsService.GetLastListenedSongs(userId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLastAdded()
        {
            return Json(_statisticsService.GetLastAdded(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetChartData(string songId)
        {
            return Json(_statisticsService.GetChartData(songId), JsonRequestBehavior.AllowGet);
        }
    }
}