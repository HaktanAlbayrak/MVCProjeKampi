using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampi.Controllers
{
    public class İstatistikController : Controller
    {
        // GET: İstatistik
        Context c = new Context();
        public ActionResult Index()
        {
            ViewBag.CategoryCount = c.Categories.Count();


            ViewBag.CategoryTrue = c.Categories.Where(x => x.CategoryStatus == true).Count();
            ViewBag.CategoryFalse = c.Categories.Where(x => x.CategoryStatus == false).Count();
            ViewBag.CategoryStatusCount = ViewBag.CategoryTrue - ViewBag.CategoryFalse;


            ViewBag.CategorySoftware = c.Headings.Where(x => x.CategoryID == 25).Count();


            ViewBag.WritersName = c.Writers.Where(x => x.WriterName.Contains("A")).Count();


            var MostTitles = c.Headings.Where(u => u.CategoryID == c.Headings.GroupBy(x => x.CategoryID).OrderByDescending(x => x.Count())
                 .Select(x => x.Key).FirstOrDefault()).Select(x => x.Category.CategoryName).FirstOrDefault();
            ViewBag.MostTitles = MostTitles;
            return View();
        }
    }
}