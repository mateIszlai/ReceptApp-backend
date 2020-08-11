using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ReceptApp.Models;
using System.Threading.Tasks;

namespace ReceptApp.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestData loginData)
        {
            User user = null;
            try
            {
                user = await _userManager.FindByNameAsync(loginData.UserName);
            } catch (SqlException) {
                return StatusCode(500, "Something went wrong");
            }

            if(user == null)
            {
                return BadRequest("Wrong username or password");
            }

            if (await _userManager.CheckPasswordAsync(user, loginData.Password))
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return Ok(new[] { user.Id, user.UserName, user.Email });
            }

            return BadRequest("Wrong username or password");
        }
    }
}
