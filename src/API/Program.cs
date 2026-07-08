using API.Middleware;
using Domain.Interfaces;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var bcilder = WebApplication.CreateBuilder(args);

bcilder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(bcilder.Configuration.GetConnectionString("DefaultConnection")));

bcilder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

bcilder.Services.AddControllers();

bcilder.Services.AddOpenApi();

var app = bcilder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
