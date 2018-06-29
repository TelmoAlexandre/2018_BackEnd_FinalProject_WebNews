using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebNews_19089.Models;

namespace WebNews_19089.Controllers {
    public class NewsController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News/Index/id?
        public ActionResult Index(int? categoryID) {


            // Caso o caregoryID tenha conteudo
            // Significa foi pedido um index com filtragem de categorias
            // Retorna as noticias dessa categoria
            if(categoryID != null) {

                var NewsCategories = db.News.Where(n => n.Category.ID == categoryID);
                return View(NewsCategories.ToList());

            } 


            // Caso o categoryID esteja vazio
            // Significa que foram pedidas todas as noticias
            // Retorna todas as noticias
            var News = db.News.Include(n => n.Category).OrderByDescending(n => n.NewsDate);
            return View(News.ToList());
        }

        // GET: News/Details/5
        public ActionResult Details(int? id) {

            // Caso o id esteja vazio
            // Significa que não foi enviado nenhum id por parametro
            // Redireciona para um 'BadRequest'
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News News = db.News.Find(id);
            if (News == null) {
                return HttpNotFound();
            }
            return View(News);
        }

        [Authorize(Roles = "Admin")]
        // GET: News/Create
        public ActionResult Create() {
            ViewBag.CategoryFK = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Content,CategoryFK")] News News, HttpPostedFileBase[] fileUploadPhoto) {

            // Registar a data da noticia
            News.NewsDate = DateTime.Now;

            // Adicionar o HTML para o paragrafo na string
            News.Content = News.Content.Replace("\r\n", "<br/>");

            if (ModelState.IsValid) {

                // Tratamento de cada imagem carregada
                int i = 0;

                foreach (var item in fileUploadPhoto) {

                    // Cria um nome para a imagem recebida e guarda a mesma
                    string photoName = News.Title.Substring(0, 10).Replace(" ", "") + i + ".jpg";
                    string photoPath = Path.Combine(Server.MapPath("~/Images/"), photoName);

                    // Cria um objeto photo e adiciona-lhe o nome
                    Photos photo = new Photos {
                        Name = photoName
                    };

                    News.PhotosList.Add(photo);
                    item.SaveAs(photoPath);

                    i++;
                }

                db.News.Add(News);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CategoryFK = new SelectList(db.Categories, "ID", "Name", News.CategoryFK);
            return View(News);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            News News = db.News.Find(id);
            News.Content = News.Content.Replace("<br/>", "\r\n");

            if (News == null) {
                return HttpNotFound();
            }
            ViewBag.CategoryFK = new SelectList(db.Categories, "ID", "Name", News.CategoryFK);
            return View(News);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Content,NewsDate,CategoryFK")] News News) {
            if (ModelState.IsValid) {
                // Adicionar o HTML para o paragrafo na string
                News.Content = News.Content.Replace("\r\n", "<br/>");
                db.Entry(News).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryFK = new SelectList(db.Categories, "ID", "Name", News.CategoryFK);
            return View(News);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News News = db.News.Find(id);
            if (News == null) {
                return HttpNotFound();
            }
            return View(News);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            News News = db.News.Find(id);
            db.News.Remove(News);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Dropdown list das categorias ------------------------------------------------------

        // GET: Categories for the dropdown list
        [ChildActionOnly]
        public ActionResult CategoriesDropdown() {

            // Recolhe todas as categorias
            var categories = db.Categories.ToList();

            // Retorna a PartialView da dropdown
            return PartialView("_categoriesDropdownPartial", categories);
        }

    protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
