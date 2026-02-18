using Notepad.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<AppDbContext>(options =>
 options.UseSqlServer(
     builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// --- SWAGGER SETTINGS START ---
// Humne "if Development" wali condition hata di hai taaki Azure par bhi Swagger dikhe
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Notepad API V1");
    options.RoutePrefix = string.Empty; // Isse direct URL (https://your-app.azurewebsites.net/) par Swagger khulega
});
// --- SWAGGER SETTINGS END ---

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();