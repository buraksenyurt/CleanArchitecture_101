using GamersWorld.Application;
using GamersWorld.Data;
using GamersWorld.Shared;

var builder = WebApplication.CreateBuilder(args);

var configuration=builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication(configuration);
builder.Services.AddData(configuration);
builder.Services.AddShared(configuration);
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();