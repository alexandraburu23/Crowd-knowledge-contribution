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
    
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Category
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            var categories = from category in db.Categories
                             orderby category.CategoryName
                             select category;
            ViewBag.Categories = categories;
            return View();
        }

        //---------------------------------------------------------------------------------------------------------

        public ActionResult Show(int id)
        {
            Category category = db.Categories.Find(id);
            var articles = db.Articles.Include("Category").Include("User").Where(article => article.CategoryId == id).OrderByDescending(article => article.Date);
            ViewBag.Articles = articles;
            SetAccessRights();
            return View(category);
        }

        //---------------------------------------------------------------------------------------------------------

        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            return View();
        }

     
        [HttpPost]
        public ActionResult New(Category cat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Categories.Add(cat);
                    db.SaveChanges();
                    TempData["message"] = "Category added successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(cat);
                }
            }
            catch (Exception e)
            {
                return View(cat);
            }
        }

        //---------------------------------------------------------------------------------------------------------

        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }


        [HttpPut]
        public ActionResult Edit(int id, Category requestCategory)
        {
            try
            {
                    Category category = db.Categories.Find(id);

                    //throw new Exception();

                    if (TryUpdateModel(category))
                    {
                        category.CategoryName = requestCategory.CategoryName;
                        db.SaveChanges();
                        TempData["message"] = "Category modified successfully!";
                        return RedirectToAction("Index");
                    }

                return View(requestCategory);
            }
            catch (Exception e)
            {
                return View(requestCategory);
            }
        }

        //---------------------------------------------------------------------------------------------------------

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            TempData["message"] = "Category deleted!";
            db.SaveChanges();
            return RedirectToAction("Index");
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
            ViewBag.utilizatorCurent = User.Identity.GetUserId();
            //ViewBag.esteConectat = User.IsInRole("Admin") || User.IsInRole("Editor") || User.IsInRole("User");
        }
    }
}
