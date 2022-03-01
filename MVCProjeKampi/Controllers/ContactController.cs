using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampi.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        Context c = new Context();
        ContactManager cm = new ContactManager(new EfContactDal());
        ContactValidator cv = new ContactValidator();

        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetByID(id);
            return View(contactvalues);
        }

        public PartialViewResult ContactPartial()
        {
            string p = (string)Session["WriterMail"];
            ViewBag.value = c.Messages.Where(x => x.ReceiverMail == p).Count();
            ViewBag.value2 = c.Contacts.Count();
            return PartialView();
        }
    }
}