using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieApi;
using MovieApi.Entities;
using MovieApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MovieDbContext>();
builder.Services.AddScoped<MovieSeeder>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IMovieServices, MovieServices>();
builder.Services.AddScoped<IUserServices, UserServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<MovieDbContext>();
    var seeder = new MovieSeeder(dbContext);
    seeder.Seed();
}
app.Run();
