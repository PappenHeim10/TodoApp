using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TodoApp.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// 1. Connection String holen
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthorization();

// 1. HIER NEU: Controller-Dienste registrieren
// Ohne das weiﬂ die App nicht, dass es Controller-Klassen gibt.
builder.Services.AddControllers();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<IdentityUser>();

// 2. HIER NEU: Controller-Routen aktivieren
// Ohne das werden die Pfade (wie /api/todos) nicht gefunden.
app.MapControllers();

app.Run();