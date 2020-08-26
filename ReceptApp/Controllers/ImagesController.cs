using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReceptApp.Models;

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
    }
}
