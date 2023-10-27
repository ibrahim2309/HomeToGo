using Microsoft.EntityFrameworkCore;
using HomeToGo.Models;
using Microsoft.AspNetCore.Identity;
using HomeToGo.Areas.Identity.Data;
<<<<<<< Updated upstream
using HomeToGo.DAL;
using Serilog;
using Serilog.Events;
=======
using Serilog; //dette burde stå i punkt, men det er en annen kode som står der4//
>>>>>>> Stashed changes

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

/*var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Information() // levels: Trace< Information < Warning < Erorr < Fatal
    .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log"); //Save to a different file according to time//

loggerConfiguration.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) &&
                           e.Level == LogEventLevel.Information &&
                           e.MessageTemplate.Text.Contains("Executed DbCommand"));
   
var logger = loggerConfiguration.CreateLogger();
builder.Logging.AddSerilog(logger);

*/
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ListingDbContext>();

builder.Services.AddRazorPages(); 
builder.Services.AddSession();

<<<<<<< Updated upstream
builder.Services.AddScoped<IListingRepository, ListingRepository>();

var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Information() // levels: Trace< Information < Warning < Erorr < Fatal
    .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

loggerConfiguration.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) &&
                                            e.Level == LogEventLevel.Information &&
                                            e.MessageTemplate.Text.Contains("Executed DbCommand"));

var logger = loggerConfiguration.CreateLogger();
builder.Logging.AddSerilog(logger);
=======
// Er kodene fra Line 29 til og med 27 riktige?// 
>>>>>>> Stashed changes

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