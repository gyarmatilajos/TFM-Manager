using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TFM_Manager.Data.DbContext;
using TFM_Manager.Data.Entities.Identity;
using TFM_Manager.Business;
using TFM_Manager.Api.ExcpetionHandling;

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
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null);
    });
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
    .AddEntityFrameworkStores<TfmManagerDbContext>();

//Szolgáltatások regisztrációja
builder.Services.AddBusinessServices();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Jogosultsági szolgáltatások
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider
        .GetRequiredService<TfmManagerDbContext>();

    var canConnect = await dbContext.Database.CanConnectAsync();

    app.Logger.LogInformation(
        "Azure SQL adatbázis-kapcsolat: {Status}",
        canConnect ? "SIKERES" : "SIKERTELEN");
}

app.Run();
