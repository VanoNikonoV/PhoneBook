using PhoneBook.Data;
using PhoneBook.Interfaces;
using PhoneBook.Middleware;
using PhoneBook.Models;

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

builder.Services.AddTransient<IContactData, ContactDataApi>();

builder.Services.AddTransient<IAuthenticationData, AuthenticationDataApi>();

builder.Services.AddSingleton<IRequestLogin, RequestLogin>();

//builder.Services.AddTransient<IAddTokenHeaders, AddTokenHeadersMiddleware>();

builder.Services.AddSession(opions =>
{
    opions.IdleTimeout = TimeSpan.FromMinutes(60);
});

builder.Services.AddControllersWithViews();

//________________________________________________________________________________________

var app = builder.Build();

app.UseHttpsRedirection();

app.UseSession();

app.Use(async (context, next) =>
{
    var JWTtoken = context.Session.GetString("JWTtoken");

    if (!string.IsNullOrEmpty(JWTtoken))
    {
        context.Response.Headers.Add("Authorization", "bearer " + JWTtoken);
    }
    await next(context);
});
//app.UseAddTokenHeadersMiddleware();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //�������� ����������� �������� �����������
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contacts}/{action=Index}/{id?}");

app.Run();
