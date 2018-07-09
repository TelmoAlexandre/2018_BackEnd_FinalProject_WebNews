using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNews_19089.Models;

namespace WebNews_19089.Controllers
{
    public class CategoriesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories/Create
        [Authorize(Roles = "Admin")]
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
    }
}