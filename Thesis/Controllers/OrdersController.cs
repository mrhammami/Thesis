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
    public class OrdersController : Controller
    {
        private Entities db = new Entities();

        /// <summary>
        /// A menü és rendelések listája, kategóriánként.
        /// Ha nincs megadva kategória, akkor mind megjelenik.
        /// </summary>
        /// <param name="dishCategory">A kért ételkategória.</param>
        /// <returns>Lista nézet.</returns>
        // GET: Orders
        public ActionResult Index(int? dishCategory = null)
        {
            //Ha nincs erre a napra még menü, akkor csinálunk egyet, DEBUG miatt
            DateTime dt = DateTime.Now.Date;
            if (!db.DailyMenuHeads
                .Where(d => DbFunctions.TruncateTime(d.MenuDate) == dt)
                .Any())
            {
                DailyMenuHead mh = new DailyMenuHead() { MenuDate = DateTime.Now, StatusID = 1 };
                db.DailyMenuHeads.Add(mh);
                db.Entry(mh).State = EntityState.Added;
                db.SaveChanges();
                var menuDetailsToAdd = db.Dishes
                    .Where(d => d.ID > 2 && d.ID < 41)
                    .ToList();
                foreach (var item in menuDetailsToAdd)
                {
                    DailyMenuDetail md = new DailyMenuDetail()
                    {
                        MenuHeadID = mh.ID,
                        DishID = item.ID
                    };
                    db.DailyMenuDetails.Add(md);
                    db.Entry(md).State = EntityState.Added;
                }
                db.SaveChanges();
            }


            OrdersAndMenuDetailsViewModel omdModel = new OrdersAndMenuDetailsViewModel();
            omdModel.Orders = db.Orders
                .Where(o => o.IsOrdered == false && 
                    DbFunctions.TruncateTime(o.DailyMenuHead.MenuDate) == dt) // ide még kell a user
                .Include(o => o.DailyMenuHead)
                .Include(o => o.Dish)
                .Include(o => o.User)
                .OrderBy(d => d.Dish.DishCategoryID);
            omdModel.MenuDetails = db.DailyMenuDetails
                .Include(d => d.DailyMenuHead)
                .Include(d => d.Dish)
                .Where(d => ((d.Dish.DishCategoryID == dishCategory) || (dishCategory == null)) &&
                    DbFunctions.TruncateTime(d.DailyMenuHead.MenuDate) == dt)
                .OrderBy(d => d.Dish.DishCategoryID);
            omdModel.DisplayDishCategories = db.DishCategories
                .OrderBy(d => d.ID)
                .ToList();
            return View(omdModel);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)    
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.MenuHeadID = new SelectList(db.DailyMenuHeads, "ID", "ID");
            ViewBag.DishID = new SelectList(db.Dishes, "ID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderConfirm()
        {
            //Mivel minden bent van az adatbázisban,
            //mindössze át kell állítani az IsOrdered kapcsolót
            //Usert httpcontext-ből, menuhead-et dátumból
            List<Order> otm = db.Orders.Where(i => i.UserID == 2 && i.MenuHeadID == 1).ToList();
            //frissítésük
            foreach (var item in otm)
            {
                item.IsOrdered = true;
                db.Entry(item).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,DishID,MenuHeadID")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuHeadID = new SelectList(db.DailyMenuHeads, "ID", "ID", order.MenuHeadID);
            ViewBag.DishID = new SelectList(db.Dishes, "ID", "Name", order.DishID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", order.UserID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuHeadID = new SelectList(db.DailyMenuHeads, "ID", "ID", order.MenuHeadID);
            ViewBag.DishID = new SelectList(db.Dishes, "ID", "Name", order.DishID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", order.UserID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,DishID,MenuHeadID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuHeadID = new SelectList(db.DailyMenuHeads, "ID", "ID", order.MenuHeadID);
            ViewBag.DishID = new SelectList(db.Dishes, "ID", "Name", order.DishID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", order.UserID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Hozzáadás mai kosárhoz
        /// </summary>
        /// <param name="dishID">A hozzáadandó étel ID-ja.</param>
        /// <returns></returns>
        public ActionResult AddToCart(int? dishID = null)
        {
            AspNetUser currentUser = db.AspNetUsers.Where(u => u.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            int? mh = db.DailyMenuHeads.Select(m => m.ID).LastOrDefault();
            if (dishID.HasValue && mh.HasValue && currentUser != null)
            {
                Order order = new Order()
                {
                    Amount = 1,
                    DishID = dishID.Value,
                    IsOrdered = false,
                    OrderDate = DateTime.Now,
                    UserID = 2,
                    MenuHeadID = mh.Value
                };
                db.Orders.Add(order);
                db.Entry(order).State = EntityState.Added;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// A rendelés mennyiségének módosítása.
        /// </summary>
        /// <param name="updatedOrder">A módosítandó rendeléstétel.</param>
        /// <returns></returns>
        public ActionResult UpdateCartAmount(Order updatedOrder)
        {
            var modifiedOrder = db.Orders.Where(o => o.ID == updatedOrder.ID).FirstOrDefault();
            if (updatedOrder != null &&
                modifiedOrder != null &&
                0 < updatedOrder.Amount && 
                updatedOrder.Amount < 100 )
            {
                modifiedOrder.Amount = updatedOrder.Amount;
                db.Entry(modifiedOrder).State = EntityState.Modified;
                db.SaveChanges();
                // A teljes összeg újraszámolása
                int priceSum = db.Orders.Where(o => o.MenuHeadID == modifiedOrder.MenuHeadID && o.IsOrdered==false).Sum(o => o.Amount * o.Dish.Price);
                return Json(new { PriceFor = modifiedOrder.Amount*modifiedOrder.Dish.Price,   status = "Success", message = "Success", priceSum = priceSum });
            }
            return Json(new { status = "Error", message = "Nem érvényes adatok" });
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
