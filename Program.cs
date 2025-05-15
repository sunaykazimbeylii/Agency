using Agency.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});
var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute(
    "Admin",
    "{Area:exists}/{controller=home}/{action=index}/{Id?}"
    );
app.MapControllerRoute(
    "default",
    "{controller=home}/{action=index}/{Id?}"
    );


app.Run();
