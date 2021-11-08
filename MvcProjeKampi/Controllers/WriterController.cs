using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator writerValidator = new WriterValidator();
        IAuthorizationService authorizationService = new AuthorizationManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        public ActionResult Index()
        {
            var writerValues = wm.GetList();
            return View(writerValues);
        }
        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWriter(WriterLogInDto p)
        {
            ValidationResult results = writerValidator.Validate(p);
            if (results.IsValid)
            {
                authorizationService.WriterAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writerValue = wm.GetByIdWriterDto(id);
            return View(writerValue);
        }
        [HttpPost]
        public ActionResult EditWriter(WriterLogInDto p)
        {
            ValidationResult results = writerValidator.Validate(p);
            if (results.IsValid)
            {
                authorizationService.WriterUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}