using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReceptApp.Models;
using ReceptApp.Models.Requests;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReceptApp.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public RegisterController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestData userData)
        {
            var user = new User { UserName = userData.UserName, FirstName = userData.FirstName, LastName = userData.LastName, Email = userData.Email, NickName = userData.NickName };

            var result = await _userManager.CreateAsync(user, userData.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }

            return StatusCode(406, string.Join("\n", result.Errors.Select(error => error.Description)));
        }
    }
}
