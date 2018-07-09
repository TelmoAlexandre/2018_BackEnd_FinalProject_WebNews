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

namespace WebNews_19089.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News/Index/id?
        public ActionResult Index(string category, int? pageNum)
        {

            // Número de notícias por página
            const int newsPerPage = 6;

            // Objeto noticias
            News news;

            // Calcular o numero de noticias que deve fazer 'skip' para ter uma paginação correta
            int takeNum = (pageNum != null) ? ((int)pageNum - 1) * newsPerPage : 0;

            // Booleano que informa se é a primeira página
            bool firstPage = (pageNum == null || (int)pageNum == 1) ? true : false;

            // Caso o category tenha conteudo
            // Significa foi pedido um index com filtragem de categorias
            // Retorna as noticias dessa categoria
            if (category != "All" && category != null)
            {

                // Procura as noticias da categoria, salta o numero de noticias necessárias para paginação e 'traz' o número de noticias por página
                var NewsCategories = db.News.Where(n => n.Category.Name == category).OrderByDescending(n => n.NewsDate).Skip(takeNum).Take(newsPerPage).ToList();

                // Tenta encontrar a ultima noticia
                // Se não conseguir é porque não existe
                try
                {
                    // Guardar a última notícia para comprar se ainda existe mais páginas
                    news = db.News.Where(n => n.Category.Name == category).OrderByDescending(n => n.NewsDate).ToList().Last();
                }
                catch (Exception)
                {
                    news = null;
                }
                

                // Booleano que vai informar na view se ainda existem mais páginas
                var lastPage = (NewsCategories.Count() != newsPerPage || NewsCategories.Contains(news)) ? true : false;

                return View(new NewsWithPageModelView
                {
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

            // Tenta encontrar a ultima noticia
            // Se não conseguir é porque não existe
            try
            {
                // Guardar a última notícia para comprar se ainda existe mais páginas
                news = db.News.OrderByDescending(n => n.NewsDate).ToList().Last();
            }
            catch (Exception)
            {
                news = null;
            }

            return View(new NewsWithPageModelView
            {
                News = News,
                // Se o pageNum for null, é porque nos encontramos na pagina 1
                // Senão, devolve o número da página
                pageNum = (pageNum == null) ? 1 : (int)pageNum,
                // Verifica se se encontra na ultima página
                lastPage = (News.Count() != newsPerPage || News.Contains(news)) ? true : false,
                category = (category == null) ? "All" : category,
                firstPage = firstPage
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string searchFilter, string category)
        {

            // Declaração
            ICollection<News> news;

            if (category != "" && category != "All")
            {
                news = db.News.Where(n => n.Category.Name == category).Where(n => n.Title.Contains(searchFilter)).OrderByDescending(n => n.NewsDate).ToList();
            }
            else
            {
                news = db.News.Where(n => n.Title.Contains(searchFilter)).OrderByDescending(n => n.NewsDate).ToList();
            }


            return View(new NewsWithPageModelView
            {
                News = news,
                // Booleanos a verdade para não aparecer os links para a proxima pagina, nem anterior
                firstPage = true,
                lastPage = true,
                category = category
            });


        }


        // GET: News/Details/5
        public ActionResult Details(int? id)
        {

            // Caso o id esteja vazio
            // Significa que não foi enviado nenhum id por parametro
            // Redireciona para um 'BadRequest'
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            News News = db.News.Find(id);
            if (News == null)
            {
                return HttpNotFound();
            }

            // Orderna os cometários por ordem (Mais recente primeiro)
            News.CommentsList = News.CommentsList.OrderByDescending(c => c.CommentDate).ToList();

            return View(News);
        }

        [Authorize(Roles = "Admin,Journalist")]
        // GET: News/Create
        public ActionResult Create()
        {

            SelectList list = new SelectList(db.Categories, "ID", "Name");

            // Caso não existam categorias, não deixa criar uma noticia
            if(list.Count() == 0)
            {
                return RedirectToAction("Index", "UserError", new { error = "You can't create a News Article.", details = "There are no categories created." });
            }

            ViewBag.CategoryFK = list;
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Content,CategoryFK")] News News, HttpPostedFileBase[] fileUploadPhoto, string email)
        {

            if (ModelState.IsValid)
            {

                // Recolher o userProfile procurando o email do ASPNET
                var userProfile = db.UsersProfile.Where(u => u.UserName == email).First();

                // Acrescentar o jornalista à lista de autores da noticia
                News.UsersProfileList = db.UsersProfile.Where(u => u.ID == userProfile.ID).ToList();

                // Registar a data da noticia
                News.NewsDate = DateTime.Now;

                // Adicionar o HTML para o paragrafo na string
                News.Content = News.Content.Replace("\r\n", "<br/>");

                // Tratamento de cada imagem carregada
                int i = 0;

                // Caso existam fotografias, gerir as suas criações
                foreach (var item in fileUploadPhoto)
                {

                    // Garantir que existe uma fotografia em cada iteração do array de fotos carregadas
                    if (item != null)
                    {

                        // Cria um nome para a imagem recebida e guarda a mesma
                        string photoName = DateTime.Now.ToString("_yyyyMMdd_hhmmss") + ".jpg";
                        string photoPath = Path.Combine(Server.MapPath("~/Images/"), photoName);

                        // Cria um objeto photo e adiciona-lhe o nome
                        Photos photo = new Photos
                        {
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
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                // Se chegar aqui é porque o id não é válido
                return RedirectToAction("Index", "UserError", new { error = "We couldn't find this News Article.", details = "The ID was not valid." });
            }

            News News = db.News.Find(id);
            News.Content = News.Content.Replace("<br/>", "\r\n");

            bool author = false;

            if (News != null)
            {
                // Percorrer os autores da noticia e verificar se o utilizador autenticado um deles
                foreach (var user in News.UsersProfileList)
                {
                    if (User.Identity.Name == user.UserName)
                    {
                        author = true;
                    }
                }
            }

            // Se o utilizador autenticado for um dos autores ou tiver permissão, pode editar a noticia
            if (User.IsInRole("Admin") || User.IsInRole("NewsEditor") || author)
            {
                if (News == null)
                {
                    return RedirectToAction("Index", "UserError", new { error = "We couldn't find this News Article.", details = "Check the ID." });
                }
                ViewBag.CategoryFK = new SelectList(db.Categories, "ID", "Name", News.CategoryFK);
                return View(News);
            }

            // Se chegar aqui é porque o utilziador não tem autorização
            return RedirectToAction("Index", "UserError", new { error = "You're not allowed to edit.", details = "You lack enough permission to edit this News article." });
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Content,NewsDate,CategoryFK")] News News)
        {
            if (ModelState.IsValid)
            {
                // Adicionar o HTML para o paragrafo na string
                News.Content = News.Content.Replace("\r\n", "<br/>");
                db.Entry(News).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryFK = new SelectList(db.Categories, "ID", "Name", News.CategoryFK);
            return View(News);
        }


        // GET: ~/News/AddAuthor/{id}
        public ActionResult AddAuthor(int? id)
        {
            bool isAuthor = false;

            if (id != null)
            {
                var news = db.News.Find(id);

                if (news != null)
                {

                    // Remover da SelectList todos os utilizadores que já são autores
                    IQueryable<UsersProfile> Users = db.UsersProfile;

                    // Corre todos os utilizadores que são autores da noticia
                    foreach (var user in news.UsersProfileList)
                    {
                        // Remove cada autor da lista
                        Users = Users.Where(u => u.UserName != user.UserName);

                        // Aproveita e verifica se o utilizador autenticado é um dos autores
                        if (User.Identity.Name == user.UserName) isAuthor = true;
                    }


                    // Autenticação
                    // Verifica se o utilizador autenticado é Admin, NewsEditor ou um dos autores
                    if (User.IsInRole("Admin") || User.IsInRole("NewsEditor") || isAuthor)
                    {
                        // Preparar uma lista com os utilizadores para serem selecionados numa dropdown
                        ViewBag.Users = new SelectList(Users, "ID", "Name");

                        return View(news);
                    }
                    else
                    {
                        // Se chegar aqui é porque o utilziador não tem autorização
                        return RedirectToAction("Index", "UserError", new { error = "You're not allowed to add authors.", details = "You don't have enough permissions." });
                    }
                }
                else
                {
                    return RedirectToAction("Index", "UserError", new { error = "We couldn't find this News Article.", details = "The ID was not valid." });
                }
            }
            else
            {
                return RedirectToAction("Index", "UserError", new { error = "ID not provided." });
            }
        }

        // POST: ~/News/AddAuthor/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAuthor(int? Users, int? newsID)
        {

            if (Users != null && newsID != null)
            {

                var user = db.UsersProfile.Find(Users);
                var actualNews = db.News.Find(newsID);

                if (actualNews != null && user != null)
                {

                    actualNews.UsersProfileList.Add(user);
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = newsID });
                }
            }

            return RedirectToAction("Index");
        }

        // GET: News/RemoveAuthor/{id}
        [Authorize(Roles = "Admin,NewsEditor")]
        public ActionResult RemoveAuthor(int? id)
        {

            if (id != null)
            {

                var news = db.News.Find(id);

                if (news != null)
                {
                    // Preparar uma lista com os utilizadores para serem selecionados numa dropdown
                    ViewBag.Users = new SelectList(news.UsersProfileList, "ID", "Name");

                    return View(news);

                }
                else
                {
                    return RedirectToAction("Index", "UserError", new { error = "We couldn't find this News Article.", details = "The ID was not valid." });
                }
            }
            else
            {
                return RedirectToAction("Index", "UserError", new { error = "ID not provided." });
            }
        }

        // POST: News/RemoveAuthor/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveAuthor(int? Users, int? newsID)
        {

            if (Users != null && newsID != null)
            {
                // Encontra o utilizador e a noticia em questão, usando os IDs que chegaram do form
                var user = db.UsersProfile.Find(Users);
                var actualNews = db.News.Find(newsID);

                if (actualNews != null && user != null)
                {

                    // Remove o utilizador
                    actualNews.UsersProfileList.Remove(user);
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = newsID });
                }
            }

            return RedirectToAction("Index", "UserError", new { error = "We weren't able to remove this Author.", details = "Something went wrong." });
        }

        // GET: News/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "UserError", new { error = "ID not provided." });
            }

            News News = db.News.Find(id);

            if (News == null)
            {
                return RedirectToAction("Index", "UserError", new { error = "We couldn't find this News Article.", details = "The ID was not valid." });
            }

            return View(News);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


            try
            {

                News News = db.News.Find(id);

                // Criar uma lista de photos para serem eliminadas
                // Isto porque correr um foreach diretamente no News.PhotoList iria causar problemas
                // Pois as fotos elimandas estariam a alterar o News.PhotoList o que causaria um erro.
                List<Photos> listPhotos = new List<Photos>();

                // Adicionar todas as fotos à lista
                foreach (var photo in News.PhotosList)
                    listPhotos.Add(photo);

                // Correr a lista e eliminar as fotos
                foreach (var photo in listPhotos)
                {

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
                
                // Remove a noticia
                db.News.Remove(News);
                db.SaveChanges();


            }
            catch (Exception)
            {

                return RedirectToAction("Index", "UserError", new { error = "We weren't able to delete this News Article.", details = "There's still data associated with it." });

            }


            return RedirectToAction("Index");
        }

        // Dropdown list das categorias ------------------------------------------------------

        // GET: Categories for the dropdown list
        [ChildActionOnly]
        public ActionResult CategoriesDropdown()
        {

            // Recolhe todas as categorias
            var categories = db.Categories.ToList();

            // Retorna a PartialView da dropdown
            return PartialView("_categoriesDropdownPartial", categories);
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
