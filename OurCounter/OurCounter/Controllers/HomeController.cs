using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OurCounter.Models;

namespace OurCounter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var images = Directory.EnumerateFiles(Server.MapPath("~/Content/Images"))
                              .Select(Path.GetFileName);

            var imageModel = new ImageModel()
            {
                Images = images
            };
            return View(imageModel);
        }

        [OutputCache(Duration = 6000, VaryByParam = "imageName")]
        public ActionResult GetImageFull(string imageName)
        {
            byte[] image = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Images/" + imageName));
            return File(image, "image/jpeg");
        }

        [OutputCache(Duration = 6000, VaryByParam = "imageName")]
        public ActionResult GetImage(string imageName)
        {
            byte[] image = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Images/" + imageName));
            image = ResizeImage(image, 100, 100);
            return File(image, "image/jpeg");
        }
        public byte[] ResizeImage(byte[] data, int widthToResize, int heightToResize)
        {
            int maxwidth = widthToResize;
            int maxheight = heightToResize;
            try
            {
                var ic = new ImageConverter();
                var img = (Image)(ic.ConvertFrom(data)); //original size
                if (img != null && img.Width > maxwidth | img.Height > maxheight) //resize if it is too big
                {
                    var bitmap = new Bitmap(maxwidth, maxheight);
                    using (var graphics = Graphics.FromImage(bitmap))
                        graphics.DrawImage(img, 0, 0, maxwidth, maxheight);


                    data = (byte[])ic.ConvertTo(bitmap, typeof(byte[]));
                    return data;
                }
            }
            catch (Exception)
            {
                
                return new byte[]{};
            }
            //convert to full size image

            return new byte[] {};
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}