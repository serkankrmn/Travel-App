using System.ComponentModel.DataAnnotations;
using TravelApp.Data.DomainClasses;

namespace TravelApp.Areas.Admin.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Bu Alan Zorunludur")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }

        public static CategoryModel MapFromCategory(Category category)
        {
            return new CategoryModel { Id = category.Id, Name = category.Name };
        }
    }
}
