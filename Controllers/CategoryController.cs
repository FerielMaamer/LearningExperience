using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public CategoryController(TaskDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Category.ToListAsync();
            return Ok(categories);

        }


        // GET: api/categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST: api/categories

        [HttpPost]
        public async Task<ActionResult<Category>> CreateTask(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            Console.WriteLine(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.CatId }, category);
        }

        

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //private bool CategoryExists(int id)
        //{
        //    return _context.Category.Any(e => e.CatId == id);
        //}
    }
}
