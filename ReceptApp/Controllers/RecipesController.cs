using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptApp.Models;
using ReceptApp.Models.Requests;
using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<RecipeToSend>>> GetRecipes()
        {
            var recipesToSend = new List<RecipeToSend>();
            var recipes = await _context.Recipes.ToListAsync();
            recipes.ForEach(r => recipesToSend.Add(new RecipeToSend(r)));
            return recipesToSend;
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeToSend>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var recipeToSend = new RecipeToSend(recipe);

            return recipeToSend;
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
            recipe.Description = string.Join('@', newRecipe.Description);
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
        public async Task<IActionResult> PostRecipe([FromBody]RecipePostRequest recipe)
        {
            var recipeToAdd = new Recipe(recipe, User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            _context.Recipes.Add(recipeToAdd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, e.Data);
            }

            return Ok(recipeToAdd.Id);
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
