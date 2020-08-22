namespace ReceptApp.Models.Requests
{
    public class AllRecipeToSend
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Picture MainPicture { get; private set; }

        public AllRecipeToSend(Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            MainPicture = recipe.MainPicture;
        }
    }
}
