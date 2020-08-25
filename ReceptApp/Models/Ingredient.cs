using System.ComponentModel.DataAnnotations.Schema;

namespace ReceptApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Recipe))]
        public int RecipeId { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
    }
}