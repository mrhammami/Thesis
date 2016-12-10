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
    public class DishesController : Controller
    {
        private Entities db = new Entities();

        /// <summary>
        /// Az aktuális bejelentkezett user neve.
        /// </summary>
        private string currentUserName { get { return HttpContext.User.Identity.Name; } }

        // GET: Dishes
        public ActionResult Index()
        {
            if (AuthenticationTools.UserHasRole(currentUserName, "StaffMember,Boss,Admin"))
            {
                var dishes = db.Dishes.Include(d => d.DishCategory);
                return View(dishes.ToList());
            }
            return RedirectToAction("Login", "Account");            
        }

        // GET: Dishes/Details/5
        public ActionResult Details(int? id)
        {
            if (AuthenticationTools.UserHasRole(currentUserName, "StaffMember,Boss,Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Dish dish = db.Dishes.Find(id);
                if (dish == null)
                {
                    return HttpNotFound();
                }
                return View(dish);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: Dishes/Create
        public ActionResult Create()
        {
            if (AuthenticationTools.UserHasRole(currentUserName, "StaffMember,Boss,Admin"))
            {
                ViewBag.DishCategoryID = new SelectList(db.DishCategories, "ID", "Name");
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: Dishes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Price,DishCategoryID")] Dish dish)
        {
            if (AuthenticationTools.UserHasRole(currentUserName, "StaffMember,Boss,Admin"))
            {
                if (ModelState.IsValid)
                {
                    db.Dishes.Add(dish);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.DishCategoryID = new SelectList(db.DishCategories, "ID", "Name", dish.DishCategoryID);
                return View(dish);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: Dishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (AuthenticationTools.UserHasRole(currentUserName, "StaffMember,Boss,Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Dish dish = db.Dishes.Find(id);
                if (dish == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DishCategoryID = new SelectList(db.DishCategories, "ID", "Name", dish.DishCategoryID);
                return View(dish);
            }
            return RedirectToAction("Login", "Account");            
        }

        // POST: Dishes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Price,DishCategoryID")] Dish dish)
        {
            if (AuthenticationTools.UserHasRole(currentUserName, "StaffMember,Boss,Admin"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(dish).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.DishCategoryID = new SelectList(db.DishCategories, "ID", "Name", dish.DishCategoryID);
                return View(dish);
            }
            return RedirectToAction("Login", "Account");            
        }

        // GET: Dishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (AuthenticationTools.UserHasRole(currentUserName, "StaffMember,Boss,Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Dish dish = db.Dishes.Find(id);
                if (dish == null)
                {
                    return HttpNotFound();
                }
                return View(dish);
            }
            return RedirectToAction("Login", "Account");            
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (AuthenticationTools.UserHasRole(currentUserName, "StaffMember,Boss,Admin"))
            {
                Dish dish = db.Dishes.Find(id);
                db.Dishes.Remove(dish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account");
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
