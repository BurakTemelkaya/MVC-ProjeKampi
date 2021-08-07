using BusinessLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Dto;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Headings()
        {
            var heading = hm.GetList();
            var content = cm.GetList();
            HeadingCountContent headingCountContent = new HeadingCountContent();           
            headingCountContent.headings = heading;
            headingCountContent.contents = content;
            return View(headingCountContent);
        }
        public PartialViewResult Index(int id = 0, int p = 1)
        {
            var contentList = cm.GetListByHeadingID(id).ToPagedList(p, 5);
            ViewBag.id = id;
            return PartialView(contentList);
        }
    }
}