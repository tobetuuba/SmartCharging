using SmartCharging.Application.Interfaces;
using SmartCharging.Application.Services;
using SmartCharging.Domain.Exceptions;
using SmartCharging.Infrastructure.Repositories;

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

app.UseHttpsRedirection();
app.MapControllers();

app.Run();