using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebNews_19089.Models;

namespace WebNews_19089.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;

        public UserProfileController()
        {
        }

        public UserProfileController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        // GET: UserProfile
        public ActionResult Edit(string email)
        {
            if (email != null)
            {
                return View(db.UsersProfile.Where(u => u.UserName.Equals(email)).First());
            }

            return RedirectToAction("Index","News",null);
        }

        // POST: UserProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Birthday")] UsersProfile user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LogOff", "Account", null);
            }

            return View();
        }
    }
}