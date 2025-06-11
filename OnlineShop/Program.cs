using Microsoft.Extensions.DependencyInjection;
//using DAL;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//builder.Services.ConfigureDAL(
//    builder.Configuration.GetConnectionString("DefaultConnection")
//);

app.MapGet("/", () => "Hello World!");

app.Run();
