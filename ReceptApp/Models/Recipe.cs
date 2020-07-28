using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReceptApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        
        [ForeignKey(nameof(User))]
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public HashSet<Ingredient> Ingredients { get; private set; }
        public string Description { get; set; }
        public Time PreparationTime { get; set; }
        public Time CookTime { get; set; }
        public Time AdditionalTime { get; set; }
        public int Servings { get; set; }
        public HashSet<Picture> Pictures { get; set; }
    }
}
