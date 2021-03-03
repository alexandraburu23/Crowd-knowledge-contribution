using AGAB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGAB.Controllers
{
    public class ArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private int _perPage = 3;

        //---------------------------------------------------------------------------------------------------------

        // GET: Article
        /* [Authorize(Roles = "User,Editor,Admin")]*/
        public ActionResult Index()
        {
            var articles = db.Articles.Include("Category").Include("User").OrderByDescending(a => a.Date);
           
            //-------------------------------------------------------------------------------------------------
            var search = "";

            if (Request.Params.Get("search")!=null)
            {
                search = Request.Params.Get("search").Trim(); //trim space at beggining and end of the search srting
                //Search in articles (title and content)
                List<int> articlesIds = db.Articles.Where(
                    at => at.Title.Contains(search)
                    || at.Content.Contains(search)
                    ).Select(a => a.Id).ToList();

                //Search in comments (content)
                List<int> changeIds = db.Changes.Where(c => c.Content.Contains(search))
                    .Select(ch => ch.ArticleId).ToList();

                //Search in chapters (content)
                List<int> chapterIds = db.Chapters.Where(c => c.Content.Contains(search))
                    .Select(ch => ch.ArticleId).ToList();

                //Unique list of articles
                List<int> mergeIds = articlesIds.Union(changeIds).ToList();

                //List of articles that contain the search string either in article title, content or comments
                articles = db.Articles.Where(article => mergeIds.Contains(article.Id)).Include("Category").Include("User").OrderBy(a => a.Date);

            }
            

           // -------------------------------------------------------------------------------------------------

            var totalItems = articles.Count();
            var currentPage = Convert.ToInt32(Request.Params.Get("page"));

            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * this._perPage;
            }

            var paginatedArticles = articles.Skip(offset).Take(this._perPage);

            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            //ViewBag.perPage = this._perPage;
            ViewBag.total = totalItems;
            ViewBag.lastPage = Math.Ceiling((float)totalItems / (float)this._perPage);
            ViewBag.Articles = paginatedArticles;

            //!!! For Search Bar 
            ViewBag.SearchString = search;

            return View();
        }

        //---------------------------------------------------------------------------------------------------------

        public ActionResult OrderDescDate()
        {
            var articles = db.Articles.Include("Category").Include("User").OrderByDescending(a => a.Date);
            var search = "";

            if (Request.Params.Get("search") != null)
            {
                search = Request.Params.Get("search").Trim(); //trim space at beggining and end of the search srting
                //Search in articles (title and content)
                List<int> articlesIds = db.Articles.Where(
                    at => at.Title.Contains(search)
                    || at.Content.Contains(search)
                    ).Select(a => a.Id).ToList();

                //Search in comments (content)
                List<int> changeIds = db.Changes.Where(c => c.Content.Contains(search))
                    .Select(ch => ch.ArticleId).ToList();

                //Search in chapters (content)
                List<int> chapterIds = db.Chapters.Where(c => c.Content.Contains(search))
                    .Select(ch => ch.ArticleId).ToList();

                //Unique list of articles
                List<int> mergeIds = articlesIds.Union(changeIds).ToList();

                //List of articles that contain the search string either in article title, content or comments
                articles = db.Articles.Where(article => mergeIds.Contains(article.Id)).Include("Category").Include("User").OrderByDescending(a => a.Date);

            }
            var totalItems = articles.Count();
            var currentPage = Convert.ToInt32(Request.Params.Get("page"));

            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * this._perPage;
            }

            var paginatedArticles = articles.Skip(offset).Take(this._perPage);

            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            //ViewBag.perPage = this._perPage;
            ViewBag.total = totalItems;
            ViewBag.lastPage = Math.Ceiling((float)totalItems / (float)this._perPage);
            ViewBag.Articles = paginatedArticles;

            //!!! For Search Bar 
            ViewBag.SearchString = search;

            return View("IndexDescDate");
        }
        
         //---------------------------------------------------------------------------------------------------------
        
        public ActionResult OrderAscDate()
        {
            var articles = db.Articles.Include("Category").Include("User").OrderBy(a => a.Date);
            var search = "";

            if (Request.Params.Get("search") != null)
            {
                search = Request.Params.Get("search").Trim(); //trim space at beggining and end of the search srting
                //Search in articles (title and content)
                List<int> articlesIds = db.Articles.Where(
                    at => at.Title.Contains(search)
                    || at.Content.Contains(search)
                    ).Select(a => a.Id).ToList();

                //Search in comments (content)
                List<int> changeIds = db.Changes.Where(c => c.Content.Contains(search))
                    .Select(ch => ch.ArticleId).ToList();

                //Search in chapters (content)
                List<int> chapterIds = db.Chapters.Where(c => c.Content.Contains(search))
                    .Select(ch => ch.ArticleId).ToList();

                //Unique list of articles
                List<int> mergeIds = articlesIds.Union(changeIds).ToList();

                //List of articles that contain the search string either in article title, content or comments
                articles = db.Articles.Where(article => mergeIds.Contains(article.Id)).Include("Category").Include("User").OrderBy(a => a.Date);

            }
            var totalItems = articles.Count();
            var currentPage = Convert.ToInt32(Request.Params.Get("page"));

            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * this._perPage;
            }

            var paginatedArticles = articles.Skip(offset).Take(this._perPage);

            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            //ViewBag.perPage = this._perPage;
            ViewBag.total = totalItems;
            ViewBag.lastPage = Math.Ceiling((float)totalItems / (float)this._perPage);
            ViewBag.Articles = paginatedArticles;

            //!!! For Search Bar 
            ViewBag.SearchString = search;

            return View("IndexAscDate");
        }

        //---------------------------------------------------------------------------------------------------------

        public ActionResult OrderDescAZ()
        {
            var articles = db.Articles.Include("Category").Include("User").OrderByDescending(a => a.Title);
            var search = "";

            if (Request.Params.Get("search") != null)
            {
                search = Request.Params.Get("search").Trim(); //trim space at beggining and end of the search srting
                //Search in articles (title and content)
                List<int> articlesIds = db.Articles.Where(
                    at => at.Title.Contains(search)
                    || at.Content.Contains(search)
                    ).Select(a => a.Id).ToList();

                //Search in comments (content)
                List<int> changeIds = db.Changes.Where(c => c.Content.Contains(search))
                    .Select(ch => ch.ArticleId).ToList();

                //Search in chapters (content)
                List<int> chapterIds = db.Chapters.Where(c => c.Content.Contains(search))
                    .Select(ch => ch.ArticleId).ToList();

                //Unique list of articles
                List<int> mergeIds = articlesIds.Union(changeIds).ToList();

                //List of articles that contain the search string either in article title, content or comments
                articles = db.Articles.Where(article => mergeIds.Contains(article.Id)).Include("Category").Include("User").OrderByDescending(a => a.Title);

            }
            var totalItems = articles.Count();
            var currentPage = Convert.ToInt32(Request.Params.Get("page"));

            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * this._perPage;
            }

            var paginatedArticles = articles.Skip(offset).Take(this._perPage);

            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            //ViewBag.perPage = this._perPage;
            ViewBag.total = totalItems;
            ViewBag.lastPage = Math.Ceiling((float)totalItems / (float)this._perPage);
            ViewBag.Articles = paginatedArticles;

            //!!! For Search Bar 
            ViewBag.SearchString = search;

            return View("IndexZA");
        }

        //---------------------------------------------------------------------------------------------------------

        public ActionResult OrderAscAZ()
        {
            var articles = db.Articles.Include("Category").Include("User").OrderBy(a => a.Title);
            var search = "";

            if (Request.Params.Get("search") != null)
            {
                search = Request.Params.Get("search").Trim(); //trim space at beggining and end of the search srting
                //Search in articles (title and content)
                List<int> articlesIds = db.Articles.Where(
                    at => at.Title.Contains(search)
                    || at.Content.Contains(search)
                    ).Select(a => a.Id).ToList();

                //Search in comments (content)
                List<int> changeIds = db.Changes.Where(c => c.Content.Contains(search))
                    .Select(ch => ch.ArticleId).ToList();

                //Search in chapters (content)
                List<int> chapterIds = db.Chapters.Where(c => c.Content.Contains(search))
                    .Select(ch => ch.ArticleId).ToList();

                //Unique list of articles
                List<int> mergeIds = articlesIds.Union(changeIds).ToList();

                //List of articles that contain the search string either in article title, content or comments
                articles = db.Articles.Where(article => mergeIds.Contains(article.Id)).Include("Category").Include("User").OrderBy(a => a.Title);

            }
            var totalItems = articles.Count();
            var currentPage = Convert.ToInt32(Request.Params.Get("page"));

            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * this._perPage;
            }

            var paginatedArticles = articles.Skip(offset).Take(this._perPage);

            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            //ViewBag.perPage = this._perPage;
            ViewBag.total = totalItems;
            ViewBag.lastPage = Math.Ceiling((float)totalItems / (float)this._perPage);
            ViewBag.Articles = paginatedArticles;

            //!!! For Search Bar 
            ViewBag.SearchString = search;

            return View("IndexAZ");
        }

        //---------------------------------------------------------------------------------------------------------
        
        // [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Show(int id)
        {
            Article article = db.Articles.Find(id);

            SetAccessRights();

            /*
            ViewBag.afisareButoane = false;
            if (User.IsInRole("Editor") || User.IsInRole("Admin"))
            {
                ViewBag.afisareButoane = true;
            }

            ViewBag.esteAdmin = User.IsInRole("Admin");
            ViewBag.utilizatorCurent = User.Identity.GetUserId();

            */

            return View(article);

        }
        

        [HttpPost]
       // [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Show(Change comm)
        {
            comm.Date = DateTime.Now;
            comm.UserId = User.Identity.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    db.Changes.Add(comm);
                    db.SaveChanges();
                    return Redirect("/Articles/Show/" + comm.ArticleId);
                }

                else
                {
                    Article a = db.Articles.Find(comm.ArticleId);

                    SetAccessRights();

                    return View(a);
                }

            }

            catch (Exception e)
            {
                Article a = db.Articles.Find(comm.ArticleId);

                SetAccessRights();

                return View(a);
            }

        }

        //---------------------------------------------------------------------------------------------------------

        [Authorize(Roles = "Editor,Admin")]
        public ActionResult New()
        {
            Article article = new Article();

            // preluam lista de categorii din metoda GetAllCategories()
            article.Categ = GetAllCategories();

            // Preluam ID-ul utilizatorului curent
            article.UserId = User.Identity.GetUserId();

            return View(article);
        }


        [HttpPost]
        [Authorize(Roles = "Editor,Admin")]
        [ValidateInput(false)]
        public ActionResult New(Article article)
        {
            article.Date = DateTime.Now;
            article.UserId = User.Identity.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    // Protect content from XSS
                    article.Content = Sanitizer.GetSafeHtmlFragment(article.Content);

                    db.Articles.Add(article);
                    db.SaveChanges();
                    TempData["message"] = "Article added successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    article.Categ = GetAllCategories();
                    return View(article);
                }
            }
            catch (Exception e)
            {
                article.Categ = GetAllCategories();
                return View(article);
            }
        }

        //---------------------------------------------------------------------------------------------------------

        [Authorize(Roles = "Editor,Admin")]
        public ActionResult Edit(int id)
        {
            Article article = db.Articles.Find(id);
            article.Categ = GetAllCategories();

            if (article.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                return View(article);
            }

            else
            {
                TempData["message"] = "You don't have the rights to do this!";
                return RedirectToAction("Index");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Editor,Admin")]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Article requestArticle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Article article = db.Articles.Find(id);

                    if (requestArticle.Protected && !User.IsInRole("Admin"))
                    {
                        TempData["message"] = "Article was protected, so you don't have the rights to edit!";
                        return RedirectToAction("Index");
                    }


                    if (article.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
                    {
                        if (TryUpdateModel(article))
                        {
                            article.Title = requestArticle.Title;
                            
                            // Protect content from XSS
                            requestArticle.Content = Sanitizer.GetSafeHtmlFragment(requestArticle.Content);

                            article.Content = requestArticle.Content;
                            article.CategoryId = requestArticle.CategoryId;
                            db.SaveChanges();
                            TempData["message"] = "Article modified successfully";
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "You don't have the rights to do this!";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    requestArticle.Categ = GetAllCategories();
                    return View(requestArticle);
                }
            }
            catch (Exception e)
            {
                requestArticle.Categ = GetAllCategories();
                return View(requestArticle);
            }
        }

        //---------------------------------------------------------------------------------------------------------

        [HttpDelete]
        [Authorize(Roles = "Editor,Admin")]
        public ActionResult Delete(int id)
        {
            Article article = db.Articles.Find(id);

            if (article.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                db.Articles.Remove(article);
                db.SaveChanges();
                TempData["message"] = "Article Deleted";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "You don't have the rights to do this!";
                return RedirectToAction("Index");
            }     
        }

        //---------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Authorize(Roles = "Editor,Admin")]
        public ActionResult Protect(int id, int change, bool setProtected)
        {
            var articles = db.Articles.Where(article => article.Id == id).ToArray();
            if (articles.Length <= 0)
                return Redirect("/Articles/Show/" + id + "?change=" + change);

            if (articles[0].UserId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
                return Redirect("/Articles/Show/" + id + "?change=" + change);

            foreach (var article in articles)
                article.Protected = setProtected;

            db.SaveChanges();

            return Redirect("/Articles/Show/" + id + "?change=" + change);
        }

        //---------------------------------------------------------------------------------------------------------

        [NonAction]
        public IEnumerable<SelectListItem> GetAllCategories()
        {
            // generam o lista goala
            var selectList = new List<SelectListItem>();

            // extragem toate categoriile din baza de date
            var categories = from cat in db.Categories
                             select cat;

            // iteram prin categorii
            foreach(var category in categories)
            {
                // adaugam in lista elementele necesare pentru dropdown
                selectList.Add(new SelectListItem
                {
                    Value = category.CategoryId.ToString(),
                    Text = category.CategoryName.ToString()
                });
            }
            /*
            foreach (var category in categories)
            {
                var listItem = new SelectListItem();
                listItem.Value = category.CategoryId.ToString();
                listItem.Text = category.CategoryName.ToString();

                selectList.Add(listItem);
            }*/

            // returnam lista de categorii
            return selectList;
        }

        //---------------------------------------------------------------------------------------------------------

        private void SetAccessRights()
        {
            ViewBag.afisareButoane = false;
            if (User.IsInRole("Editor") || User.IsInRole("Admin"))
            {
                ViewBag.afisareButoane = true;
            }

            ViewBag.esteAdmin = User.IsInRole("Admin");
            ViewBag.esteEditor = User.IsInRole("Editor");
            ViewBag.utilizatorCurent = User.Identity.GetUserId();
            //ViewBag.esteConectat = User.IsInRole("Admin") || User.IsInRole("Editor") || User.IsInRole("User");
        }

        /*[AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile(HttpPostedFileBase aUploadedFile)
        {
            var vReturnImagePath = string.Empty;
            if (aUploadedFile.ContentLength > 0)
            {
                var vFileName = Path.GetFileNameWithoutExtension(aUploadedFile.FileName);
                var vExtension = Path.GetExtension(aUploadedFile.FileName);

                string sImageName = vFileName + DateTime.Now.ToString("YYYYMMDDHHMMSS");

                var vImageSavePath = Server.MapPath("/UpImages/") + sImageName + vExtension;
                //sImageName = sImageName + vExtension;  
                vReturnImagePath = "/UpImages/" + sImageName + vExtension;
                ViewBag.Msg = vImageSavePath;
                var path = vImageSavePath;

                // Saving Image in Original Mode  
                aUploadedFile.SaveAs(path);
                var vImageLength = new FileInfo(path).Length;
                //here to add Image Path to You Database ,  
                TempData["message"] = string.Format("Image was Added Successfully");
            }
            return Json(Convert.ToString(vReturnImagePath), JsonRequestBehavior.AllowGet);
        }*/
    }
}