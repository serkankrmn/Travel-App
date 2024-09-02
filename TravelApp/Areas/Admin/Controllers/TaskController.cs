using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Areas.Admin.Common;
using TravelApp.Areas.Admin.Models;
using TravelApp.CommonModels;
using TravelApp.Data;

namespace TravelApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public TaskController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpGet]
        public async Task<IActionResult> GetTaskList(string email, CustomerTaskStatus taskStatus, int? seyahatid)
        {

            var data = _applicationDbContext.CustomerServiceTasks.Include(t => t.CustomerService).Include(t => t.Seyahat).AsQueryable();

            if (!string.IsNullOrEmpty(email))
            {

                data = data.Where(t => t.Email == email);
            }


            if (taskStatus != CustomerTaskStatus.ALL)
            {
                bool status = taskStatus == CustomerTaskStatus.COMPLETED ? true : false;
                data = data.Where(t => t.IsComplete == status);
            }


            if (seyahatid > 0)
            {
                data = data.Where(t => t.SeyahatId == seyahatid);
            }

            var pageModel = data.OrderByDescending(t => t.TaskDate).Select(t => new CustomerTaskModel
            {
                CreateDate = t.TaskDate,
                CustomerServiceId = t.CustomerServiceId,
                CustomerServiceName = t.CustomerService.Name,
                Description = t.Description,
                SeyahatName = t.Seyahat.Header,
                SeyahatId = t.SeyahatId,
                Email = t.Email,
                Id = t.Id,
                IsCompleted = t.IsComplete

            }).ToList();


            var model = new CustomerServiceListPageModel
            {
                ServiceList = pageModel
            };

            return PartialView("_PartialCustomerServiceList", model);
        }


        public async Task<IActionResult> CompleteTask(int id)
        {


            var relatedTask = await _applicationDbContext.CustomerServiceTasks.FindAsync(id);
            relatedTask.IsComplete = true;
            await _applicationDbContext.SaveChangesAsync();


            var pageModel = _applicationDbContext.CustomerServiceTasks.Include(t => t.CustomerService).Include(t => t.Seyahat).Select(t => new CustomerTaskModel
            {
                CreateDate = t.TaskDate,
                CustomerServiceId = t.CustomerServiceId,
                CustomerServiceName = t.CustomerService.Name,
                Description = t.Description,
                SeyahatName = t.Seyahat.Header,
                SeyahatId = t.SeyahatId,
                Email = t.Email,
                Id = t.Id,
                IsCompleted = t.IsComplete

            }).First(t => t.Id == id);

            return PartialView("_PartialCustomerServiceTaskItem", pageModel);



        }

        public IActionResult List()
        {
            ViewBag.AllSeyahatList = _applicationDbContext.Seyahats.Select(t => new ProductModel
            {
                Id = t.Id,
                Header = t.Header
            }).ToList();

            return View();
        }
    }
}
