using BusinessLayer.Concrete;
using DataAccesLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using MvcProjeKampi.Models;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        ContentManager cm = new ContentManager(new EfContentDal());
        [Route("Baslik/{HeadingName?}/{id:int:min(2)}")]
        public ActionResult Headings()
        {
            HeadingByContentCount headingCountContent = new HeadingByContentCount();
            headingCountContent.Headings = hm.GetList().Where(x => x.HeadingStatus);
            headingCountContent.Contents = cm.GetList().Where(x => x.Heading.HeadingStatus);
            return View(headingCountContent);
        }
        public PartialViewResult Index(int p = 1, int id = 1)
        {
            var contentList = cm.GetListByHeadingID(id).ToPagedList(p, 5);
            ViewBag.ID = id;
            ViewBag.HeadingName = hm.GetByID(id).HeadingName;
            return PartialView(contentList);
        }
    }
}