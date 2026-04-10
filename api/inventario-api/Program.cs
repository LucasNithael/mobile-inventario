using FluentValidation;
using inventario_api.Data;
using inventario_api.DTOs;
using inventario_api.Repositories;
using inventario_api.Services;
using inventario_api.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ======================
// 🔹 SERVICES
// ======================

builder.Services.AddControllers();

// Swagger clássico
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMovementService, MovementService>();

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMovementRepository, MovementRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Validators
builder.Services.AddScoped<IValidator<ProductInput>, ProductInputValidator>();
builder.Services.AddScoped<IValidator<CategoryInput>, CategoryInputValidator>();
builder.Services.AddScoped<IValidator<MovementInput>, MovementInputValidator>();

var app = builder.Build();

// ======================
// 🔹 IMPORTANTE (IIS)
// ======================

// Se estiver publicado em /inventario no IIS
app.UsePathBase("/inventario");

// ======================
// 🔹 PIPELINE
// ======================

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/inventario/swagger/v1/swagger.json", "Inventario API v1");
        c.RoutePrefix = "swagger"; // acessa via /inventario/swagger
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();