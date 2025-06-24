using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NVPCalculatorApi.Services.Implementations;
using NVPCalculatorApi.Services.Interfaces;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// CORS: Allow any origin for development use only
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            //.WithOrigins(
            //    "http://localhost:3000" // Frontend localhost          
            //)
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<INpvCalculatorService, NpvCalculatorService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Global exception handler - production
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            var errorResponse = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = "An unexpected error occurred.",
                Detail = exceptionHandlerPathFeature?.Error.Message,
                Instance = context.Request.Path
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
