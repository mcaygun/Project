using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CategoryServices.Data;
using CategoryServices.Models;
using Microsoft.EntityFrameworkCore;



namespace CategoryServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _context.Categories.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCategories), new { id = category.CategoryId }, category);
            }
            return BadRequest(ModelState);
        }
    }

}
