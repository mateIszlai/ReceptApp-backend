using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptApp.Models.Requests
{
    public class RecipeToSend
    {
        public int Id { get; private set; }
        public string OwnerId { get; private set; }
        public string Name { get; private set; }
        public HashSet<Ingredient> Ingredients { get; private set; }
        public List<string> Description { get; private set; }
        public double PreparationTimeAmount { get; private set; }
        public string PreparationTimeUnit { get; private set; }
        public double CookTimeAmount { get; private set; }
        public string CookTimeUnit { get; private set; }
        public double AdditionalTimeAmount { get; private set; }
        public string AdditionalTimeUnit { get; private set; }
        public int Servings { get; private set; }
        public HashSet<Picture> Pictures { get; private set; }
        public Picture MainPicture { get; private set; }

        public RecipeToSend(Recipe recipe)
        {
            Id = recipe.Id;
            OwnerId = recipe.OwnerId;
            Name = recipe.Name;
            Ingredients = recipe.Ingredients;
            var description = recipe.Description.Split('@').ToList();
            description.ForEach(d => d.Trim('@'));
            Description = description;
            PreparationTimeAmount = recipe.PreparationTimeAmount;
            PreparationTimeUnit = recipe.PreparationTimeUnit;
            CookTimeAmount = recipe.CookTimeAmount;
            CookTimeUnit = recipe.CookTimeUnit;
            AdditionalTimeAmount = recipe.AdditionalTimeAmount;
            AdditionalTimeUnit = recipe.AdditionalTimeUnit;
            Servings = recipe.Servings;
            Pictures = recipe.Pictures;
            MainPicture = recipe.MainPicture;
        }
    }
}
