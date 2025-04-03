using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoPrograIII.Context;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Conexión a base de datos
builder.Services.AddDbContext<ProyectoProgra3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2️⃣ CORS (para permitir llamadas desde frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 3️⃣ Controladores + serialización JSON segura
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// 4️⃣ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5️⃣ Middleware
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🟢 Sirve archivos estáticos desde wwwroot
app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
