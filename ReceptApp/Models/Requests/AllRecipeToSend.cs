namespace ReceptApp.Models.Requests
{
    public class AllRecipeToSend
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public Picture MainPicture { get;  set; }

        public string SmallDescription { get; set; }
    }
}
