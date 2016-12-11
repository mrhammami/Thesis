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
    public class KitchenOrdersController : Controller
    {
        private Entities db = new Entities();

        // GET: KitchenOrders
        public ActionResult Index(int? levelID = null, int? companyID = null)
        {
            //Összegyűjtjük az összes rendelést, szűrünk
            var results = from rendelesek in db.Orders.AsEnumerable()
                          join cegek in db.CompaniesForUsers.AsEnumerable()
                          on rendelesek.UserID equals cegek.UserID into cegekkel
                          from table3 in cegekkel.DefaultIfEmpty()
                          where (table3 == null ||table3.Company.LevelID == levelID || levelID == null ) &&
                                (table3 == null || table3.CompanyID == companyID || companyID == null)
                          select new Order()
                          {
                              LevelName = table3 != null ? table3.Company.Level.Name : null,
                              UserID = rendelesek.UserID,
                              IsOrdered = rendelesek.IsOrdered,
                              Amount = rendelesek.Amount,
                              DishName = rendelesek.Dish.Name,
                              DishCategoryName = rendelesek.Dish.DishCategory.Name,
                              DishID = rendelesek.DishID,
                              DishCategoryID = rendelesek.Dish.DishCategoryID
                          };

            //csoportosítjük a rendeléseket
            List<Order> endResult = new List<Order>();
            foreach (var item in results)
            {
                if (!endResult.Any(o => o.DishID == item.DishID))
                {
                    Order no = new Order() {
                        DishID = item.DishID,
                        DishName = item.DishName,
                        DishCategoryName = item.DishCategoryName,
                        LevelName = item.LevelName,
                        Amount = results.Where(r => r.DishID == item.DishID).Sum(r => r.Amount),
                        DishCategoryID = item.DishCategoryID
                    };
                    endResult.Add(no);
                }
            }
            endResult = endResult.OrderBy(o => o.LevelName).ThenBy(o => o.DishCategoryID).ToList();

            //Kiegészítő adatk feltöltése nézethez
            ViewBag.Levels = db.Levels.ToList();
            ViewBag.Companies = db.Companies.ToList();
            ViewBag.levelID = levelID;
            ViewBag.companyID = companyID;

            return View(endResult.ToList());
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
