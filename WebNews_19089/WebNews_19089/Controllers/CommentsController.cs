using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNews_19089.Models;

namespace WebNews_19089.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: ~/Comments/Edit/{id, email}
        public ActionResult Edit(int? id, string Page)
        {

            if (id == null)
            {
                return RedirectToAction("Index", "UserError", new { error = "ID not provided." });
            }

            Comments comment = db.Comments.Find(id);

            // Caso tenha não encontrado o comentário
            if (comment == null)
            {
                return RedirectToAction("Index", "UserError", new { error = "We couldn't find this comment.", details = "The ID was not valid." });
            }

            // Garantir que o utilizador está autenticado
            if (User.Identity.Name != null)
            {
                // Garantir que se trata do dono do comentario
                if (User.Identity.Name == comment.UserProfile.UserName)
                {

                    comment.Content = comment.Content.Replace("<br/>", "\r\n");

                    // Guardar a pagina de origem deste GET para retornar corretamente no DeleteConfirm
                    // Criei um view model que recebe um objeto Comments e uma string com a página de origem para este GET
                    return View(new CommentsModificationViewModel
                    {
                        comment = comment,
                        // Para saber a localização de origim do pedido para poder fazer o redirect
                        // correto no POST
                        Page = Page
                    });
                }
                else
                {
                    return RedirectToAction("Index", "UserError", new { error = "You can't edit this comment.", details = "You lack the permissions to do so." });
                }
            }
            else
            {
                // Se não estiver logado
                // Redirecionar para o login
                return RedirectToAction("Login", "Account", null);
            }

        }


        // POST: ~/Comments/Edit/{id, email}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NewsFK,UserProfileFK,CommentDate,Content")] Comments comment, string Page)
        {
            if (ModelState.IsValid)
            {

                comment.Content = comment.Content.Replace("\r\n", "<br/>");

                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();

                // Retornar para a página em que foi originado o pedido GET
                if (Page == "Manage")
                {
                    return RedirectToAction("Index", "Manage", new { email = User.Identity.Name });
                }
                else
                {
                    return RedirectToAction("Details", "News", new { id = comment.NewsFK });
                }
            }
            else
            {
                // Retornar com a validation message
                return View(new CommentsModificationViewModel
                {
                    comment = comment,
                    // Para saber a localização de origim do pedido para poder fazer o redirect
                    // correto no POST
                    Page = Page
                });
            }
        }

        // Este GET do delete tem autenticação manual feita por mim
        // Para garantir que o dono do comentário o pode apagar.

        // GET: ~/Comments/Delete/{id, email}
        public ActionResult Delete(int? id, string Page)
        {

            if (id == null)
            {
                return RedirectToAction("Index", "UserError", new { error = "ID not provided." });
            }

            Comments comment = db.Comments.Find(id);

            // Caso tenha encontrado o comentário
            if (comment == null)
            {
                return RedirectToAction("Index", "UserError", new { error = "We couldn't find this comment.", details = "The ID was not valid." });
            }

            // Garantir autenticação manualmente para garantir que o user tem o role necessário
            // ou é o dono do comentário
            if (User.Identity.Name != null)
            {
                // Garantir que se trata do dono do comentario, ou de um utilizador com o papel necessario para apagar o mesmo
                if (User.Identity.Name == comment.UserProfile.UserName || User.IsInRole("Admin") || User.IsInRole("NewsEditor"))
                {

                    // Guardar a pagina de origem deste GET para retornar corretamente no DeleteConfirm
                    // Criei um view model que recebe um objeto Comments e uma string com a página de origem para este GET
                    return View(new CommentsModificationViewModel
                    {
                        comment = comment,
                        // Para saber a localização de origim do pedido para poder fazer o redirect
                        // correto no POST
                        Page = Page
                    });
                }
                else
                {
                    return RedirectToAction("Index", "UserError", new { error = "You can't delete this comment.", details = "You lack the permissions to do so." });
                }
            }
            else
            {
                // Se não tiver autenticado(email == null)
                // Redirecionar para o login
                return RedirectToAction("Login", "Account", null);
            }
        }

        // POST: ~/Comments/Delete/{id, email}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string Page)
        {

            Comments comment = db.Comments.Find(id);

            db.Comments.Remove(comment);
            db.SaveChanges();

            // Garantir que retorna para a página de onde foi chamado o get
            // Caso tenha sido diretamente na noticia ou no Manage do user
            if (Page == "Manage")
            {
                return RedirectToAction("Index", "Manage", new { email = User.Identity.Name });
            }
            else
            {
                return RedirectToAction("Details", "News", new { id = comment.NewsFK });
            }
        }

        // POST: Comments
        [HttpPost]
        [ValidateAntiForgeryToken]
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