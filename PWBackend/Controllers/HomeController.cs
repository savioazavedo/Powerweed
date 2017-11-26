using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PWBackend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin objUser)
        {
            if (ModelState.IsValid)
            {
                using (visionDatabaseEntities db = new visionDatabaseEntities())
                {
                    var obj = db.Admins.Where(a => a.AdminNAME.Equals(objUser.AdminNAME) && a.AdminPASSWORD.Equals(objUser.AdminPASSWORD)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.AdminID.ToString();
                        Session["UserNAME"] = obj.AdminNAME.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(objUser);
        }
    }
}
