using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptApp.Models;
using ReceptApp.Models.Requests;
using System.Threading.Tasks;

namespace ReceptApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly RecipeDbContext _context;

        public ImagesController(RecipeDbContext context)
        {
            _context = context;
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _context.Pictures.FindAsync(id);

            if(image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }

        [HttpPost]
        public async Task<IActionResult> PostImage(PicturePostRequest pictureToAdd)
        {
            _context.Pictures.Add(new Picture { Name = pictureToAdd.Name, Content = pictureToAdd.Content, RecipeId = pictureToAdd.RecipeId });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch( DbUpdateException e)
            {
                return StatusCode(500, e.Data);
            }

            return Ok();
        }
    }
}
