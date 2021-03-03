using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AGAB.Models;
using Newtonsoft.Json;

namespace AGAB.Controllers
{
    public class ChaptersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Capitole
        public ActionResult Index(int id)
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            return View(db.Articles.Find(id));
        }

        //---------------------------------------------------------------------------------------------------------

        //Stergere capitol
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Chapter capt = db.Chapters.Find(id);
            db.Chapters.Remove(capt);
            db.SaveChanges();
            TempData["message"] = "Chapter deleted successfully!";
            return Redirect("/Articles/Show/" + capt.ArticleId);
        }

        //---------------------------------------------------------------------------------------------------------

        //Adaugare capitol nou
        public ActionResult New(int id)
        {
            ViewBag.IdCap = id;
            Chapter capitol = new Chapter();
            return View();
        }


        [HttpPost]
        public ActionResult New(Chapter capt)
        {
            capt.Data = DateTime.Now;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Chapters.Add(capt);
                    db.SaveChanges();
                    JsonConvert.SerializeObject(capt);
                    Debug.WriteLine(JsonConvert.SerializeObject(capt));
                    TempData["message"] = "Chapter added successfully!";
                    return Redirect("/Articles/Show/" + capt.ArticleId);
                }
                else
                {
                    Debug.WriteLine("else");
                    JsonConvert.SerializeObject(capt);
                    Debug.WriteLine(JsonConvert.SerializeObject(capt));
                    ViewBag.IdCap = capt.ArticleId;
                    return View(capt);
                }
            }

            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                ViewBag.IdCap = capt.ArticleId;
                return View(capt);
            }

        }

        //---------------------------------------------------------------------------------------------------------

        //Editare capitol
        public ActionResult Edit(int id)
        {
            Chapter capt = db.Chapters.Find(id);
            //ViewBag.Capitol = capt;
            return View(capt);
        }

        [HttpPut]
        public ActionResult Edit(int id, Chapter requestChapter)
        {
            try
            {
                Chapter capt = db.Chapters.Find(id);
                if (TryUpdateModel(capt))
                {
                    capt.Content = requestChapter.Content;
                    db.SaveChanges();
                    TempData["message"] = "Chapter modified successfully!";
                }
                return Redirect("/Articles/Show/" + capt.ArticleId);
            }
            catch (Exception e)
            {
                return View();
            }

        }

        //---------------------------------------------------------------------------------------------------------

        //Afisare capitol
        public ActionResult Show(int id)
        {
            
            Chapter capitol = db.Chapters.Find(id);
            return View(capitol);

        }


    }
}