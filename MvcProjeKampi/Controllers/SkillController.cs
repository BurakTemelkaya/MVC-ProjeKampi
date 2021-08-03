using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class SkillController : Controller
    {
        SkillManager skillManager = new SkillManager(new EfSkillDal());
        SkillValidator skillvalidator = new SkillValidator();
        [AllowAnonymous]
        public ActionResult Index()
        {
            var skills = skillManager.GetList();
            return View(skills);
        }
        public ActionResult SkillListMenu()
        {
            var skills = skillManager.GetList();
            return View(skills);
        }
        [HttpGet]
        public ActionResult AddSkill()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSkill(Skill p)
        {
            ValidationResult result = skillvalidator.Validate(p);
            if (result.IsValid)
            {
                skillManager.SkillAdd(p);
                return RedirectToAction("SkillListMenu");
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
        public ActionResult DeleteSkill(int id)
        {
            var skill = skillManager.GetByID(id);
            skillManager.SkillDelete(skill);
            return RedirectToAction("SkillListMenu");
        }
        [HttpGet]
        public ActionResult EditSkill(int id)
        {
            var skill = skillManager.GetByID(id);
            return View(skill);
        }
        [HttpPost]
        public ActionResult EditSkill(Skill p)
        {
            ValidationResult result = skillvalidator.Validate(p);
            if (result.IsValid)
            {
                skillManager.SkillUpdate(p);
                return RedirectToAction("SkillListMenu");
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
    }
}