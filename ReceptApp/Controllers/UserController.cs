using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptApp.Models;
using ReceptApp.Models.Requests;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReceptApp.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly RecipeDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(RecipeDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserToSend>> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            return new UserToSend(user);
        }
        // PUT api/users/id
        [HttpPut("{id}")]
        public async  Task<IActionResult> Put(string id, [FromBody] UserToModify toModify)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null || user.Id != User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value)
                return BadRequest();

            if (toModify.FirstName != string.Empty)
                user.FirstName = toModify.FirstName;

            if (toModify.LastName != string.Empty)
                user.LastName = toModify.LastName;

            if (toModify.NickName != string.Empty)
                user.NickName = toModify.NickName;

            if (toModify.Email != string.Empty)
                user.Email = toModify.Email;

            try
            {
                await _context.SaveChangesAsync();
            } catch(DbUpdateException)
            {
                return StatusCode(500);
            }

            if(toModify.NewPassword != string.Empty)
            {
                try
                {
                    await _userManager.ChangePasswordAsync(user, toModify.OldPassword, toModify.NewPassword);
                }
                catch (DbUpdateException)
                {
                    return StatusCode(500);
                }
            }

            return Ok();
        }

        // DELETE api/users/sanyi
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null || user.Id != User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value)
                return BadRequest();

            await _signInManager.SignOutAsync();

            try
            {
                await _userManager.DeleteAsync(user);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
