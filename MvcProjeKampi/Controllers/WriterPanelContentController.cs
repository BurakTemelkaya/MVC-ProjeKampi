using BusinessLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;

namespace MvcProjeKampi.Controllers
{
    [Authorize]
    public class WriterPanelContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        Context c = new Context();
        public ActionResult MyContent(string p, int page = 1)
        {
            p = (string)Session["WriterMail"];
            var writerIdInfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var contentValues = cm.GetListByWriter(writerIdInfo).ToPagedList(page, 5);
            return View(contentValues);
        }
        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.HeadingName = hm.GetByID(id).HeadingName;
            ViewBag.d = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddContent(Content p)
        {
            ContentValidator contentValidator = new ContentValidator();
            ValidationResult result = contentValidator.Validate(p);
            if (result.IsValid)
            {
                p.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                string mail = (string)Session["WriterMail"];
                var writerIdInfo = c.Writers.Where(x => x.WriterMail == mail).Select(y => y.WriterID).FirstOrDefault();
                if (writerIdInfo != 0)
                {
                    p.WriterID = writerIdInfo;
                    p.ContentStatus = true;
                    cm.ContentAdd(p);
                    return RedirectToAction("MyContent");
                }
                else
                {
                    return RedirectToAction("writerlogin", "login");
                }
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }
        public ActionResult toDoList()
        {
            return View();
        }
    }
}