using BookStore.Data.Context;
using BookStore.Data.Entities;
using BookStore.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext context;
        public CategoryService(AppDbContext context)
        {
            this.context = context;
        }

        public List<Category> GetAllCategories()
        {
            return context.Categories.ToList();
        }


        public Category GetCategoryById(int id)
        {
            return context.Categories.First(c => c.Id == id);
        }

        public bool CategoriesExists(int id)
        {
            return context.Categories.Any(c => c.Id == id);
        }

        
    }
}
