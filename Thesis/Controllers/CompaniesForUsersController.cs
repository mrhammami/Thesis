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
    public class CompaniesForUsersController : Controller
    {
        private Entities db = new Entities();

        // GET: CompaniesForUsers
        public ActionResult Index()
        {
            var aspNetUsers = db.AspNetUsers.ToList();
            return View(aspNetUsers.ToList());
        }

        // GET: CompaniesForUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser user = db.AspNetUsers.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.CompanyID = db.CompaniesForUsers.Where(u => u.UserID == id).Select(u => u.CompanyID).FirstOrDefault();
            return View(user);
        }

        // GET: CompaniesForUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser anu = db.AspNetUsers.Where(c => c.Id == id).FirstOrDefault();
            anu.CompanyID = db.CompaniesForUsers.Where(c => c.UserID == id).Select(c => c.CompanyID).FirstOrDefault();
            if (anu == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", anu.CompanyID);
            return View(anu);
        }

        // POST: CompaniesForUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                AspNetUser anu = db.AspNetUsers.Find(aspNetUser.Id);
                
                if (anu != null)            // ha van ilyen user
                {
                    CompaniesForUser cfu = db.CompaniesForUsers.Where(d => d.UserID == aspNetUser.Id).FirstOrDefault();
                    if (aspNetUser.CompanyID != null) // ha van kiválasztva cég
                    {
                        if (cfu != null) // ha van már ilyen bejegyzés
                        {
                            cfu.CompanyID = aspNetUser.CompanyID.Value;
                            db.Entry(cfu).State = EntityState.Modified;
                        }
                        else // ha nincs még ilyen bejegyzés
                        {
                            cfu = new CompaniesForUser() { UserID = aspNetUser.Id, CompanyID = aspNetUser.CompanyID.Value };
                            db.CompaniesForUsers.Add(cfu);
                            db.Entry(cfu).State = EntityState.Added;
                        }
                    }
                    else // ha nincs kiválasztva cég
                    {
                        if (cfu != null) // ha van már bejegyzés
                        {
                            db.CompaniesForUsers.Remove(cfu);
                            db.Entry(cfu).State = EntityState.Deleted;
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", aspNetUser.CompanyID);
            return View(aspNetUser);
        }

        // GET: CompaniesForUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompaniesForUser companiesForUser = db.CompaniesForUsers.Find(id);
            if (companiesForUser == null)
            {
                return HttpNotFound();
            }
            return View(companiesForUser);
        }

        // POST: CompaniesForUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompaniesForUser companiesForUser = db.CompaniesForUsers.Find(id);
            db.CompaniesForUsers.Remove(companiesForUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected SelectList CompaniesSelectList()
        {
            var cl = db.Companies.ToList();
            SelectList sl = new SelectList(cl, "ID", "Name");
            return sl;
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
