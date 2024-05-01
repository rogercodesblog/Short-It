using Microsoft.EntityFrameworkCore;
using Short_It.Data;
using Short_It.Services.LinkService;
using Short_It.Services.RedirectionService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add Database
builder.Services.AddDbContext<ShortItAppContext>(options=>options.UseSqlite(builder.Configuration.GetConnectionString("ShortItAppConnection")));

//Add Link Service Injection
builder.Services.AddScoped<ILinkService, LinkService>();

//Add Redirection Service Injection
builder.Services.AddScoped<IRedirectionService, RedirectionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
