using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReceptApp.Models;
using ReceptApp.Models.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReceptApp.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly RecipeDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(RecipeDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserToModify toModify)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
