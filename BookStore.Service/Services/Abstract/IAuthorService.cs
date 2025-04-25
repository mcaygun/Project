using BookStore.Service.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using BookStore.Data.Context;
using BookStore.Data.Entities;

namespace BookStore.Service.Services.Abstract
{
    public interface IAuthorService
    {
        List<Author> GetAllAuthors();
        Author GetAuthorById(int id);
        bool AuthorExists(int id);
    }
}
