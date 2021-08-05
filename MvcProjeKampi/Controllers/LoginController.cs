using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        IAuthorizationService authorizationService = new AuthorizationManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(AdminLogInDto p, string ReturnUrl)
        {
            //var adminUserInfo = adminLoginManager.GetAdmin(p.AdminUserName, p.AdminPassword);
            //if (adminUserInfo != null)//hashsiz giriş
            if(authorizationService.AdminLogin(p))
            {
                FormsAuthentication.SetAuthCookie(p.AdminUserName, false);
                Session["AdminUserName"] = p.AdminUserName;
                if (!string.IsNullOrEmpty(ReturnUrl))
                    return Redirect(ReturnUrl);
                else
                    return RedirectToAction("Index", "Istatistik");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(WriterLoginDto p, string ReturnUrl)
        {            
            //var writerUserInfo = wm.GetWriter(p.WriterMail, p.WriterPassword);
            //if (writerUserInfo != null)
            if(authorizationService.WriterLogIn(p))
            {
                FormsAuthentication.SetAuthCookie(p.WriterMail, false);
                Session["WriterMail"] = p.WriterMail;
                if (!string.IsNullOrEmpty(ReturnUrl))
                    return Redirect(ReturnUrl);//return url'e gitmemizi sağlıyor
                else
                    return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                return RedirectToAction("WriterLogin");
            }

        }        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings", "Default");
        }
    }
}