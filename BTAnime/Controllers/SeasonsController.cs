using Microsoft.AspNetCore.Mvc;
using BTAnime.DbContext; // Adjust based on your actual namespace
using BTAnime.Models;    // Adjust based on your actual namespace
using System.Linq;

namespace BTAnime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SeasonsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/seasons
        [HttpGet]
        public IActionResult GetAllSeasons()
        {
            var seasons = _context.Seasons.ToList();
            return Ok(seasons);
        }

        // POST: api/seasons
        [HttpPost]
        public IActionResult AddSeason([FromBody] SeasonsModel season)
        {
            _context.Seasons.Add(season);
            _context.SaveChanges();
            return Ok(season);
        }

        // DELETE: api/seasons/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSeason(int id)
        {
            var season = _context.Seasons.Find(id);
            if (season == null) return NotFound();

            _context.Seasons.Remove(season);
            _context.SaveChanges();
            return Ok();
        }
    }
}
