using MyFirstApi;
using MyFirstApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLifeTimeServices();

var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var sillyService = services.GetRequiredService<ISillyService>();
    var message = sillyService.GetData();
    sillyService.DoSillyThing();
    Console.WriteLine(message);
}

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
