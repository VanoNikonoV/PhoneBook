using PhoneBook.Data;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Interfaces;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Areas.Identity.Data;

    /*
    ��� ����� �������
    ��������� Web-���������� �� �������� ��������� ������� � ���������� � ���� ������ �����������. 
    ��� ���� ���������� ������������ ����� �������:

    ��������� ������������ � ����� ����� ������������� ������ ���������� �����;
    �������������� ������������ � ����� ����������� ��������� ����� ������ � ���������� �����, 
    �� �� ����� ����������� �������������;
    ������������� � ����� ������ �����: ���������, �������������, ������� ������; 
    ��������� ����� ������������� �� ����, ������� ������������ �������������.

    ��� �����������
    ���������� �����������.
    ����������� ��� ����������� ������������� ��� ������ ����, ��������� ����.
    ������������ ���� ������.
    */

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") 
    ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<IContactData, ContactDataApi>();

builder.Services.AddControllersWithViews();

#region ����������� � �����������

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequireUppercase = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // ������������ Cookie � ����� ������������� �� ��� �������� �����������
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.SlidingExpiration = true;
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PhoneBook.Core.Constants.Policies.RequireAdmin, policy => policy.RequireRole(PhoneBook.Core.Constants.Roles.Administrator));
    options.AddPolicy(PhoneBook.Core.Constants.Policies.RequireManager, policy => policy.RequireRole(PhoneBook.Core.Constants.Roles.Manager));
});

#endregion

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication(); //�������� ����������� �������� �����������
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contacts}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
