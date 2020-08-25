using ReceptApp.Models.Requests;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReceptApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        
        [ForeignKey(nameof(User))]
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Description { get; set; }
        public string SmallDescription { get; set; }
        public double PreparationTimeAmount { get; set; }
        public string PreparationTimeUnit { get; set; }
        public double CookTimeAmount { get; set; }
        public string CookTimeUnit { get; set; }
        public double AdditionalTimeAmount { get; set; }
        public string AdditionalTimeUnit { get; set; }
        public int Servings { get; set; }

        [ForeignKey(nameof(Picture))]
        public int MainPictureId { get; set; }

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
            SmallDescription = recipe.SmallDescription;
            Description = string.Join('@', recipe.Description);
            Ingredients = recipe.Ingredients;
            Servings = recipe.Servings;
            PreparationTimeAmount = recipe.PreparationTimeAmount;
            PreparationTimeUnit = recipe.PreparationTimeUnit;
            OwnerId = ownerId;        }
    }
}
