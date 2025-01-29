using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // İlişkiyi kurmak için navigasyon özelliği
        public ICollection<Product> Products { get; set; }
    }
}
