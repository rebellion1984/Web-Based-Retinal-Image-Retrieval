using FYP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class UploadDetailsController : Controller
    {
        // creating a dbContext object
        ImageDBEntities dbEntities = new ImageDBEntities();

        //listing everything in the table
        public ActionResult List()
        {
            return View(dbEntities.Patients.ToList());
        }
        // saving fields in DB
        public ActionResult Submit()
        {
            return View();
        }
        //saving fields in DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(Patient patient, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string ds = file.FileName.Substring(file.FileName.Length - 3);//patient photo
                string p = string.Empty;
                p = Server.MapPath("~/Images/"); // path to save image
                file.SaveAs(p + file.FileName);

                // string fileName = Path.GetFileNameWithoutExtension(file.FileName); // file name
                // string extension = Path.GetExtension(file.FileName); // file extension
                // fileName = fileName +DateTime.Now.ToString("yymmssfff")+ extension; // full file name
                //// file.I = "~/Image/" + fileName; // relative path
                // fileName = Path.Combine(Server.MapPath("~/ Image / "), fileName);
                // file.SaveAs(fileName);
                using (dbEntities)
                {
                    patient.Retinal_Image = file.FileName; // pic is field name in table (DB)

                    dbEntities.Patients.Add(patient);
                    dbEntities.SaveChanges();
                }
            }
            else
            {
                return Content("Please Select File");
            }
            return RedirectToAction("List");
        }
    }
}

