using System.ComponentModel.DataAnnotations;
using TravelApp.Areas.Admin.Models;
using TravelApp.Data.DomainClasses;

namespace TravelApp.CommonModels
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık alanı gereklidir.")]
        public string Header { get; set; }

        public string MiniDescription { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Kategori")]
        public CategoryModel Category { get; set; }

        [Display(Name = "Resim")]
        public IFormFile ProductImage { get; set; }


        public string ImagePath { get; set; }

        [Display(Name = "Resim")]
        public IFormFile ProductSliderImage { get; set; }


        public string ImagePathSlider { get; set; }


        [Display(Name = "Anasayfada gözüksün mü")]
        public bool ShowMainPage { get; set; }


        [Display(Name = "Sliderda gözüksün mü")]
        public bool ShowInSlider { get; set; }


        public static List<ProductModel> MapFromSeyahatList(List<Seyahat> seyahatList)
        {
            List<ProductModel> productList = new List<ProductModel>();

            foreach (var item in seyahatList)
            {
                productList.Add(MapFromSeyahat(item));
            }
            return productList;

        }

        public static ProductModel MapFromSeyahat(Seyahat t)
        {
            var product = new ProductModel
            {
                Id = t.Id,
                Header = t.Header,
                MiniDescription = t.MiniDescription,
                Description = t.Description,
                ImagePath = t.ImagePath,
                ShowMainPage = t.ShowMainPage,
                ShowInSlider = t.ShowInSlider,
                ImagePathSlider = t.ImagePathSlider,
                CategoryId = t.CategoryId,
                // Category'yi null kontrolü ile bu şekilde ayarlayalım
                Category = t.Category != null ? CategoryModel.MapFromCategory(t.Category) : null
            };
            return product;
        }


        //başka hali

        //public static ProductModel MapFromSeyahat(Seyahat t)
        //{
        //    var product = new ProductModel
        //    {
        //        Id = t.Id,
        //        Header = t.Header,
        //        MiniDescription = t.MiniDescription,
        //        Description = t.Description,
        //        ImagePath = t.ImagePath,
        //        ShowMainPage = t.ShowMainPage,
        //        ShowInSlider = t.ShowInSlider,
        //        ImagePathSlider = t.ImagePathSlider,
        //        CategoryId = t.CategoryId,
        //        //Category = t.Category != null ? new CategoryModel
        //        //{
        //        //    Id = t.Category.Id,
        //        //    Name = t.Category.Name
        //        //} : null
        //    };
        //    if (t.CategoryId != null )
        //    {
        //        product.Category = CategoryModel.MapFromCategory(t.Category);
        //    }
        //    return product;
        //}

    }
}
