
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace BookStore.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public int? Price { get; set; }
        public int? Stock { get; set; }

        public string? Description { get; set; }

        public DateTime? PublishDate { get; set; }

        
        public string ImagePath { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int AuthorId { get; set; }

        public Author? Author { get; set; }
    }
}
