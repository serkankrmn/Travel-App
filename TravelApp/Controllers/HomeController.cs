using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelApp.CommonModels;
using TravelApp.Data;
using TravelApp.HelperService;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            
        }


        public IActionResult Index()
        {
            var relatedSeyahatForSlider = _applicationDbContext.Seyahats.Where(t => t.ShowInSlider == true).OrderByDescending(t => t.Id).ToList();
            var productList = ProductModel.MapFromSeyahatList(relatedSeyahatForSlider);

            HomePageModel model = new HomePageModel
            {
                Products = productList,

            };
            return View(model);
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

    }
}
