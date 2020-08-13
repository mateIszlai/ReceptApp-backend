using ReceptApp.Models.Requests;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ReceptApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        
        [ForeignKey(nameof(User))]
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public HashSet<Ingredient> Ingredients { get; set; }
        public string Description { get; set; }
        public double PreparationTimeAmount { get; set; }
        public string PreparationTimeUnit { get; set; }
        public double CookTimeAmount { get; set; }
        public string CookTimeUnit { get; set; }
        public double AdditionalTimeAmount { get; set; }
        public string AdditionalTimeUnit { get; set; }
        public int Servings { get; set; }
        public HashSet<Picture> Pictures { get; set; }
        public Picture MainPicture { get; set; }

        public Recipe()
        {

        }
        public Recipe(RecipePostRequest recipe, string ownerId)
        {
            AdditionalTimeAmount = recipe.AdditionalTimeAmount;
            AdditionalTimeUnit = recipe.AdditionalTimeUnit;
            CookTimeAmount = recipe.CookTimeAmount;
            CookTimeUnit = recipe.CookTimeUnit;
            Name = recipe.Name;
            Description = string.Join('@', recipe.Description);
            Ingredients = recipe.Ingredients.ToHashSet();
            Servings = recipe.Servings;
            PreparationTimeAmount = recipe.PreparationTimeAmount;
            PreparationTimeUnit = recipe.PreparationTimeUnit;
            OwnerId = ownerId;
            Pictures = new HashSet<Picture>();
        }
    }
}
