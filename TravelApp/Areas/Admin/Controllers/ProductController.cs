using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Areas.Admin.Models;
using TravelApp.CommonModels;
using TravelApp.Data;
using TravelApp.Data.DomainClasses;
using TravelApp.HelperService;
using TravelApp.Models;

namespace TravelApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostEnvironment;
        private readonly IProductImageService _productImageService;
        private readonly CustomerServiceHelper _customerServiceHelper;
        private readonly IMailSender _mailSender;



        public ProductController(ApplicationDbContext applicationDbContext, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostEnvironment, IProductImageService productImageService, CustomerServiceHelper customerServiceHelper, IMailSender mailSender)
        {
            _applicationDbContext = applicationDbContext;
            _hostEnvironment = hostEnvironment;
            _productImageService = productImageService;
            _customerServiceHelper = customerServiceHelper;
            _mailSender = mailSender;
        }

        public async Task<IActionResult> List()
        {
            //var seyahatList = await _applicationDbContext.Seyahats.ToListAsync();
            var seyahatList = await _applicationDbContext.Seyahats
                .Include(t => t.Category)
                .ToListAsync();
            
            var model = new ProductListModel();
            model.Products = ProductModel.MapFromSeyahatList(seyahatList);

           

            return View(model);
        }



        public async Task<IActionResult> New()
        {

            var categories = await _applicationDbContext.Categories.Select(t => new CategoryModel
            {
                Id = t.Id,
                Name = t.Name,
            }).ToListAsync();

            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewoLD(ProductModel productModel)
        {

            // wwwroot --> D:\Documents\Microsoft Visual Studio\TravelApp\TravelApp\wwwroot\ProductImages\2.png

            string productImagePath = Path.Combine(_hostEnvironment.WebRootPath, "ProductImages", productModel.ProductImage.FileName);
            string productImageAbsolurePath = Path.Combine("/ProductImages", productModel.ProductImage.FileName);

            //_hostEnvironment.ContentRootPath + "/wwwroot/" + productModel.ProductImage.Name;

            using (Stream fileStream = new FileStream(productImagePath, FileMode.Create))
            {
                await productModel.ProductImage.CopyToAsync(fileStream);
            }



            string imagePathSliderAbsolutePath = null;

            if (productModel.ShowInSlider)
            {
                string productImageSliderPath = Path.Combine(_hostEnvironment.WebRootPath, "ProductImages", productModel.ProductSliderImage.FileName);

                using (Stream fileStream = new FileStream(productImageSliderPath, FileMode.Create))
                {
                    await productModel.ProductSliderImage.CopyToAsync(fileStream);
                }


                imagePathSliderAbsolutePath = Path.Combine("/ProductImages", productModel.ProductImage.FileName);
                ;
            }



            Seyahat seyahat = new Seyahat
            {

                CategoryId = productModel.CategoryId,
                Description = productModel.Description,
                Header = productModel.Header,
                ImagePath = productImageAbsolurePath,
                MiniDescription = productModel.MiniDescription,
                ShowInSlider = productModel.ShowInSlider,
                ShowMainPage = productModel.ShowMainPage,
                ImagePathSlider = imagePathSliderAbsolutePath
            };


            _applicationDbContext.Seyahats.Add(seyahat);
            await _applicationDbContext.SaveChangesAsync();


            return RedirectToAction("List", "Product", new { area = "Admin" });
        }




        [HttpPost]
        public async Task<IActionResult> New(ProductModel productModel)
        {
            using var tran = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                var seyahat = new Seyahat
                {
                    CategoryId = productModel.CategoryId,
                    Description = productModel.Description,
                    Header = productModel.Header,
                    ImagePath = "",
                    MiniDescription = productModel.MiniDescription,
                    ShowInSlider = productModel.ShowInSlider,
                    ShowMainPage = productModel.ShowMainPage,
                    ImagePathSlider = ""
                };

                _applicationDbContext.Seyahats.Add(seyahat);
                await _applicationDbContext.SaveChangesAsync();

                seyahat.ImagePath = await _productImageService.SaveProductImage(productModel.ProductImage, seyahat.Id, ProductImageTypeEnums.PRODUCT);

                if (productModel.ShowInSlider)
                {
                    seyahat.ImagePathSlider = await _productImageService.SaveProductImage(productModel.ProductSliderImage, seyahat.Id, ProductImageTypeEnums.SLIDER);
                }

                await _applicationDbContext.SaveChangesAsync();
                await tran.CommitAsync();
            }
            catch (Exception)
            {
                await tran.RollbackAsync();
                throw;
            }

            return RedirectToAction("List", "Product", new { area = "Admin" });
        }

        public async Task<IActionResult> Edit(int id) //Edit GET'i bu
        {
            var seyahatData = await _applicationDbContext.Seyahats.FindAsync(id);
            var model = ProductModel.MapFromSeyahat(seyahatData);

            var categories = await _applicationDbContext.Categories.Select(t => new CategoryModel
            {
                Id = t.Id,
                Name = t.Name,
            }).ToListAsync();

            ViewBag.Categories = categories;

            return View("New", model);


        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel model)
        {


            string productNewImagePath = null,
                  productSliderImagePath = null;

            if (model.ProductImage != null)
                productNewImagePath = await _productImageService.SaveProductImage(model.ProductImage, model.Id, ProductImageTypeEnums.PRODUCT);


            if (model.ProductSliderImage != null)
                productSliderImagePath = await _productImageService.SaveProductImage(model.ProductSliderImage, model.Id, ProductImageTypeEnums.SLIDER);



            var seyahatData = await _applicationDbContext.Seyahats.FindAsync(model.Id);


            seyahatData.Header = model.Header;
            seyahatData.MiniDescription = model.MiniDescription;
            seyahatData.Description = model.Description;
            seyahatData.CategoryId = model.CategoryId;
            seyahatData.ImagePath = productNewImagePath != null ? productNewImagePath : seyahatData.ImagePath;
            seyahatData.ImagePathSlider = productSliderImagePath != null ? productSliderImagePath : seyahatData.ImagePathSlider;
            seyahatData.ShowInSlider = model.ShowInSlider;
            seyahatData.ShowMainPage = model.ShowMainPage;

            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("List", "Product", new { area = "Admin" });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            // Önce veritabanından ürün çekilir
            var seyahat = await _applicationDbContext.Seyahats.FindAsync(id);

            if (seyahat == null)
            {
                return NotFound(); // Ürün bulunamadıysa 404
            }

            // Veritabanından siliyoruz
            _applicationDbContext.Seyahats.Remove(seyahat);
            await _applicationDbContext.SaveChangesAsync();

            // Ürünü silme işleminden sonra liste sayfasına yönlendirilir
            return RedirectToAction("List", "Product", new { area = "Admin" });
        }

    }

}
