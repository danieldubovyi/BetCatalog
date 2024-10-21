using BetCatalog.Infrastructure.EFCore;
using BetCatalog.Models.Users;
using BetCatalog.Services;
using BetCatalog.WebApi.Identity;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BetCatalogDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("BetCatalogDocker")));
    //options => options.UseMySQL(builder.Configuration.GetConnectionString("BetCatalogMySql")));


//builder.Services.AddEmailSender(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>(o => o.SignIn.RequireConfirmedEmail = true)
    .AddRoles<UserRole>()
    .AddEntityFrameworkStores<BetCatalogDbContext>();

builder.Services.AddRepositories();
builder.Services.AddServices();
//builder.Services.AddGoogleSheets(builder.Configuration);

builder.Services.AddHttpLogging(
    options =>
    {
        options.LoggingFields = HttpLoggingFields.All;
        options.CombineLogs = true;
    });

builder.Services.AddControllers();

builder.Services.AddOpenApiDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(c =>
    c.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapUserEndpoints();

app.MapControllers();

app.Run();
