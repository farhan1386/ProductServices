using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductServices.Data;
using ProductServices.Models;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public CategoriesController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await db.Categories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await db.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> New(Category category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetCategories", new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;


            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await db.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return category;
        }
        private bool CategoryExists(int id)
        {
            return db.Categories.Any(c => c.Id == id);
        }
    }
}