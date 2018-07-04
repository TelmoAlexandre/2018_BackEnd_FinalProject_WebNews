using Microsoft.AspNet.Identity;
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


        // Este GET do delete tem autenticação manual feita por mim
        // Para garantir que o dono do comentário o pode apagar.

        // GET: ~/Comments/Delete/{id, email}
        public ActionResult Delete(int? id, string email, string page)
        {

            if (id != null)
            {

                Comments comment = db.Comments.Find(id);

                // Caso tenha encontrado o comentário
                if (comment != null)
                {

                    // Garantir autenticação manualmente para garantir que o user tem o role necessário
                    // ou é o dono do comentário
                    if (email != null)
                    {
                        // Garantir que se trata do dono do comentario, ou de um utilizador com o papel necessario para apagar o mesmo
                        if (email.Equals(comment.UserProfile.UserName) || User.IsInRole("Admin") || User.IsInRole("NewsEditor"))
                        {

                            // Guardar a pagina de origem deste GET para retornar corretamente no DeleteConfirm
                            // Criei um view model que recebe um objeto Comments e uma string com a página de origem para este GET
                            return View(new CommentsDeleteViewModel {
                                comment = comment,
                                Page = page
                            });
                        }
                    }
                    else
                    {
                        // Se não for o dono do comment ou não tiver o papel necessário para apagar ou não tiver autenticado(email == null)
                        // Redirecionar para o login
                        return RedirectToAction("Login", "Account", null);
                    }
                }

            }

            return RedirectToAction("Index", "News", null);
        }

        // POST: ~/Comments/Delete/{id, email}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string Page)
        {

            Comments comment = db.Comments.Find(id);

            int NewsID = comment.NewsFK;

            db.Comments.Remove(comment);
            db.SaveChanges();

            // Garantir que retorna para a página de onde foi chamado o get
            // Caso tenha sido diretamente na noticia ou no Manage do user
            if (Page.Equals("Manage"))
            {
                return RedirectToAction("Index", "Manage", new { email = User.Identity.Name });
            }
            else
            {
                return RedirectToAction("Details", "News", new { id = NewsID });
            }
        }

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