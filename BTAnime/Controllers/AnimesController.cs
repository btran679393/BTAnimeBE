using Microsoft.AspNetCore.Mvc;
using BTAnime.Models;
using BTAnime.DbContext; // Replace with your actual namespace
using System.Linq;

namespace BTAnime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AnimesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/animes
        [HttpGet]
        public IActionResult GetAllAnimes()
        {
            var animes = _context.Animes.ToList();
            return Ok(animes);
        }

        // GET: api/animes/5
        [HttpGet("{id}")]
        public IActionResult GetAnimeById(int id)
        {
            var anime = _context.Animes.Find(id);
            if (anime == null) return NotFound();
            return Ok(anime);
        }

        // POST: api/animes
        [HttpPost]
        public IActionResult AddAnime([FromBody] AnimeModel anime)
        {
            _context.Animes.Add(anime);
            _context.SaveChanges();
            return Ok(anime);
        }

        // DELETE: api/animes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAnime(int id)
        {
            var anime = _context.Animes.Find(id);
            if (anime == null) return NotFound();
            _context.Animes.Remove(anime);
            _context.SaveChanges();
            return Ok();
        }
    }
}
