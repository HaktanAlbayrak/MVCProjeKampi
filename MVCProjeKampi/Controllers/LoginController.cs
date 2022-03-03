using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCProjeKampi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        Context c = new Context();
        AdminLoginManager adminLoginManager = new AdminLoginManager(new EfAdminDal());
        WriterLoginManager writerLoginManager = new WriterLoginManager(new EfWriterDal());

        // GET: Login

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin p)
        {

            //var adminuserinfo = c.Admins.FirstOrDefault(x => x.AdminUserName == p.AdminUserName && x.AdminPassword == p.AdminPassword);
            //if (adminuserinfo != null)
            //{
            //    FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName, false);
            //    Session["AdminUserName"] = adminuserinfo.AdminUserName;
            //    RedirectToAction("Index", "AdminCategory");
            //}
            //else
            //{
            //    RedirectToAction("Index");
            //}

            var adminuserinfo = adminLoginManager.GetAdmin(p.AdminUserName, p.AdminPassword);
            if (adminuserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName, false);
                Session["AdminUserName"] = adminuserinfo.AdminUserName;
                return RedirectToAction("Index", "Heading");
            }
            else
            {
                RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = "Kullanıcı Adı veya Şifre Yanlış";
            return View();
        }

        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {
            var writeruserinfo = writerLoginManager.GetWriter(p.WriterMail, p.WriterPassword);
            if (writeruserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
                Session["WriterMail"] = writeruserinfo.WriterMail;
                return RedirectToAction("WriterProfile", "WriterPanel");
            }
            else
            {
                RedirectToAction("WriterLogin");
            }
            return View();

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings", "Default");
        }

        public ActionResult AdminLogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Index");
        }
    }
}