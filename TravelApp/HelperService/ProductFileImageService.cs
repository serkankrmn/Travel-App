
namespace TravelApp.HelperService
{
    public class ProductFileImageService : IProductImageService
    {

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public ProductFileImageService(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveProductImage(IFormFile file, int productid, ProductImageTypeEnums productImageType)
        {

        


            string productDirectoryPath = Path.Combine(_environment.WebRootPath, "ProductImages", productid.ToString());

            if (!Directory.Exists(productDirectoryPath))
            {
                Directory.CreateDirectory(productDirectoryPath);
            }


            if (productImageType == ProductImageTypeEnums.SLIDER)
            {

                var sliderDirectoryPath = Path.Combine(_environment.WebRootPath, "ProductImages", productid.ToString(), "Sliders");

                if (!Directory.Exists(sliderDirectoryPath))
                {
                    Directory.CreateDirectory(sliderDirectoryPath);
                }
            }




            string productImagePath = productImageType == ProductImageTypeEnums.PRODUCT ? Path.Combine(_environment.WebRootPath, "ProductImages", productid.ToString(), file.FileName) : Path.Combine(_environment.WebRootPath, "ProductImages", productid.ToString(), "Sliders", file.FileName);


            string productImageAbsolurePath = productImageType == ProductImageTypeEnums.PRODUCT ? Path.Combine("/ProductImages", productid.ToString(), file.FileName) : Path.Combine("/ProductImages", productid.ToString(), "Sliders", file.FileName);

            

            using (Stream fileStream = new FileStream(productImagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return productImageAbsolurePath;


        }
    }
}
