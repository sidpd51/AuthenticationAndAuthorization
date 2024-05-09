using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Web.Security;

namespace WebApplication2.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model) {
            using (var context = new office_sidpdEntities())
            {
                bool isValid = context.Users.Any(x => x.username == model.username && x.password == model.password);
                if (isValid) 
                {
                    FormsAuthentication.SetAuthCookie(model.username, false);
                    return RedirectToAction("Index", "Employees");
                }
                ModelState.AddModelError("", "Invalid user and password");
                return View();
            }
        }

        public ActionResult Signup()
        {
            return View();  
        }

        [HttpPost]
        public ActionResult Signup(User model) {
            using (var context = new office_sidpdEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}