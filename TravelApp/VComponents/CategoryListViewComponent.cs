using Microsoft.AspNetCore.Mvc;
using TravelApp.Data;
using TravelApp.Models;


namespace TravelApp.VComponents
{
    public class CategoryListViewComponent : ViewComponent
    {

        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryListViewComponent(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var categoryList = _applicationDbContext.Categories.Select(t => new CategoryDataModel
            {
                Id = t.Id,
                Name = t.Name,

            }).ToList();



            return View("/Views/Shared/_PartialCategoryList.cshtml", categoryList);

        }

    }
}
