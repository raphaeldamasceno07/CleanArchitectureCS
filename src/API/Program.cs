using API.Middleware;
using Application.UseCases;
using Application.Validators.Customer;
using Domain.Interfaces;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IPasswordHasher, Infrastructure.Security.PasswordHasher>();

builder.Services.AddScoped<RegisterCustomerUseCase>();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterCustomerValidator>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "Clean Arch API";
        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Clean Arch API")
            .WithTheme(ScalarTheme.Kepler)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithOpenApiRoutePattern("/openapi/v1.json");
    });
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
