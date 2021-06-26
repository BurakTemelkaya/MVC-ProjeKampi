using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Views.istatistik_view_klasoru
{
    public class IstatistikController : Controller
    {
        Context c = new Context();
        // GET: Istatistik
        public ActionResult Index()
        {
            ViewBag.SumCategori = c.Categories.Count();
            ViewBag.HeadYazilimSum = c.Categories.Where(x => x.CategoryName == "Yazılım").Count();
            ViewBag.WriterCount = c.Writers.Where(c => c.WriterName.Contains("a")).Count();
            ViewBag.MaxCategoryName = c.Headings.Max(y => y.Category.CategoryName);
            ViewBag.CategoryMaxMinDifference = c.Categories.Where(x => x.CategoryStatus).Count() - c.Categories.Where(x => !x.CategoryStatus).Count();
            return View();
        }
    }
}