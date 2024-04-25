using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArticlesSite.Models;

namespace ArticlesSite.Controllers
{
    
    public class ArtilcesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        // GET: Artilces
        public ActionResult Index()
        {
            var artilces = db.Artilces.Include(a => a.Category);
            return View(artilces.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: Artilces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artilce artilce = db.Artilces.Find(id);
            if (artilce == null)
            {
                return HttpNotFound();
            }
            return View(artilce);
        }

        // GET: Artilces/Create
        public ActionResult Create()
        {
            if(User.IsInRole("Admin") || User.IsInRole("Publisher"))
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
                return View();
            }
            return HttpNotFound();
           
        }

        // POST: Artilces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Artilce artilce, HttpPostedFileBase imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null && imageUrl.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageUrl.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads/Artilce"), fileName);
                    imageUrl.SaveAs(path);
                    artilce.ImageUrl = fileName;
                }

                artilce.Created = DateTime.Now;

                db.Artilces.Add(artilce);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", artilce.CategoryId);
            return View(artilce);
        }

        [Authorize(Roles = "Admin")]
        // GET: Artilces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artilce artilce = db.Artilces.Find(id);
            if (artilce == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", artilce.CategoryId);
            return View(artilce);
        }

        // POST: Artilces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Artilce artilce, HttpPostedFileBase imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null && imageUrl.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageUrl.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads/Artilce"), fileName);
                    imageUrl.SaveAs(path);
                    artilce.ImageUrl = fileName;
                }
                
                db.Entry(artilce).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", artilce.CategoryId);
            return View(artilce);
        }

        // GET: Artilces/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artilce artilce = db.Artilces.Find(id);
            if (artilce == null)
            {
                return HttpNotFound();
            }
            return View(artilce);
        }

        // POST: Artilces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artilce artilce = db.Artilces.Find(id);
            db.Artilces.Remove(artilce);
            db.SaveChanges();
            return RedirectToAction("Index");
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
