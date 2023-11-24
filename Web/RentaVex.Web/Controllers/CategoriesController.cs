namespace RentaVex.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using RentaVex.Data;
    using RentaVex.Data.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public CategoriesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            var category = this.db.Categories.Find(id);

            if (category == null)
            {
                return this.NotFound();
            }

            return category;
        }

        // The ones down from here are currentrly not working!

        [HttpPost]
        public async Task<ActionResult> Post(Category category)
        {
            await this.db.Categories.AddAsync(category);
            await this.db.SaveChangesAsync();

            return this.CreatedAtAction("Get", new { id = category.Id }, category);
        }

        [HttpPut] // We use put to edit data -->> patch is the same as put but we don't need all the info do change it.
        public async Task<ActionResult> Put(Category category)
        {
            this.db.Entry(category).State = EntityState.Modified;
            await this.db.SaveChangesAsync();
            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = this.db.Categories.Find(id);

            if (category == null)
            {
                return this.NotFound();
            }
            this.db.Remove(category);

            await this.db.SaveChangesAsync();
            return this.NoContent();
        }
    }
}
