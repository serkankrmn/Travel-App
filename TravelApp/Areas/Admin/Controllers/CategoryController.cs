using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Areas.Admin.Models;
using TravelApp.Data;
using TravelApp.Data.DomainClasses;

namespace TravelApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext applicationDbContext, ApplicationDbContext context)
        {
            _applicationDbContext = applicationDbContext;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> New(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            _applicationDbContext.Categories.Add(new Category
            {
                Name = model.Name
            });

            await _applicationDbContext.SaveChangesAsync();


            return RedirectToAction("List", "Category", new { area = "Admin" });
        }


        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }


            var category = await _applicationDbContext.Categories.FindAsync(model.Id);
            category.Name = model.Name;

            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction("List", "Category", new { area = "Admin" });


        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _applicationDbContext.Categories.FindAsync(id);

            return View("new", new CategoryModel
            {
                Id = id,
                Name = category.Name
            });
        }


        public IActionResult Delete(int id, string name)
        {

            return View(new CategoryModel
            {
                Id = id,
                Name = name
            });

        }

        [HttpPost]
        public IActionResult Delete(CategoryModel model)
        {
            Category category = new Category { Id = model.Id };
            _applicationDbContext.Entry<Category>(category).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _applicationDbContext.SaveChanges();

            return RedirectToAction("List", "Category", new { area = "Admin" });


        }


        public async Task<IActionResult> List()
        {

            var categories = await _applicationDbContext.Categories.ToListAsync();

            var model = new CategoryListModel
            {
                Categories = categories
            };

            return View(model);
        }


        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
    }
}
