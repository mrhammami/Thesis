using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thesis.Models;

namespace Thesis.Controllers
{
    public class HomeController : Controller
    {

        static Entities Entities = new Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Írjon nekünk!";

            return View();
        }

        public ActionResult DishList()
        {
            List<Dish> dl = Entities.Dishes.ToList();
            return View(dl);
        }

        /// <summary>
        /// Új étel kreálása.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DishCreate()
        {
            return View(new DishViewModel());
        }

        /// <summary>
        /// Új étel kreálásának jóváhagyása.
        /// </summary>
        /// <param name="newDish">Az új étel.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DishCreate(DishViewModel newDish)
        {
            if (ModelState.IsValid)
            {
                Dish nd = new Dish()
                {
                    Name = newDish.Name,
                    Price = newDish.Price,
                    DishCategoryID = newDish.DishCategoryID
                };
                Entities.Dishes.Add(nd);
                Entities.SaveChanges();
                return RedirectToAction("DishList");
            }
            return View();
        }

        /// <summary>
        /// Az adott étel módosítása.
        /// </summary>
        /// <param name="dishID">A módosítandó étel dishID-ja.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DishEdit(int? dishID)
        {
            if (dishID != null)
            {
                Dish editDish = Entities.Dishes.Where(d => d.ID == dishID).FirstOrDefault();
                DishViewModel dvm = new DishViewModel()
                {
                    ID = editDish.ID,
                    Name = editDish.Name,
                    Price = editDish.Price,
                    DishCategoryID = editDish.DishCategoryID
                };
                if (editDish != null)
                    return View(dvm);
                
            }
            return RedirectToAction("DishList");
        }
        
        /// <summary>
        /// Adott étel módosításának jóváhagyása.
        /// </summary>
        /// <param name="newDish">A módosított étel.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DishEdit(DishViewModel newDish)
        {
            if (ModelState.IsValid)
            {
                var trackedEntity = Entities.Dishes.Find(newDish.ID);
                Entities.Entry(trackedEntity).CurrentValues.SetValues(newDish);
                Entities.Entry(trackedEntity).State = EntityState.Modified;
                Entities.SaveChanges();
                return RedirectToAction("DishList");
            }
            return View();
        }

        /// <summary>
        /// Adott étel törlése.
        /// </summary>
        /// <param name="dishID">Az étel dishID-ja.</param>
        /// <returns></returns>
        public ActionResult DishDelete(int? dishID)
        {
            if (dishID != null && dishID > 0)
            {
                var dishDelete = Entities.Dishes.Where(d => d.ID == dishID).FirstOrDefault();
                if (dishDelete != null)
                {
                    Entities.Entry(dishDelete).State = EntityState.Deleted;
                    Entities.SaveChanges();
                }
            }
            return RedirectToAction("DishList");
        }

        /// <summary>
        /// Visszaadja az ételkategóriákat dropdownlist-hez.
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetDishCategories()
        {
            List<SelectListItem> sli = new List<SelectListItem>();
            var dc = Entities.DishCategories.ToList();
            foreach (var item in dc)
            {
                sli.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString() });
            }
            return sli;
        }
    }
}