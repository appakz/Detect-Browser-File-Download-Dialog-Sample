using System;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BlockUIFileDownloadWeb.Models;

namespace BlockUIFileDownloadWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "File Download With Block UI Sample";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateInfoSheet(CustomerInfoModel customerInfo)
        {
            //use the submitted download token value to set a cookie in the response
            Response.AppendCookie(new HttpCookie("fileDownloadToken", customerInfo.DownloadToken));

            var file = CreateFile();
            var outputFileName = string.Format("{0}_CustomerInfo.pdf", DateTime.Now.Ticks);
            return File(file, "application/pdf", outputFileName);
        }

        private Stream CreateFile()
        {
            //to simulate a server process taking a few seconds to render the PDF file:
            Thread.Sleep(5000);
            var filePath = Request.MapPath("~/Content/CustomerInfoSheet.pdf");
            return new FileStream(filePath, FileMode.Open);
        }
    }
}
