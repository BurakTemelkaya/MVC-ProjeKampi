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
        AdminManager adminManager = new AdminManager(new EfAdminDal());
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
            int id=0;
            var authorize = authorizationService.AdminLogin(p, out id);
            if(authorize)
            {
                var adminInfo = adminManager.GetByID(id);
                FormsAuthentication.SetAuthCookie(adminInfo.AdminUserName, false);
                Session["AdminUserName"] = adminInfo.AdminUserName;
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
        public ActionResult WriterLogin(WriterLogInDto p, string ReturnUrl)
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