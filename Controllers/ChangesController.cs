using AGAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace AGAB.Controllers
{
    public class ChangesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //---------------------------------------------------------------

        // GET: Changes
        public ActionResult Index()
        {
            return View();
        }

        //-----------------------------------------------------------

        /*[HttpPost]
        public ActionResult New(Change changes)
        {
            changes.Date = DateTime.Now;
            try
            {
                db.Changes.Add(changes);
                db.SaveChanges();
                return Redirect("/Articles/Show/" + changes.ArticleId);
            }

            catch (Exception)
            {
                return Redirect("/Articles/Show/" + changes.ArticleId);
            }

        }*/

        //-----------------------------------------------------------

        /*//GET - implicit
        public ActionResult Edit(int id)
        {
            Change changes = db.Changes.Find(id);
            ViewBag.Change = changes;
            return View();
        }

        [HttpPut]
        public ActionResult Edit(int id, Change requestChange)
        {
            try
            {
                Change changes = db.Changes.Find(id);
                if (TryUpdateModel(changes))
                {
                    changes.Content = requestChange.Content;
                    db.SaveChanges();
                }
                return Redirect("/Articles/Show/" + changes.ArticleId);
            }
            catch (Exception)
            {
                return View();
            }

        }*/

        //---------------------------------------------------------------------------------------------------------

        [HttpDelete]
        [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Delete(int id)
        {
            Change comm = db.Changes.Find(id);
            {
                db.Changes.Remove(comm);
                db.SaveChanges();
                return Redirect("/Articles/Show/" + comm.ArticleId);
            }
            /*else
            {
                TempData["message"] = "You don't have the rights to do this!";
                return RedirectToAction("Index", "Articles");
            }*/

        }

        //---------------------------------------------------------------------------------------------------------

        [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Edit(int id)
        {
            Change comm = db.Changes.Find(id);
            
           // if (comm.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            //{
                return View(comm);
            //}
            /*else
            {
                TempData["message"] = "You don't have the rights to do this!";
                return RedirectToAction("Index", "Articles");
            }*/

        }


        [HttpPut]
        [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Edit(int id, Change requestChanges)
        {
            try
            {
                Change comm = db.Changes.Find(id);

               // if (comm.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
                //{
                    if (TryUpdateModel(comm))
                    {
                        comm.Content = requestChanges.Content;
                        db.SaveChanges();
                    }
                    return Redirect("/Articles/Show/" + comm.ArticleId);
               // }
                /*else
                {
                    TempData["message"] = "You don't have the rights to do this!";
                    return RedirectToAction("Index", "Articles");
                }*/
            }
            catch (Exception e)
            {
                return View(requestChanges);
            }
        }
    }
}