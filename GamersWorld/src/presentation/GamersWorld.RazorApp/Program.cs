using GamersWorld.Application;
using GamersWorld.Data;
using GamersWorld.Data.Contexts;
using GamersWorld.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var sharedDbPath = Path.GetFullPath(Path.Combine(
    builder.Environment.ContentRootPath,
    "..",
    "GamersWorld.WebApi",
    "GamersWorldDb.sqlite3"));
builder.Configuration["ConnectionStrings:DevConStr"] = $"Data Source={sharedDbPath}";

builder.Services.AddRazorPages();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddData(builder.Configuration);
builder.Services.AddShared(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GamersWorldDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
