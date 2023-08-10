using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechCareerFinalProject.Data;
using TechCareerFinalProject.Models;

namespace TechCareerFinalProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;

        public AdminController(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        
        public IActionResult ListCategories()
        {

            var categories = db.Categories.ToList();
            return View(categories);

        }
        //////////////////////////Kategori Ekle//////////////////////////
        
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("ListCategories");
            
        }
        /////////////////////////////////////////////////////////////////


        //////////////////////////Kategori Güncelle//////////////////////////
        
        public IActionResult EditCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            var existingCategory = db.Categories.Find(category.ID);
            if(existingCategory == null)
            {
                return NotFound();
            }
            existingCategory.Name= category.Name;
            db.SaveChanges();
            return RedirectToAction("ListCategories");
        }

        ////////////////////////////////////////////////////
        
        ///////////// Kategoriyi Sil //////////////////////
        
        public IActionResult DeleteCategory(int id)
        {
            var category=db.Categories.Find(id);
            if(category == null)
            {
                return NotFound();
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("ListCategories");
        }
        ////////////////////////////////////////////////
        
        ////////////// Product CRUD İşlemleri
        public IActionResult ListProducts()
        {
            var products = db.Products.ToList();
            return View(products);
        }

        public IActionResult AddProduct()
        {
            var categories = db.Categories.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();
            var vievModel = new ProductViewModel
            {
                Categories = new SelectList(categories,"Value","Text")
            };
            return View(vievModel);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product,IFormFile file)
        {
            try
            {
                if(file == null || file.Length == 0)
                {
                    return BadRequest("Yüklenen dosya bulunamadı");
                } 
                using(var memoryStream=new MemoryStream())
                {
                    file.CopyTo(memoryStream);

                    byte[] imageData = memoryStream.ToArray();
                    string base64String = Convert.ToBase64String(imageData);

                    product.Image = base64String;
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                return RedirectToAction("ListProducts");
            }
            catch (Exception ex)
            {
                return BadRequest("Veri Yüklenemedi");
            }
        }
        public IActionResult EditProduct(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);


        }

        [HttpPost]
        public IActionResult EditProduct(Product product, IFormFile file)
        {
            var existingProduct = db.Products.Find(product.ID);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = product.Name;
            existingProduct.CategoryID = product.CategoryID;

            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);

                    byte[] imageData = memoryStream.ToArray();
                    existingProduct.Image = Convert.ToBase64String(imageData);
                }
            }
            db.SaveChanges();

            return RedirectToAction("ListProducts");
        }



        public IActionResult DeleteProduct(int id)
        {
            var product = db.Products.Find(id);
            if(product == null)
            {
                return NotFound();
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ListProducts");
        }
           
    }
}
