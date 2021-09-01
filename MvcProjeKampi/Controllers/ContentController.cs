using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using PagedList;

namespace MvcProjeKampi.Controllers
{
    public class ContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllContent(string p)
        {
            if (p == null)
            {
                var values = cm.GetList();
                return View(values);
            }
            else
            {
                var values = cm.GetList(p);
                return View(values);
            }
        }
        public ActionResult ContentByHeading(int id, int p = 1)
        {
            var contentValues = cm.GetListByHeadingID(id).ToPagedList(p, 5);
            return View(contentValues);
        }
        public ActionResult GetAllContentByWriter(string p)
        {
            string writerMail = Session["WriterMail"].ToString();
            int writerID = wm.GetByMail(writerMail).WriterID;
            if (p == null)
            {
                var values = cm.GetListByWriter(writerID);
                return View(values);
            }
            else
            {
                var values = cm.GetListByWriter(writerID, p);
                return View(values);
            }
        }
    }
}