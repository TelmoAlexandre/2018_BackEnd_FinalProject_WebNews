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
        public ActionResult Index(string category, int? pageNum) {

            // Número de notícias por página
            const int newsPerPage = 3;

            // Calcular o numero de noticias que deve fazer 'skip' para ter uma paginação correta
            int takeNum = (pageNum != null) ? ((int)pageNum - 1) * newsPerPage : 0;

            // Booleano que informa se é a primeira página
            bool firstPage = (pageNum == null || (int)pageNum == 1) ? true : false;

            // Caso o caregory tenha conteudo
            // Significa foi pedido um index com filtragem de categorias
            // Retorna as noticias dessa categoria
            if (category != "all" && category != null) {

                // Procura as noticias da categoria, salta o numero de noticias necessárias para paginação e 'traz' o número de noticias por página
                var NewsCategories = db.News.Where(n => n.Category.Name == category).OrderByDescending(n => n.NewsDate).Skip(takeNum).Take(newsPerPage).ToList();

                // Guardar a última notícia para comprar se ainda existe mais páginas
                News news = db.News.Where(n => n.Category.Name == category).OrderByDescending(n => n.NewsDate).ToList().Last();

                // Booleano que vai informar na view se ainda existem mais páginas
                var lastPage = (NewsCategories.Count() != newsPerPage || NewsCategories.Contains(news)) ? true : false;

                return View(new NewsWithPageModelView {
                    News = NewsCategories,
                    pageNum = (int)pageNum,
                    lastPage = lastPage,
                    category = category,
                    firstPage = firstPage
                });
            }

            // Caso o category seja 'all'
            // Significa que foram pedidas todas as noticias
            // Retorna todas as noticias

            var News = db.News.Include(n => n.Category).OrderByDescending(n => n.NewsDate).Skip(takeNum).Take(newsPerPage).ToList();
            
            // Verifica qual é a ultima noticia para poder saber se é a ultima página
            var lastNews = db.News.OrderByDescending(n => n.NewsDate).ToList().Last();

            return View(new NewsWithPageModelView {
                News = News,
                // Se o pageNum for null, é porque nos encontramos na pagina 1
                // Senão, devolve o número da página
                pageNum = (pageNum == null) ? 1 : (int)pageNum,
                // Verifica se se encontra na ultima página
                lastPage = (News.Count() != newsPerPage || News.Contains(lastNews))? true : false,
                category = category,
                firstPage = firstPage
            });
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

            // Orderna os cometários por ordem (Mais recente primeiro)
            News.CommentsList = News.CommentsList.OrderByDescending(c => c.CommentDate).ToList();

            return View(News);
        }

        [Authorize(Roles = "Admin,Journalist")]
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
        public ActionResult Create([Bind(Include = "ID,Title,Description,Content,CategoryFK")] News News, HttpPostedFileBase[] fileUploadPhoto, string email) {

            if (ModelState.IsValid) {

                // Recolher o userProfile procurando o email do ASPNET
                var userProfile = db.UsersProfile.Where(u => u.UserName == email).First();

                // Acrescentar o jornalista à lista de autores da noticia
                News.UsersProfileList = db.UsersProfile.Where(u => u.ID==userProfile.ID).ToList();

                // Registar a data da noticia
                News.NewsDate = DateTime.Now;

                // Adicionar o HTML para o paragrafo na string
                News.Content = News.Content.Replace("\r\n", "<br/>");

                // Tratamento de cada imagem carregada
                int i = 0;

                // Caso existam fotografias, gerir as suas criações
                foreach (var item in fileUploadPhoto) {

                    // Garantir que existe uma fotografia em cada iteração do array de fotos carregadas
                    if (item != null) {

                        // Cria um nome para a imagem recebida e guarda a mesma
                        string photoName = DateTime.Now.ToString("_yyyyMMdd_hhmmss") + ".jpg";
                        string photoPath = Path.Combine(Server.MapPath("~/Images/"), photoName);

                        // Cria um objeto photo e adiciona-lhe o nome
                        Photos photo = new Photos {
                            Name = photoName
                        };

                        News.PhotosList.Add(photo);
                        item.SaveAs(photoPath);
                    }

                    i++;
                }


                db.News.Add(News);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryFK = new SelectList(db.Categories, "ID", "Name", News.CategoryFK);
            return View(News);
        }

        // GET: News/Edit/5
        [Authorize(Roles = "Admin,NewsEditor")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
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


            //try {

            News News = db.News.Find(id);

            // Criar uma lista de photos para serem eliminadas
            // Isto porque correr um foreach diretamente no News.PhotoList iria causar problemas
            // Pois as fotos elimandas estariam a alterar o News.PhotoList o que causaria um erro.
            List<Photos> listPhotos = new List<Photos>();

            // Adicionar todas as fotos à lista
            foreach (var photo in News.PhotosList)
                listPhotos.Add(photo);

            // Correr a lista e eliminar as fotos
            foreach (var photo in listPhotos) {

                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Images/"), photo.Name));
                db.Photos.Remove(photo);

            }

            // A mesma situação, mas para os comments
            List<Comments> listComments = new List<Comments>();
            
            foreach (var comment in News.CommentsList)
                listComments.Add(comment);

            // Remover os comentários da notícia
            foreach (var comment in listComments)
                db.Comments.Remove(comment);

            // Curtar a realação n-n
            


            db.News.Remove(News);
            db.SaveChanges();


            //} catch (Exception) {

            //    ModelState.AddModelError("", string.Format("I wasn't possible to remove this news article because there's still comments associated with it."));

            //}


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
