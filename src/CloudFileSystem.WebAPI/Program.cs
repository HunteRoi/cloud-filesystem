using CloudFileSystem.Application;
using CloudFileSystem.WebAPI.Middlewares.ExceptionHandling;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddApiVersioning();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region adding custom services

builder.Services.AddApplication();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

#endregion adding custom services

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

#region using custom middlewares

app.UseMiddleware<ExceptionHandlingMiddleware>();

#endregion using custom middlewares

app.MapControllers();

app.Run();