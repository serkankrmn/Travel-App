namespace TravelApp.HelperService
{
    public interface IProductImageService
    {

        Task<string>SaveProductImage(IFormFile file, int productid, ProductImageTypeEnums productImageType);
    }
}
