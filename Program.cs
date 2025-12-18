using API_de_Ventas.DALs.ClienteRepositoryCarpeta;
using API_de_Ventas.Middlewares;
using API_de_Ventas.Models;
using API_de_Ventas.Service.ClienteServiceCarpeta;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// =======================
// SERILOG
// =======================
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// =======================
// DATABASE
// =======================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// =======================
// JWT AUTH
// =======================
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSection["Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// =======================
// SERVICES
// =======================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

// =======================
// APP
// =======================
var app = builder.Build();

// =======================
// MIDDLEWARE
// =======================
app.UseMiddleware<ErrorHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
