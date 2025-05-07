using BTAnime.DbContext;
using BTAnime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly MyDbContext _context;

    public FavoritesController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost("add")]
    public IActionResult AddFavorite(int userId, int animeId)
    {
        var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserId == userId);
        var anime = _context.Animes.Find(animeId);

        if (user == null || anime == null)
            return NotFound("User or anime not found.");

        var role = _context.Roles.Find(user.RoleId);
        if (role == null || role.Name.ToLower() != "vip")
            return Unauthorized("Only VIP users can favorite shows.");

        if (_context.Favorites.Any(f => f.UserId == userId && f.AnimeID == animeId))
            return BadRequest("Anime already favorited.");

        var fav = new Favorite { UserId = userId, AnimeID = animeId };
        _context.Favorites.Add(fav);
        _context.SaveChanges();

        return Ok("Favorited successfully.");
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetFavorites(int userId)
    {
        var favorites = _context.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.Anime)
            .Select(f => new { f.Anime.AnimeID, f.Anime.Title })
            .ToList();

        return Ok(favorites);
    }
}
