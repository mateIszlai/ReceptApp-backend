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
        public async Task<ActionResult<IEnumerable<AllRecipeToSend>>> GetRecipes()
        {
            var recipesToSend = new List<AllRecipeToSend>();
            var recipes = await _context.Recipes.Take(50).ToListAsync();
            recipes.ForEach(r => recipesToSend.Add(new AllRecipeToSend(r)));
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

            var recipeToSend = new RecipeToSend();
            recipeToSend.Id = recipe.Id;
            recipeToSend.OwnerId = recipe.OwnerId;
            recipeToSend.Name = recipe.Name;
            var ingredients = new List<IngredientToSend>();
            _context.Ingredients.Where(i => i.RecipeId == recipe.Id).ToList().ForEach(ingr => ingredients.Add(new IngredientToSend {  Name = ingr.Name, Amount = ingr.Amount, Unit = ingr.Unit}));
            recipeToSend.Ingredients = ingredients;
            var description = recipe.Description.Split('@').ToList();
            description.ForEach(d => d.Trim('@'));
            recipeToSend.Description = description;
            recipeToSend.PreparationTimeAmount = recipe.PreparationTimeAmount;
            recipeToSend.PreparationTimeUnit = recipe.PreparationTimeUnit;
            recipeToSend.CookTimeAmount = recipe.CookTimeAmount;
            recipeToSend.CookTimeUnit = recipe.CookTimeUnit;
            recipeToSend.AdditionalTimeAmount = recipe.AdditionalTimeAmount;
            recipeToSend.AdditionalTimeUnit = recipe.AdditionalTimeUnit;
            recipeToSend.Servings = recipe.Servings;
            recipeToSend.Pictures = recipe.Pictures;
            recipeToSend.MainPicture = recipe.MainPicture;

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
            var toRemove = new HashSet<Ingredient>();
            recipe.Ingredients.ForEach(i =>
            {
                var ingredient = newRecipe.Ingredients.FirstOrDefault(ing => ing.Name == i.Name);
                if(ingredient == null)
                {
                    toRemove.Add(i);
                }
                else
                {
                    i.Amount = ingredient.Amount;
                    i.Unit = ingredient.Unit;
                }
            });

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
