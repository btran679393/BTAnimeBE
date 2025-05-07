using Microsoft.AspNetCore.Mvc;
using BTAnime.Models;
using BTAnime.DbContext; // Replace with your actual DbContext namespace
using System.Linq;

namespace BTAnime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public EpisodesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/episodes
        [HttpGet]
        public IActionResult GetAllEpisodes()
        {
            var episodes = _context.Episodes.ToList();
            return Ok(episodes);
        }

        // GET: api/episodes/5
        [HttpGet("{id}")]
        public IActionResult GetEpisodeById(int id)
        {
            var episode = _context.Episodes.Find(id);
            if (episode == null) return NotFound();
            return Ok(episode);
        }

        // POST: api/episodes
        [HttpPost]
        public IActionResult AddEpisode([FromBody] EpisodesModel episode)
        {
            _context.Episodes.Add(episode);
            _context.SaveChanges();
            return Ok(episode);
        }

        // DELETE: api/episodes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEpisode(int id)
        {
            var episode = _context.Episodes.Find(id);
            if (episode == null) return NotFound();
            _context.Episodes.Remove(episode);
            _context.SaveChanges();
            return Ok();
        }
    }
}
