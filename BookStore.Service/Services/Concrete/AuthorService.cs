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
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext context;

        public AuthorService(AppDbContext context)
        {
            this.context = context;
        }

        public List<Author> GetAllAuthors()
        {

        return context.Authors.ToList();
        }

        public Author GetAuthorById(int id)
        {
            return context.Authors.First(c => c.Id == id);
        }

        public bool AuthorExists(int id)
        {
            return context.Authors.Any(c => c.Id == id);
        }
    }
}
