using SkyriseRecruitWebAplication.Models;
using System;
using System.Web.Mvc;
using System.Net;
using System.Linq;

namespace SkyriseRecruitWebAplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(String url)
        {
            HTMLAnalizator HTMLAnalizator = new HTMLAnalizator();
            try
            {
                ViewModel Model = new ViewModel
                {
                    Result = HTMLAnalizator.GetAPIResult(url)
                };
                if (Model.Result.Error)
                {
                    ViewBag.Error = Model.Result.Messages;

                    return View();
                }

                Model.ListOfOccurence = HTMLAnalizator.GetListOfOccurence(Model.Result.Content);
                Model.MaxValue = Model.ListOfOccurence.Select(a => a.Value).Max();

                return View(Model);
            }
            catch (WebException ex)
            {
                ViewBag.Error = ex.Message;

                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt";

            return View();
        }
    }
}
