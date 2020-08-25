using System.ComponentModel.DataAnnotations.Schema;

namespace ReceptApp.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }

        [ForeignKey(nameof(Recipe))]
        public int RecipeId { get; set; }
    }
}