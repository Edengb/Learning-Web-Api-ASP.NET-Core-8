using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProcedureAndQueriesSample.Data;
using ProcedureAndQueriesSample.Enum;
using ProcedureAndQueriesSample.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<CatDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/cats/procedure-sample-1", async (CatDbContext _context) => {
    
    var value = new SqlParameter("value", 2);

    var listCats = await _context.Cats
        .FromSql($"EXECUTE [dbo].[GetCatsByBreed] {value}")
        .ToListAsync();

    return listCats;
});

app.MapGet("api/breeds", async (CatDbContext _context) => {

    var listBreeds =  _context.Database
        .SqlQuery<int>($"EXECUTE [dbo].[GetAllBreeds]")
        .AsEnumerable()
        .Distinct()
        .Select(breed => ((CatBreeds) breed).ToString())
        .ToList(); 

    return new {
        Breeds = listBreeds
    };
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
