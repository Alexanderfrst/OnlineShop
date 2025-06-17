using Microsoft.OpenApi.Models;
using DAL;
using BLL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDAL(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.ConfigureBLL();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OnlineShop API",
        Version = "v1",
        Description = "REST API для интернет-магазина"
    });

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineShop API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
