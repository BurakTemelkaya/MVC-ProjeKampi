using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;

namespace MvcProjeKampi.Controllers
{
    public class ContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllContent(string p)
        {           
            if (p==null)
            {
                var values = cm.GetList();
                return View(values.ToList());
            }
            else
            {
                var values = cm.GetList(p);
                return View(values);
            }            
        }
        public ActionResult ContentByHeading(int id)
        {
            var contentValues = cm.GetListByHeadingID(id);
            return View(contentValues);
        }
    }
}