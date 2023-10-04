var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Re-add necessary middleware
app.UseStaticFiles();

app.MapDefaultControllerRoute();

app.Run();