using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReceptApp.Models;
using System.Threading.Tasks;

namespace ReceptApp.Controllers
{
    [Route("api/logout")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        
        public LogoutController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
