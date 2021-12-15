using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using System.Net;
using MvcProjeKampi.Models;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        IAuthorizationService authorizationService = new AuthorizationManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(AdminLogInDto p, string ReturnUrl)
        {
            var response = Request["g-recaptcha-response"];
            const string secret = "6LeuxTscAAAAAJ6cc1flqjJt3-51epR4rxl58OCC";
            var client = new WebClient();
            var reply =
                    client.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            if (captchaResponse.Success)
            {
                int id = 0;
                var authorize = authorizationService.AdminLogin(p, out id);
                if (authorize)
                {
                    var adminInfo = adminManager.GetByID(id);
                    FormsAuthentication.SetAuthCookie(adminInfo.AdminUserName, true);
                    Session["AdminUserName"] = adminInfo.AdminUserName;
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return Redirect(ReturnUrl);
                    else
                        return RedirectToAction("Index", "Istatistik");
                }
                else
                {
                    TempData["Message"] = "Kullanıcı Adınız veya Şifreniz Hatalı Lütfen Tekrar Deneyiniz";
                }
            }
            else
            {
                TempData["Message"] = "Lütfen güvenliği doğrulayınız";
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(WriterLogInDto p, string ReturnUrl)
        {
            var response = Request["g-recaptcha-response"];
            const string secret = "6LeuxTscAAAAAJ6cc1flqjJt3-51epR4rxl58OCC";
            var client = new WebClient();
            var reply =
                    client.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            if (captchaResponse.Success)
            {
                if (authorizationService.WriterLogIn(p))
                {
                    FormsAuthentication.SetAuthCookie(p.WriterMail, true);
                    Session["WriterMail"] = p.WriterMail;
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return Redirect(ReturnUrl);//return url'e gitmemizi sağlıyor
                    else
                        return RedirectToAction("MyContent", "WriterPanelContent");
                }
                else
                {
                    TempData["Message"] = "Kullanıcı Adınız veya Şifreniz Hatalı Lütfen Tekrar Deneyiniz";
                    return RedirectToAction("WriterLogin");
                }
            }
            else
            {
                TempData["Message"] = "Lütfen Güvenliği Doğrulayınız";
                return RedirectToAction("WriterLogin");
            }


        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("HomePage", "Home");
        }
    }
}