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
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    public class AuthorizationController : Controller
    {
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        WriterValidator writerValidator = new WriterValidator();
        WriterManager WriterManager = new WriterManager(new EfWriterDal());
        IAuthorizationService authorizationService = new AuthorizationManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        public ActionResult Index()
        {
            var adminValues = adminManager.GetList();
            return View(adminValues);
        }
        [HttpGet][AllowAnonymous]
        public ActionResult AddAdmin()
        {            
            ViewBag.roles = GetRoles();
            return View();
        }
        [HttpPost][AllowAnonymous]
        public ActionResult AddAdmin(AdminLogInDto adminLogInDto)
        {          
            adminLogInDto.AdminStatus = true;
            authorizationService.AdminAdd(adminLogInDto);         
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            ViewBag.roles = GetRoles();
            var adminValue = adminManager.GetByIDDto(id);
            return View(adminValue);
        }
        [HttpPost]
        public ActionResult EditAdmin(AdminLogInDto p)
        {
            authorizationService.AdminUpdate(p);
            return RedirectToAction("Index");
        }
        public ActionResult StatusChangedAdmin(int id)
        {
            var admin = adminManager.GetByID(id);
            if (admin.AdminStatus)
            {
                admin.AdminStatus = false;
            }
            else
            {
                admin.AdminStatus = true;
            }
            adminManager.AdminUpdate(admin);
            return RedirectToAction("Index");
        }
        [AllowAnonymous][HttpGet]
        public ActionResult WriterRegister()
        {
            return View();
        }
        [AllowAnonymous][HttpPost]
        public ActionResult WriterRegister(WriterLoginDto writerLogInDto)
        {
            /*ValidationResult result = writerValidator.Validate(p);
            if (result.IsValid)
            {
                p.WriterStatus = true;
                WriterManager.WriterAdd(p);
                FormsAuthentication.SetAuthCookie(p.WriterMail, false);
                Session["WriterMail"] = p.WriterMail;
                return RedirectToAction("AllHeading", "WriterPanel");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            eski kodlar */
            authorizationService.WriterRegister(
                writerLogInDto.WriterName,
                writerLogInDto.WriterSurName,
                writerLogInDto.WriterTitle,
                writerLogInDto.WriterAbout,
                writerLogInDto.WriterImage,
                writerLogInDto.WriterMail,
                writerLogInDto.WriterPassword,
                writerLogInDto.WriterStatus = true
                );
            FormsAuthentication.SetAuthCookie(writerLogInDto.WriterMail, false);
            Session["WriterMail"] = writerLogInDto.WriterMail;
            return RedirectToAction("AllHeading", "WriterPanel");
        }
        public List<SelectListItem> GetRoles()
        {
            List<string> roles = new List<string>();
            roles.Add("A");
            roles.Add("B");
            roles.Add("C");
            List<SelectListItem> adminRole = (from x in roles
                                              select new SelectListItem
                                              {
                                                  Text = x,
                                                  Value = x
                                              }).ToList();
            return adminRole;
        }
    }
}