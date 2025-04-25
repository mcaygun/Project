
using BookStore.Data.Entities;

namespace BookStore.Service.Services.Abstract
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        bool ProductExists(int id);
    }
}
