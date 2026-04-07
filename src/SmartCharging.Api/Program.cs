using SmartCharging.Application.Interfaces;
using SmartCharging.Application.Services;
using SmartCharging.Domain.Exceptions;
using SmartCharging.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositories
builder.Services.AddSingleton<IGroupRepository, InMemoryGroupRepository>();
builder.Services.AddSingleton<IChargeStationRepository, InMemoryChargeStationRepository>();

// Services
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IChargeStationService, ChargeStationService>();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        IExceptionHandlerFeature? error = context.Features.Get<IExceptionHandlerFeature>();

        if (error?.Error is DomainException domainEx)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new { error = domainEx.Message });
        }
        else
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred." });
        }
    });
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();