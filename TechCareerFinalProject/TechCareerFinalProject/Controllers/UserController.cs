using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechCareerFinalProject.Areas.Identity.User;
using TechCareerFinalProject.Data;
using TechCareerFinalProject.Models;

namespace TechCareerFinalProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<AppUser> um;

        public UserController(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            db = dbContext;
            um = userManager;
        }
        public IActionResult Index()
        {
            var currentUser = um.GetUserAsync(User).Result;
            if (currentUser != null)
            {
                string currentUserId = currentUser.Id;
                var list = db.Lists.Where(l => l.UserId == currentUserId).ToList();
                return View(list);
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AddList()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddList(List list)
        {
            var currrentUser = um.GetUserAsync(User).Result;
            if (currrentUser != null)
            {
                var newList = new List { Name = list.Name, UserId = currrentUser.Id };
                db.Lists.Add(newList);
                db.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            return View(list);

        }
        public IActionResult ListDetail(int id)
        {
            var listWithProducts=db.ListDetails.Include(l=>l.Products).FirstOrDefault(l=>l.ID==id);
            if (listWithProducts == null)
            {
                return NotFound();
            }
            return View(listWithProducts);
        }

        public IActionResult AddProductToList(int listId)
        {
            var productList = db.Products.Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Name }).ToList();

            var viewModel = new AddProductToListViewModel
            {
                ListId = listId,
                ProductList = productList
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddProductToList(AddProductToListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newProduct = db.Products.FirstOrDefault(p => p.ID == viewModel.SelectedProductId);

                if (newProduct != null)
                {
                    var list = db.ListDetails.FirstOrDefault(l => l.ID == viewModel.ListId);

                    if (list != null)
                    {
                        list.Products.Add(newProduct);
                        db.SaveChanges();
                    }

                    return RedirectToAction("ListDetail", new { id = viewModel.ListId });
                }
            }
            viewModel.ProductList = db.Products
        .Select(p => new SelectListItem
        {
            Value = p.ID.ToString(),
            Text = p.Name
        })
        .ToList();

            return View(viewModel);
        }
    }
}
