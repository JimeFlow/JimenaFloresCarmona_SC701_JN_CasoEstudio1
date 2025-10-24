using CasoEstudio1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();            // keep if you also use Razor Pages
builder.Services.AddControllersWithViews();  // MVC controllers + views

builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<IEjerciciosRepository, EjerciciosRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

// Map MVC controllers so HomeController responds at "/"
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Optional: keep if you also have Razor Pages
app.MapRazorPages();

app.Run();
