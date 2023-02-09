using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAndFood.Models;
using Microsoft.AspNetCore.Authorization;

namespace CoreAndFood.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository categoryRepository = new CategoryRepository();

        //[Authorize]
        public IActionResult Index()
        {
            
            return View(categoryRepository.TList());
        }

        [HttpGet]
        public IActionResult CategoryAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoryAdd(Category p)
        {
          
            if (!ModelState.IsValid)    //Boş Değilse Category Name Boş Geçilemez Modelde Belirtildi...
            {
                return View("CategoryAdd");
            }
            categoryRepository.TAdd(p);
            return RedirectToAction("Index");
        }

        public IActionResult CategoryFind(int id)   //Update için veriler çağrıldı
        {
            var x = categoryRepository.TFind(id);
            Category ct = new Category()        //nesnenin içine değerler aktarıldı
            {
                CategoryName=x.CategoryName,
                CategoryDescription=x.CategoryDescription,
                CategoryID=x.CategoryID
            };
            return View(ct);                    //değerler ekranda görüntülendi.
        }

        [HttpPost]
        public IActionResult CategoryUpdate(Category p)
        {
            var x = categoryRepository.TFind(p.CategoryID);      //parametreden gelen categoryId arandı      
            x.CategoryName = p.CategoryName;                       
            x.CategoryDescription = p.CategoryDescription;
            x.Status = true;
            categoryRepository.TUpdate(x);
            return RedirectToAction("Index");

        }

        public IActionResult CategoryDelete(int id)
        {
            var x = categoryRepository.TFind(id);
            x.Status = false;
            categoryRepository.TDelete(x);
            return RedirectToAction("Index");
        }
    }
}
