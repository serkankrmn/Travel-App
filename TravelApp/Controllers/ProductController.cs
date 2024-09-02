using Microsoft.AspNetCore.Mvc;
using TravelApp.CommonModels;
using TravelApp.Data;
using Microsoft.EntityFrameworkCore;
using TravelApp.Models;
using TravelApp.Data.DomainClasses;
using TravelApp.HelperService;

namespace TravelApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly CustomerServiceHelper _customerServiceHelper;
        private readonly IMailSender _mailSender;

        public ProductController(ApplicationDbContext applicationDbContext, CustomerServiceHelper customerServiceHelper, IMailSender mailSender)
        {
            _applicationDbContext = applicationDbContext;
            _customerServiceHelper = customerServiceHelper;
            _mailSender = mailSender;
        }

        public async Task<IActionResult> CategoryProduct(int categoryId)
        {
            var seyahats = await _applicationDbContext.Seyahats.Where(t => t.CategoryId == categoryId).ToListAsync();

            var products = ProductModel.MapFromSeyahatList(seyahats);

            var model = new CategoryProductsPageModel
            {
                Products = products
            };

            ViewBag.ActiveCategoryId = categoryId;

            return View("/Views/Product/CategoryProducts.cshtml", model);

        }

        public async Task<IActionResult> Detail(int id)
        {

            var seyahatData = await _applicationDbContext.Seyahats.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);
            var product = ProductModel.MapFromSeyahat(seyahatData);

            var model = new DetailPageModel { Product = product };

            return View(model);
        }

        public async Task<IActionResult> Contact(ContactFormModel contactFormModel)
        {


            if (!ModelState.IsValid)
            {
                return PartialView("_PartialContactForm", contactFormModel);
            }
            var suitableCustomerService = await _customerServiceHelper.FindMostSuitableCustomerService();


            CustomerServiceTask customerServiceTask = new CustomerServiceTask
            {
                CustomerServiceId = suitableCustomerService.Id,
                Email = contactFormModel.Email,
                Description = contactFormModel.Description,
                IsComplete = false,
                Name = contactFormModel.Name,
                SeyahatId = contactFormModel.RelatedEmlakId,
                TaskDate = DateTime.Now
            };

            using var tran = await _applicationDbContext.Database.BeginTransactionAsync();



            try
            {
                await _applicationDbContext.CustomerServiceTasks.AddAsync(customerServiceTask);
                await _applicationDbContext.SaveChangesAsync();
                _mailSender.sendMailFORapp("Yeni bir talebiniz var", $"{contactFormModel.RelatedEmlakId} id li emlak için {contactFormModel.Email} kişisi size {contactFormModel.Description} mesajını gönderdi", suitableCustomerService.Email);

                await tran.CommitAsync();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }



            return PartialView("_PartialContactSuccess");
        }

        public IActionResult Index()
        {
            return View();
        }

    }

}
