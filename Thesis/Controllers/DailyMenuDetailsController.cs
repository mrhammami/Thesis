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
    public class DailyMenuDetailsController : Controller
    {
        private Entities db = new Entities();

        // GET: DailyMenuDetails
        public ActionResult Index()
        {
            var dailyMenuDetails = db.DailyMenuDetails.Include(d => d.DailyMenuHead).Include(d => d.Dish);
            return View(dailyMenuDetails.ToList());
        }

        // GET: DailyMenuDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyMenuDetail dailyMenuDetail = db.DailyMenuDetails.Find(id);
            if (dailyMenuDetail == null)
            {
                return HttpNotFound();
            }
            return View(dailyMenuDetail);
        }

        // GET: DailyMenuDetails/Create
        public ActionResult Create()
        {
            ViewBag.MenuHeadID = new SelectList(db.DailyMenuHeads, "ID", "ID");
            ViewBag.DishID = new SelectList(db.Dishes, "ID", "Name");
            return View();
        }

        // POST: DailyMenuDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DishID,MenuHeadID")] DailyMenuDetail dailyMenuDetail)
        {
            if (ModelState.IsValid)
            {
                db.DailyMenuDetails.Add(dailyMenuDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuHeadID = new SelectList(db.DailyMenuHeads, "ID", "ID", dailyMenuDetail.MenuHeadID);
            ViewBag.DishID = new SelectList(db.Dishes, "ID", "Name", dailyMenuDetail.DishID);
            return View(dailyMenuDetail);
        }

        // GET: DailyMenuDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyMenuDetail dailyMenuDetail = db.DailyMenuDetails.Find(id);
            if (dailyMenuDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuHeadID = new SelectList(db.DailyMenuHeads, "ID", "ID", dailyMenuDetail.MenuHeadID);
            ViewBag.DishID = new SelectList(db.Dishes, "ID", "Name", dailyMenuDetail.DishID);
            return View(dailyMenuDetail);
        }

        // POST: DailyMenuDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DishID,MenuHeadID")] DailyMenuDetail dailyMenuDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailyMenuDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuHeadID = new SelectList(db.DailyMenuHeads, "ID", "ID", dailyMenuDetail.MenuHeadID);
            ViewBag.DishID = new SelectList(db.Dishes, "ID", "Name", dailyMenuDetail.DishID);
            return View(dailyMenuDetail);
        }

        // GET: DailyMenuDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyMenuDetail dailyMenuDetail = db.DailyMenuDetails.Find(id);
            if (dailyMenuDetail == null)
            {
                return HttpNotFound();
            }
            return View(dailyMenuDetail);
        }

        // POST: DailyMenuDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyMenuDetail dailyMenuDetail = db.DailyMenuDetails.Find(id);
            db.DailyMenuDetails.Remove(dailyMenuDetail);
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
