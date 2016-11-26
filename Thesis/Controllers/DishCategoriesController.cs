using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Thesis.Models;

namespace Thesis.Controllers
{
    public class DishCategoriesController : Controller
    {
        private Entities db = new Entities();

        // GET: DishCategories
        public ActionResult Index()
        {
            return View(db.DishCategories.ToList());
        }

        // GET: DishCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DishCategory dishCategory = db.DishCategories.Find(id);
            if (dishCategory == null)
            {
                return HttpNotFound();
            }
            return View(dishCategory);
        }

        // GET: DishCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DishCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] DishCategory dishCategory)
        {
            if (ModelState.IsValid)
            {
                db.DishCategories.Add(dishCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dishCategory);
        }

        // GET: DishCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DishCategory dishCategory = db.DishCategories.Find(id);
            if (dishCategory == null)
            {
                return HttpNotFound();
            }
            return View(dishCategory);
        }

        // POST: DishCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] DishCategory dishCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dishCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dishCategory);
        }

        // GET: DishCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DishCategory dishCategory = db.DishCategories.Find(id);
            if (dishCategory == null)
            {
                return HttpNotFound();
            }
            return View(dishCategory);
        }

        // POST: DishCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DishCategory dishCategory = db.DishCategories.Find(id);
            db.DishCategories.Remove(dishCategory);
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
