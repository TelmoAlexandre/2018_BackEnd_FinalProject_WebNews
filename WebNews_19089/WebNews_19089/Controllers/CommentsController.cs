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

        
        

        // POST: Comments
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Content, NewsFK")] Comments comment, string email)
        {

            if (ModelState.IsValid)
            {

                // Procura o utilizador no UserProfile pelo email
                var user = db.UsersProfile.Where(u => u.UserName.Equals(email)).First();

                // Adiciona o ID do utilizador que comenta
                comment.UserProfileFK = user.ID;

                // Adicionar o HTML para o paragrafo na string
                comment.Content = comment.Content.Replace("\r\n", "<br/>");

                // TimeStamp da data
                comment.CommentDate = DateTime.Now;

                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "News", new { id = comment.NewsFK });
            }

            // Encontra novamente a noticia e retorna a mesma
            // Para mostrar a mensagem de validação do comentário.
            var news = db.News.Where(n => n.ID == comment.NewsFK).First();

            return View("../News/Details", news);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}