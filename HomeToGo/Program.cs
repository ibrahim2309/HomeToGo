using Microsoft.EntityFrameworkCore;
using HomeToGo.Models;
using Microsoft.AspNetCore.Identity;
using HomeToGo.Areas.Identity.Data;
using HomeToGo.DAL;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ListingDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ListingDbContextConnection' not found.");

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ListingDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:ListingDbContextConnection"]);
});

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ListingDbContext>();

builder.Services.AddRazorPages(); 
builder.Services.AddSession();

builder.Services.AddScoped<IListingRepository, ListingRepository>();

var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Information() // levels: Trace< Information < Warning < Erorr < Fatal
    .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

loggerConfiguration.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) &&
                                            e.Level == LogEventLevel.Information &&
                                            e.MessageTemplate.Text.Contains("Executed DbCommand"));

var logger = loggerConfiguration.CreateLogger();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    DBInit.Seed(app);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();

app.UseAuthentication();
app.UseSession();
app.UseAuthorization();


app.UseAuthentication();
app.MapRazorPages();

app.MapDefaultControllerRoute();

app.Run();