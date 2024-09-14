using EFCoreDbContextPoolingSample.Data;
using EFCoreDbContextPoolingSample.Mapping;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var useDbContextPooling = Convert.ToBoolean(configuration["Database:UseDbContextPooling"]);

Console.WriteLine("useDbContextPooling: " + useDbContextPooling);

if(useDbContextPooling)
{
    builder.Services.AddDbContextPool<CatDbContext>(options =>
        options.UseSqlServer(configuration["Database:ConnectionString"]));
}
else
{
    builder.Services.AddDbContext<CatDbContext>(options => 
        options.UseSqlServer(configuration["Database:ConnectionString"]));
}

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/api/cats", async (CatDbContext dbContext) =>
{
    return await dbContext.Cats
            .Select(cat => cat.ToCatDto())
            .AsNoTracking()
            .ToListAsync();
});

app.Run();