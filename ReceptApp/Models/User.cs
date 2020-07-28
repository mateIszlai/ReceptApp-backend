using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ReceptApp.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public List<int> FavouriteRecipesIds { get; set; }
    }
}
