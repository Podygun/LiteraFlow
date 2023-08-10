

using LiteraFlow.Web.BL.Auth;
using LiteraFlow.Web.BL.DBSession;
using LiteraFlow.Web.DAL.Auth;
using LiteraFlow.Web.DAL.DBSession;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
//builder.Services.AddSession();

// Custom Services

builder.Services.AddSingleton<IAuthDAL, AuthDAL>();
builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddSingleton<IDBSessionDAL, DBSessionDAL>();
builder.Services.AddScoped<IDBSession, DBSession>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();


//..

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
//app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
