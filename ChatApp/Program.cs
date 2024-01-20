using DataAccsesLayer;
using DataAccsesLayer.Hubs;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
const string CORS_POLICY = "AllowAll";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalSqlServer")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY,
                builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5500")
                    .AllowCredentials() 
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
});

builder.Services.AddSignalR();

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
app.UseCors(CORS_POLICY);

app.MapHub<ChatHub>("/chatHub");
app.Run();
