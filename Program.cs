using Microsoft.EntityFrameworkCore;
using MvcCrudApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=products.db"));

// Add MVC services
builder.Services.AddControllersWithViews();

// Add HttpClient for external calls
builder.Services.AddHttpClient();

// Register LogicAppService for Dependency Injection
builder.Services.AddScoped<LogicAppService>();

// Session Services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Enable serving static files from wwwroot
app.UseStaticFiles();

app.UseRouting();

// Enable session middleware
app.UseSession();

app.UseAuthorization();

// Default route (points to Account/Login initially)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
