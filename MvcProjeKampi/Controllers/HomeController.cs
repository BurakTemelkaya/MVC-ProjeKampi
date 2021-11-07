using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;

namespace MvcProjeKampi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()//veri döndürmek için kullanılır
        {
            return View();
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
        public ActionResult Test()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult HomePage()
        {
            HeadingManager hm = new HeadingManager(new EfHeadingDal());
            ViewBag.FirstHeadingID = hm.GetList().FirstOrDefault().HeadingID;
            ViewBag.FirstHeadingName = hm.GetList().FirstOrDefault().HeadingName;
            return View();
        }
    }
}