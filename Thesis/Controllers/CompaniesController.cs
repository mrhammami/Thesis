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
    public class CompaniesController : Controller
    {
        private Entities db = new Entities();

        public string currentUser { get { return HttpContext.User.Identity.Name; } }

        // GET: Companies
        public ActionResult Index()
        {
            if (!AuthenticationTools.UserHasRole(currentUser, "Boss,Admin"))
                return RedirectToAction("Login", "Account");

            var companies = db.Companies.Include(c => c.State);
            return View(companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (!AuthenticationTools.UserHasRole(currentUser, "Boss,Admin"))
                return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            if (!AuthenticationTools.UserHasRole(currentUser, "Boss,Admin"))
                return RedirectToAction("Login", "Account");

            ViewBag.StatusID = new SelectList(db.States, "ID", "Name");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,StatusID")] Company company)
        {
            if (!AuthenticationTools.UserHasRole(currentUser, "Boss,Admin"))
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                company.StatusID = 1;
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusID = new SelectList(db.States, "ID", "Name", company.StatusID);
            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!AuthenticationTools.UserHasRole(currentUser, "Boss,Admin"))
                return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusID = new SelectList(db.States, "ID", "Name", company.StatusID);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name")] Company company)
        {
            if (!AuthenticationTools.UserHasRole(currentUser, "Boss,Admin"))
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                Company nc = db.Companies.Where(x => x.ID == company.ID).FirstOrDefault();
                nc.Name = company.Name;
                db.Entry(nc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(db.States, "ID", "Name", company.StatusID);
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!AuthenticationTools.UserHasRole(currentUser, "Boss,Admin"))
                return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!AuthenticationTools.UserHasRole(currentUser, "Boss,Admin"))
                return RedirectToAction("Login", "Account");

            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
