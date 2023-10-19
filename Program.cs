
//-------------------------------------- Global Services------------------------------------// 
global using dotnet__rpg.Services.CharacterService;
global using dotnet__rpg.Services.SuperHeroService;
//-----------------------------------------------------------------------------------------// 
//-------------------------------------- Global DTOS --------------------------------------// 
global using dotnet__rpg.Dtos.Character;
global using dotnet__rpg.Dtos.SuperHero;
//-----------------------------------------------------------------------------------------// 

global using Microsoft.EntityFrameworkCore;
global using dotnet__rpg.Models;
global using AutoMapper;
using dotnet__rpg.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<ISuperHeroService, SuperHeroService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
