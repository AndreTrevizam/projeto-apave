using Microsoft.EntityFrameworkCore;
using projeto_apave.Data;

var builder = WebApplication.CreateBuilder(args);

// Serviços
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbApave>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSession();

builder.Services.AddAuthentication("Autentificacao")
    .AddCookie("Autentificacao", options =>
    {
        options.LoginPath = "/Usuario/Login";
        options.LogoutPath = "/Usuario/Logout";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Middlewares
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}"
);

app.Run();
