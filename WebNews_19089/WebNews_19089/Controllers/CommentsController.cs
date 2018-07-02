using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNews_19089.Models;

namespace WebNews_19089.Controllers
{
    public class CommentsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public CommentsController()
        {
        }

        // GET: Comments
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Create([Bind(Include = "Content, NewsFK")] Comments comment, string email)
        {

            if (ModelState.IsValid)
            {

                var user = db.UsersProfile.Where(u => u.UserName.Equals(email)).First();

                comment.CommentDate = DateTime.Now;

                comment.UserProfileFK = user.ID;

                db.Comments.Add(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Details", "News", new { id = comment.NewsFK });
        }
    }
}