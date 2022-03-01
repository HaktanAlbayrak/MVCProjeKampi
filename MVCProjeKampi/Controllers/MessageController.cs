using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCProjeKampi.Controllers
{
    public class MessageController : Controller
    {

        // GET: Message
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();

        [Authorize]
        public ActionResult Inbox()
        {
            string p = (string)Session["AdminUserName"];
            var messagelist = mm.GetListInbox();
            return View(messagelist);
        }
        public ActionResult Sendbox()
        {
            string p = (string)Session["AdminUserName"];
            var messagelist = mm.GetListSendbox();
            return View(messagelist);
        }

        public ActionResult GetInBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult result = messagevalidator.Validate(p);
            if (result.IsValid)
            {
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(p);
                return RedirectToAction("SendBox");
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