
using BookStore.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace BookStore.Data.ViewModels.Products
{
    public class ProductAddViewModel
    { //Model klasörünün içindeki product classının aynı proplarını buraya yazıyoruz.



        [Required(ErrorMessage = "İsim Alanı Boş olamaz")] //Required boş geçilemez anlamına geliyor.
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girilebilir.")]
        public string? Name { get; set; }


         //[RegularExpression(@"^[0-9]+(\.[0-9]{1-2})",ErrorMessage ="Fiyat alanında noktadan sonra en fazla 2 basamak olmalıdır.")] 
         [Required(ErrorMessage = "Fiyat Alanı Boş olamaz")]
         [Range(1, 1000, ErrorMessage = "Ürün Fiyatı 1 ile 1000 arasında bir değer olmalıdır.")]
        public int? Price { get; set; }

        [Required(ErrorMessage = "Stok Alanı Boş olamaz")]
        [Range(1, 200, ErrorMessage = "Stok Alanı 1 ile 200 arasında bir değer olmalıdır.")] //TAM değerlerde add.cshtml kısmında type kısmına number ver.
        public int? Stock { get; set; }

        [Required(ErrorMessage = "Acıklama Alanı Boş olamaz")]
        [StringLength(300, MinimumLength = 50, ErrorMessage = "Açıklama alanı 50 ile 300 karakter arasında olabilir.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Yayınlanma Tarihi Boş olamaz")]
        public DateTime? PublishDate { get; set; }

        //  [Required(ErrorMessage = "Renk Seçimi Boş olamaz")]
        //   public string? Color { get; set; }

        //  public bool IsPublish { get; set; }

        //  [Required(ErrorMessage = "Süre  Alanı Boş olamaz")]
        //  public int? Expire { get; set; }


        public IFormFile Image { get; set; }
        public string? ImagePath { get; set; }  // Bu satırı ekliyoruz
        public int CategoryId { get; set; }


        public int AuthorId { get; set; }

    }
}
