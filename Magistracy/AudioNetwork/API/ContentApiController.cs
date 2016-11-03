using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;

namespace AudioNetwork.Web.API
{
    public class ImagePathModel
    {
        public string RelativePath { get; set; }
        public string FullPath { get; set; }
    }
    public class ContentApiController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage UploadImage()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string baseUrl = Url.Request.RequestUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var postedFile = httpRequest.Files[0];
                var relativePath = "/Content/ResourceImages/" + Guid.NewGuid() +
                                Path.GetExtension(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath(relativePath);

                if (postedFile.ContentLength > 0)
                {
                    postedFile.SaveAs(filePath);
                    var imagePaths = new ImagePathModel
                    {
                        FullPath = baseUrl + relativePath,
                        RelativePath = relativePath
                    };
                
                    var response = Request.CreateResponse(HttpStatusCode.OK, imagePaths);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                }
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet]
        public HttpResponseMessage Recognisetext(string imagePath)
        {
            try
            {
                var filePath = HttpContext.Current.Server.MapPath(imagePath);
                var recogniseApi = new OcrApi.TextRecogniser();
                string result = recogniseApi.Recognise(filePath);

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(result)
                };

                return response;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
