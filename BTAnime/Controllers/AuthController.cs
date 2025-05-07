using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BTAnime.DbContext;
using BTAnime.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(MyDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginModel register)
    {
        if (string.IsNullOrWhiteSpace(register.Username) || string.IsNullOrWhiteSpace(register.Password))
            return BadRequest("Username and password are required.");

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == register.Username);
        if (existingUser != null)
            return Conflict("Username already exists.");

        // Hash password using SHA256 (you can switch to BCrypt later for stronger hashing)
        string hashedPassword = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(register.Password)));

        // Assign default role if needed
        var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
        if (defaultRole == null)
            return StatusCode(500, "Default role not found.");

        var newUser = new User
        {
            Username = register.Username,
            PasswordHash = hashedPassword,
            Role = defaultRole
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == login.Username);

        if (user == null) return Unauthorized("Invalid username");

        // Verify password
        string hashedInputPassword = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(login.Password)));
        if (user.PasswordHash != hashedInputPassword)
            return Unauthorized("Invalid password");

        var key = Encoding.ASCII.GetBytes("YourSuperSecretKeyHe321312551321adsaghdhht(*)*)#!&#@)!(*!DADADSAre");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new
        {
            token = tokenHandler.WriteToken(token),
            user = new { user.UserId, user.Username, Role = user.Role.Name }
        });
    }
}
