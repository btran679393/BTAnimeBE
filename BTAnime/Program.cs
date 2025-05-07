using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BTAnime.DbContext;

var builder = WebApplication.CreateBuilder(args);

// 🔐 Configure JWT authentication
var key = Encoding.ASCII.GetBytes("Mybirthdayisnovember12th20009andmynameisbrandonABCDEFG!*&#!@!(*#!^#@%)(*^&!@!"); // Replace with strong key & store in secrets/env

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// 📦 Add EF Core with SQL Server
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🌐 Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutterApp",
        policy => policy
            .AllowAnyOrigin()   // You can restrict this later
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// 📚 Add Swagger and controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🛠️ Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// 🌐 Enable CORS BEFORE auth
app.UseCors("AllowFlutterApp");

app.UseAuthentication(); // 🔐
app.UseAuthorization();

app.MapControllers();

app.Run();
