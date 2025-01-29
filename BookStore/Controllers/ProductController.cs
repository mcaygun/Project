using BookStore.Data.Context;
using BookStore.Data.Entities;
using BookStore.Data.ViewModels.Products;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        

        public IActionResult Index(List<string> categoryNames, List<string> authorNames)
        {
            // Ürünleri çekiyoruz ve kategori ve Yazar bilgilerini yüklüyoruz
            var products = _context.Products
                .Include(p => p.Category) // Kategori bilgilerini de yüklüyoruz
                .Include(p => p.Author) // Yazar bilgilerini de yüklüyoruz 
                .AsQueryable();

            // Kategori filtreleme işlemi
            if (categoryNames != null && categoryNames.Any())
            {
                products = products.Where(p => categoryNames.Contains(p.Category.Name));
            }

            // Yazar filtreleme işlemi
            if (authorNames != null && authorNames.Any())
            {
                products = products.Where(p => authorNames.Contains(p.Author.Name));
            }

            // Sonuçları sıralayıp 100 ürün ile sınırlıyoruz
            var productList = products
                .OrderByDescending(p => p.Id)
                .Take(100) // Son 100 ürünü getiriyoruz
                .ToList();

            // Kategori ve Yazar verilerini View'a göndermek için model oluşturuyoruz
            var model = new ProductFilterViewModel
            {
                Products = productList,
                Categories = _context.Categories.ToList(),
                Authors = _context.Authors.ToList() 
            };

            return View(model);
        }
        public IActionResult GetAllProduct()
        {
            var products = _context.Products
                .Include(p => p.Category) // Kategori bilgiler
                .Include(p => p.Author) // Yazar bilgileri
                .AsQueryable().ToList() ;

            //var products = _context.Products.ToList();
            return View(products);
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
           


            // Product ile birlikte Category ve Author bilgilerini de çekiyoruz
            var product = await _context.Products.Include(p => p.Category).Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == id);


            if (product == null)
            {
                return NotFound(); // Eğer ürün bulunamazsa 404 sayfası döner
            }

            return View(product); // Ürünü ProductDetail.cshtml'e gönderir
        }

        


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Authors = new SelectList(_context.Authors, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Add(ProductAddViewModel newProduct)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", newProduct.CategoryId);
            ViewBag.Authors = new SelectList(_context.Authors, "Id", "Name", newProduct.AuthorId);

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage); 
                    }
                }
                return View(newProduct);  
            }

            // Eğer validasyon geçerse kayıt işlemi yapılacak
            try
            {
                if (newProduct.Image != null)
                {
                    var fileName = Path.GetFileName(newProduct.Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        newProduct.Image.CopyTo(stream);
                    }

                    newProduct.ImagePath = "/images/" + fileName;
                }

                Product ekleme = new Product()
                {
                    Name = newProduct.Name,
                    Price = newProduct.Price,
                    Stock = newProduct.Stock,
                    Description = newProduct.Description,
                    PublishDate = newProduct.PublishDate,
                    ImagePath = newProduct.ImagePath,
                    CategoryId = newProduct.CategoryId,
                    AuthorId = newProduct.AuthorId,
                };

                _context.Products.Add(ekleme);  // Ürün veritabanına ekleniyor
                _context.SaveChanges();

                TempData["status"] = "Ürün başarıyla eklendi";
                return RedirectToAction("Add");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // Hata mesajını kontrol et
                ModelState.AddModelError(String.Empty, "Ürün kaydedilirken bir hata meydana geldi, lütfen daha sonra tekrar deneyiniz");
                return View(newProduct);
            }
        }


        public IActionResult Remove(int id)
        {
            var product=_context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("GetAllProduct");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound(); // Eğer ürün bulunamazsa 404 hatası döner
            }
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Authors = new SelectList(_context.Authors, "Id", "Name", product.AuthorId);
            return View(product); // Eğer ürün bulunursa sayfaya model olarak gönderilir
        }



        [HttpPost]
        public IActionResult Update(Product updatedProduct, IFormFile image)
        {
            var product = _context.Products.Find(updatedProduct.Id);

            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
                product.Stock = updatedProduct.Stock;
                product.Description = updatedProduct.Description;
                product.PublishDate = updatedProduct.PublishDate;
                product.CategoryId = updatedProduct.CategoryId;
                product.AuthorId = updatedProduct.AuthorId;

                // Resim dosyası yüklendiyse
                if (image != null && image.Length > 0)
                {
                    // Dosya adını oluşturuyoruz
                    var fileName = Path.GetFileName(image.FileName);
                    // Dosyanın kaydedileceği klasör
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    // Dosyayı kaydediyoruz
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }

                    // Dosya yolu ürünün ImagePath alanına kaydediliyor
                    product.ImagePath = "/images/" + fileName;
                }

                _context.SaveChanges(); // Değişiklikleri veritabanına kaydediyoruz
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", updatedProduct.CategoryId);
            ViewBag.Authors = new SelectList(_context.Authors, "Id", "Name", updatedProduct.AuthorId);
            return RedirectToAction("GetAllProduct");
        }

    }
}
