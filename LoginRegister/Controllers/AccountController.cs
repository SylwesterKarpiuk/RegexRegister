using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginRegister.Models;

namespace LoginRegister.Controllers
{
    public class AccountController : Controller
    {
        Hash hash = new Hash();
        OurDbContext db = new OurDbContext();
        // GET: Account
        public ActionResult Index()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.userAccount.ToList());
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                account.Password = hash.CreatePasswordHash(account.Password);
                account.ConfirmPassword = account.Password;
                db.userAccount.Add(account);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
                ModelState.Clear();
                Session["Email"] = account.Email.ToString();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.userAccount.Where(u => u.Email == user.Email).FirstOrDefault();
                if (usr !=null && hash.VerifyPassword(user.Password, usr.Password))
                {
                    Session["Email"] = usr.Email.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Niepoprawny email, lub hasło");
                }
            }
            return View();
        }
        public ActionResult LoggedIn()
        {
            if (Session["Email"] != null && Session["Email"].ToString() != "")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public JsonResult doesEmailExist(string Email)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.userAccount.Where(u => u.Email == Email).FirstOrDefault();
                return Json(usr == null);
            }
        }
        public ActionResult Logoff()
        {
            if (Session["Email"] != null && Session["Email"].ToString() != "")
            {
                Session["Email"] = null;
               
            }
            return RedirectToAction("Index");
        }
    }
}