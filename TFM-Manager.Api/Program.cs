using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TFM_Manager.Data.DbContext;
using TFM_Manager.Data.Entities.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
}

// Controllers
builder.Services.AddControllers();

// OpenAPI
builder.Services.AddOpenApi();

// DbContext
builder.Services.AddDbContext<TfmManagerDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Identity alapregisztráció
builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<TfmManagerDbContext>()
    .AddDefaultTokenProviders();

// Jogosultsági szolgáltatások
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
