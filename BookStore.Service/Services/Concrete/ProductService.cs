
using BookStore.Service.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using BookStore.Data.Context;
using BookStore.Data.Entities;

namespace BookStore.Service.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext context;

        public ProductService(AppDbContext context)
        {
            this.context = context;
        }



        public List<Product> GetAllProducts()
        {
            return context.Products.ToList();
        }
        public Product GetProductById(int id)
        {
            return context.Products.First(x => x.Id == id);
        }
        public bool ProductExists(int id)
        {
            return context.Products.Any(x => x.Id == id);
        }











        private async Task ImageUpload(string name, IFormFile imageFile)
        {
            string wwwroot;
            string rootImgFolder = "images";
        }
    }
}
