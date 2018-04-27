using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class LoginController : Controller
    {
        ImageDBEntities dbEntities = new ImageDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        //get
        public ActionResult Register()
        {
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Stafff staff)
        {
            if (ModelState.IsValid)
            {
                dbEntities.Stafffs.Add(staff);
                dbEntities.SaveChanges();
                ViewBag.message = "User Successfully Registered";
                return RedirectToAction("Login");
            }
            return View();
        }

        //Get method
        public ActionResult Login()
        {
            return View();
        }
        //Post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Stafff staff)
        {
            //check model validity
            if (ModelState.IsValid)
            {
                // accessing database 
                ImageDBEntities dbEntities = new ImageDBEntities();
                //validating and getting user details
                var userDetails = dbEntities.Stafffs.Where(a => a.UserID.Equals(staff.UserID)
                                  && a.Password.Equals(staff.Password)).FirstOrDefault();
                // validating and redirecting to other webpage
                if (userDetails != null)
                {
                    Session["ID"] = userDetails.UserID.ToString();
                    return RedirectToAction("Submit", "UploadDetails");
                }
                else
                {
                    return Content("Incorrect details, try again!");
                }
            }
            return View(staff);

        }

        //logout action
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

    }
}