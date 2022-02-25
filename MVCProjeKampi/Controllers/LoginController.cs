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
    public class LoginController : Controller
    {

        Context c = new Context();
        AdminLoginManager adminLoginManager = new AdminLoginManager(new EfAdminDal());


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
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                RedirectToAction("Index");
            }
            ViewBag.value = "Kullanıcı Adı veya Şifre Yanlış";
            return View();
        }
    }
}