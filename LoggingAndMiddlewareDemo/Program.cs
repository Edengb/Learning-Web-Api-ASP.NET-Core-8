using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();


var logger = new LoggerConfiguration()
                .WriteTo
                .File(new JsonFormatter(), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/log.json"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
                .CreateLogger();

builder.Logging.AddSerilog(logger);

builder.Logging.AddEventLog();
builder.Logging.AddConsole();
builder.Logging.AddDebug();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) => {
    Console.WriteLine("First inicial - Global Middleware");
    await next(context);    
    Console.WriteLine("Last end - Global Middleware");
});

app.Map("/api/testingMiddleware", app => {

    app.Use(async (context, next) => {
            Console.WriteLine(context.Request.Path.Value);
            Console.WriteLine("Second before - Middleware for /api/testingMiddleware");
            await next();
            Console.WriteLine("Second after - Second Middleware for /api/testingMiddleware");
    });

    app.UseWhen(context => context.Request.Path == "/especial-middleware", app =>
    {
        app.Run(async context =>
        {
            Console.WriteLine("Before All Middleware for /api/testingMiddleware/especial-middleware");
            await context.Response.WriteAsync($"Special Middleware {context.Request.QueryString.Value}");
            Console.WriteLine("First After Middleware for /api/testingMiddleware/especial-middlewar");
        });
    });

    
    app.Run(async (context) => {
        Console.WriteLine("Before All Middleware for /api/testingMiddleware");
        await context.Response.WriteAsync("Without special middleware condition");
        Console.WriteLine("First After Middleware for /api/testingMiddleware");
    });
});




app.Run();
