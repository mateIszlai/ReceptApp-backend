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


    }
}
