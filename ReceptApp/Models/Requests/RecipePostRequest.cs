using System.Collections.Generic;

namespace ReceptApp.Models.Requests
{
    public class RecipePostRequest
    {
        public string Name { get; set; }
        public string SmallDescription { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Description { get; set; }
        public double PreparationTimeAmount { get; set; }
        public string PreparationTimeUnit { get; set; }
        public double CookTimeAmount { get; set; }
        public string CookTimeUnit { get; set; }
        public double AdditionalTimeAmount { get; set; }
        public string AdditionalTimeUnit { get; set; }
        public int Servings { get; set; }
    }
}
