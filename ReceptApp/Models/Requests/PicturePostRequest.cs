namespace ReceptApp.Models.Requests
{
    public class PicturePostRequest
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
