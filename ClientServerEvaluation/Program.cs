using ClientServerEvaluation;
using ClientServerEvaluation.Data;
using ClientServerEvaluation.Dtos;
using ClientServerEvaluation.Enums;
using ClientServerEvaluation.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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


app.MapGet("/api/iqueryble-server-evaluation",  async static (CatDbContext _context, ILogger<Program> logger) =>
{
    const int page = 1;
    const int pageSize = 2;
    IQueryable<Cat> list1 = _context.Cats.Where(x => x.Breed == CatBreeds.Balinese);
    logger.LogInformation($"IQueryable created!");
    logger.LogInformation($"Query the result using IQueryable...");
    var query1 = list1.OrderByDescending(cat => cat.Age)
        .Skip((page -1) * pageSize)
        .Take(pageSize);
    logger.LogInformation($"Execute the query using IQueryable");
    var result1 = await query1.ToListAsync();
    logger.LogInformation($"Resulte created using IQueryable");
    return "Look at logs, server evaluation example!";
});

app.MapGet("/api/ienumerable-client-evaluation",  async static (CatDbContext _context, ILogger<Program> logger) =>
{
    const int page = 1;
    const int pageSize = 2;
    IEnumerable<Cat> list1 = _context.Cats.Where(x => x.Breed == CatBreeds.Balinese).AsEnumerable<Cat>();
    logger.LogInformation($"IEnumerable created!");
    logger.LogInformation($"Query the result using IEnumerable...");
    var query1 = list1.OrderByDescending(cat => cat.Age)
        .Skip((page -1) * pageSize)
        .Take(pageSize);
    logger.LogInformation($"Execute the query using IEnumerable because of for or foreach");
    foreach(Cat cat in list1)
    {
        logger.LogInformation($"Nickname: {cat.Nickname}");
    }
    logger.LogInformation($"Execute again the query using IEnumerable");
    var result1 = query1.ToList();
    logger.LogInformation($"Result created using IEnumerable");
    return "Look at logs, server evaluation example!";
});

app.MapGet("/api/potentially-memory-leak/non-thread-safety/sample", async static (CatDbContext _context, ILogger<Program> logger) =>
{
    var list =  _context.Cats
        .AsEnumerable()
        .Where(cat => cat.Age >= new Util().RandomBasedOnAgeMemoryLeakAndNoThreadSatefy(cat.Age))
        .Select(cat => new CatDto
        (
            cat.Id,
            $"{cat.Nickname} - {new Util().RandomBasedOnAgeMemoryLeakAndNoThreadSatefy(cat.Age)}",
            cat.Age,
            cat.Breed.ToString()
        ))
        .ToList();
    

    return list;
});

app.MapGet("api/avoid-memory-leak/thread-safety/sample", async static (CatDbContext _context, ILogger<Program> logger) =>
{
    var list =  _context.Cats
        .AsEnumerable()
        .Where(cat => cat.Age >= Util.RandomBasedOnAge(cat.Age))
        .Select(cat => new CatDto
        (
            cat.Id,
            $"{cat.Nickname} - {Util.RandomBasedOnAge(cat.Age)}",
            cat.Age,
            cat.Breed.ToString()
        ))
        .ToList();

    return list;
});

app.Run();