using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAndFood.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace CoreAndFood.Controllers
{
    public class FoodController : Controller
    {
        Context context = new Context();
        FoodRepository foodRepository = new FoodRepository();
        public IActionResult Index(int page=1)
        {
           
            return View(foodRepository.TList("category").ToPagedList(page,3));
        }

        [HttpGet]
        public IActionResult FoodAdd()
        {
            List<SelectListItem> values = (from x in context.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
            return View();
        }

        public IActionResult FoodAdd(Food p)
        {
            foodRepository.TAdd(p);
            return RedirectToAction("Index");
        }

        public IActionResult FoodDelete(int id)
        {
            foodRepository.TDelete(new Food {FoodID = id});
            return RedirectToAction("Index");
        }

        public IActionResult FoodFind(int id)
        {
            List<SelectListItem> values = (from y in context.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.CategoryName,
                                               Value = y.CategoryID.ToString()
                                           }).ToList();
            ViewBag.v1 = values;



            var x = foodRepository.TFind(id);
            
            Food f = new Food()
            {
                FoodID=x.FoodID,
                category = x.category,
                Description = x.Description,
                Name = x.Name,
                ImageURL = x.ImageURL,
                Price = x.Price,
                Stock = x.Stock
            };
            return View(f);
        }

        [HttpPost]
        public IActionResult FoodUpdate(Food p)
        {
            var x = foodRepository.TFind(p.FoodID);
            x.Name = p.Name;
            x.Price = p.Price;
            x.Description = p.Description;
            x.ImageURL = p.ImageURL;
            x.Stock = p.Stock;
            x.CategoryID = p.CategoryID;
            foodRepository.TUpdate(x);
            return RedirectToAction("Index");




        }
    }
}
