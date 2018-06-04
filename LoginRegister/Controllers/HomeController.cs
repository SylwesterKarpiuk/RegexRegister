using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginRegister.Models;
namespace LoginRegister.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.userAccount.ToList());
            }
        }

    }
}