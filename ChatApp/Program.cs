    using DataAccsesLayer;
    using DataAccsesLayer.Hubs;
    using Microsoft.EntityFrameworkCore;

    var builder = WebApplication.CreateBuilder(args);
    const string CORS_POLICY = "AllowAll";

    builder.Services.AddControllers();
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
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

builder.Services.AddSignalR();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();

    app.UseCors(CORS_POLICY);
    app.MapHub<ChatHub>("/chathub");

    app.MapControllers();
    app.Run();
