using System.Collections.Generic;

namespace ReceptApp.Models.Requests
{
    public class RecipeToChange
    {
        public string Name { get; set; }
        public List<string> Description { get; set; }
        public double PreparationTimeAmount { get; set; }
        public string PreparationTimeUnit { get; set; }
        public double CookTimeAmount { get; set; }
        public string CookTimeUnit { get; set; }
        public double AdditionalTimeAmount { get; set; }
        public string AdditionalTimeUnit { get; set; }
        public int Servings { get; set; }
        public List<IngredientToSend> Ingredients { get; set; }
    }
}
