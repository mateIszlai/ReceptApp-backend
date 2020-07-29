using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptApp.Models;
using ReceptApp.Models.Requests;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReceptApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeDbContext _context;
        private readonly string[] permittedExtensions = { ".jpg", ".png", ".bmp" };


        public RecipesController(RecipeDbContext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, RecipeToChange newRecipe)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == default(Recipe))
                return NotFound();

            if (recipe.OwnerId != User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value)
                return BadRequest();

            recipe.Name = newRecipe.Name;
            recipe.Servings = newRecipe.Servings;
            recipe.AdditionalTimeUnit = newRecipe.AdditionalTimeUnit;
            recipe.AdditionalTimeAmount = newRecipe.AdditionalTimeAmount;
            recipe.CookTimeAmount = newRecipe.CookTimeAmount;
            recipe.CookTimeUnit = newRecipe.CookTimeUnit;
            recipe.Description = newRecipe.Description;
            recipe.PreparationTimeAmount = newRecipe.PreparationTimeAmount;
            recipe.PreparationTimeUnit = newRecipe.PreparationTimeUnit;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            return NoContent();
        }

        // POST: api/Recipes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            var formFile = Request.Form.Files[0];
            var filename = Request.Form.FirstOrDefault(k => k.Key == "FileName").Value;
            var ext = Path.GetExtension(filename).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                return BadRequest("Invalid file extension");
            }

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recipe>> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return NotFound();

            if (recipe.OwnerId != User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value)
                return BadRequest();

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }
    }
}
