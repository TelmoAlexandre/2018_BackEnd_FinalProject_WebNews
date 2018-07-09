using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNews_19089.Models;

namespace WebNews_19089.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories/Create
        
        public ActionResult Create()
        {
            return View(new Categories { });
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Categories category)
        {

            if (ModelState.IsValid)
            {
                // Garante que não existem categorias com o mesmo nome
                if(db.Categories.Where(c => c.Name == category.Name).Count() == 0)
                {
                    try
                    {
                        db.Categories.Add(category);
                        db.SaveChanges();

                        return RedirectToAction("Index", "News", null);
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Index", "UserError", new { error = "We couldn't create this category.", details = "Something went wrong." });
                    }
                }
                else
                {
                    return RedirectToAction("Index", "UserError", new { error = "There's already a category with that name.", details = "Try a different name." });
                }

                

            }

            return View(category);
        }

        // GET: Categories/Delete
        public ActionResult Delete()
        {
            ViewBag.Categories = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? Categories)
        {
            // Garantir que foi recibido um valor
            // Caso seja null, retornar a view Delete
            if(Categories != null)
            {
                Categories category = db.Categories.Find(Categories);

                // Caso a categoria não exista.
                if(category == null)
                {
                    return RedirectToAction("Index", "UserError", new { error = "This category doesn't exist.", details = "Can't delete an nonexistent category." });
                }


                // Tenta remover as noticias associadas com a categoria
                // Caso não consiga, indicar ao utilizador através da view de erro.
                try
                {
                    List<News> newsList = new List<News>();

                    foreach (var news in category.NewsList)
                        newsList.Add(news);

                    foreach (var news in newsList)
                        db.News.Remove(news);

                    db.SaveChanges();

                }
                catch(Exception)
                {
                    return RedirectToAction("Index", "UserError", new { error = "We weren't able to delete this category.", details = "There's still News articles associated with it." });
                }
                

                // Tentar apagar
                // Caso não consiga, indicar ao utilizador através da view de erro.
                try
                {
                    db.Categories.Remove(category);
                    db.SaveChanges();

                    return RedirectToAction("Index", "News", null);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "UserError", new { error = "We weren't able to delete this category.", details = "Something went wrong." });
                }
                
            }

            return View();
        }
    }
}