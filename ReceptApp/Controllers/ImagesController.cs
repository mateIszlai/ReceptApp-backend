using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptApp.Models;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReceptApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly RecipeDbContext _context;
        private readonly string[] permittedExtensions = { ".jpg", ".png", ".bmp" };

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
        public async Task<IActionResult> PostImage()
        {
            var formfile = Request.Form.Files[0];
            var filename = Request.Form.FirstOrDefault(k => k.Key == "fileName").Value;
            var ext = Path.GetExtension(filename).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                return BadRequest("Invalid file extension");
            }

            using( var memoryStream = new MemoryStream())
            {
                await formfile.CopyToAsync(memoryStream);
                var picture = new Picture
                {
                    Name = WebUtility.HtmlEncode(filename),
                    RecipeId = int.Parse(Request.Form.First(k => k.Key == "recipeId").Value),
                    Content = memoryStream.ToArray()
                };

                _context.Pictures.Add(picture);
            }

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
