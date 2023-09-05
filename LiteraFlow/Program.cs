#region Usings
using LiteraFlow.Web.BL.Auth;
using LiteraFlow.Web.BL.Books;
using LiteraFlow.Web.BL.DBSession;
using LiteraFlow.Web.BL.Profiles;
using LiteraFlow.Web.BL.WebCookie;
using LiteraFlow.Web.DAL.Auth;
using LiteraFlow.Web.DAL.Books;
using LiteraFlow.Web.DAL.BooksRelaltions;
using LiteraFlow.Web.DAL.DBSession;
using LiteraFlow.Web.DAL.Profiles;
using LiteraFlow.Web.DAL.UserToken;
using LiteraFlow.Web.Services;



#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
//builder.Services.AddSession();

// Custom Services

builder.Services.AddSingleton<IAuthDAL, AuthDAL>();
builder.Services.AddSingleton<IDBSessionDAL, DBSessionDAL>();
builder.Services.AddSingleton<IUserTokenDAL, UserTokenDAL>();
builder.Services.AddSingleton<IBooksDAL, BooksDAL>();
builder.Services.AddSingleton<IChaptersDAL, ChaptersDAL>();
builder.Services.AddSingleton<IBooks, Books>();
builder.Services.AddSingleton<IUserTokenDAL, UserTokenDAL>();
builder.Services.AddSingleton<IProfileDAL, ProfileDAL>();
builder.Services.AddSingleton<IProfile, Profile>();
builder.Services.AddSingleton<IBooksRelationDAL, BooksRelationDAL>();
builder.Services.AddSingleton<ICacheService, CacheService>();


builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddScoped<IDBSession, DBSession>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IWebCookie, WebCookie>();



var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();    
    
}
app.UseWebAssemblyDebugging();



app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true
});
app.UseDefaultFiles();

//app.UseDirectoryBrowser();
app.UseRequestLocalization();

app.UseRouting();
//app.UseSession();
app.UseAuthorization();

app.UseBlazorFrameworkFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.MapBlazorHub();

app.Run();
