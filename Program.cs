using PhoneBook.Data;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Interfaces;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Areas.Identity.Data;
using Microsoft.VisualBasic;
    /*
    Что нужно сделать
    Расширьте Web-приложение из прошлого домашнего задания и подключите к нему модуль авторизации. 
    При этом необходимо разграничить права доступа:

    анонимный пользователь — имеет право просматривать только телефонную книгу;
    авторизованный пользователь — имеет возможность добавлять новые записи в телефонную книгу, 
    но не имеет возможности редактировать;
    администратор — имеет полные права: добавлять, редактировать, удалять запись; 
    добавлять новых пользователей на сайт, удалять существующих пользователей.

    Что оценивается
    Подключена авторизация.
    Реализованы все возможности пользователей для каждой роли, описанной выше.
    Использована база данных.
    */
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") 
    ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
    options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>(); //

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
//    options.AddPolicy(Constants.Policies.RequireManager, policy => policy.RequireRole(Constants.Roles.Manager));
//});

builder.Services.AddTransient<IContactData, ContactDataApi>();

builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequireUppercase = false;
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication(); //Включает возможности проверки подлинности
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contacts}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
