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
    [AllowAnonymous]
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
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        [AllowAnonymous]
        public ActionResult HomePage()
        {
            ViewBag.FirstHeadingID = hm.GetList().FirstOrDefault().HeadingID;
            ViewBag.FirstHeadingName = hm.GetList().FirstOrDefault().HeadingName;
            return View();
        }
        public PartialViewResult Istatistikler()
        {
            ContentManager cm = new ContentManager(new EfContentDal());
            WriterManager wm = new WriterManager(new EfWriterDal());
            MessageManager mm = new MessageManager(new EfMessageDal());
            ViewBag.HeadingCount = hm.GetList().Count();
            ViewBag.EntryCount = cm.GetList().Count();
            ViewBag.WriterCount = wm.GetList().Count();                        
            ViewBag.MessageCount = mm.MessageCount();

            return PartialView();
        }
    }
}