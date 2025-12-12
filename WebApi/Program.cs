using BookStoreApi.Application.DTOs;
using BookStoreApi.Application.Interfaces;
using BookStoreApi.Application.Services;
using BookStoreApi.Application.Validators;
using BookStoreApi.Infrastructure.Persistence;
using BookStoreApi.Infrastructure.Repositories;
using BookStoreApi.WebApi.MiddleWare;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Voeg DB context toe
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source = books.db"));

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add services to the container.
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<BookService>();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(CreateBookDtoValidator).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IValidator<CreatedBookDto>, CreateBookDtoValidator>(); 
// Replace default .NET logging with Serilog
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
