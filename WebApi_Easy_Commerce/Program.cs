using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoPrograIII.Context;

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

// 3️⃣ Servicios básicos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4️⃣ Middleware
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 5️⃣ Routing
app.UseRouting();

// 🔐 No necesitas .UseAuthentication() ni .UseAuthorization()
// a menos que uses JWT u otro esquema más adelante

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
