using Microsoft.EntityFrameworkCore;
using P05Shop.API.Models;
using P05Shop.API.Services;
using P06Shop.Shared.Services.ProductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductService, ProductService>();

// addScoped - oznacza, że w trakcie jednego requestu będzie istniała tylko jedna instancja klasy ProductService
// addTransient - oznacza, że obiekt będzie tworzony za każdym razem, gdy odwolujemy się do niego
// addSingleton - oznacza, że obiekt będzie tworzony tylko raz i będzie istniał tak długo, jak długo istnieje aplikacja

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
