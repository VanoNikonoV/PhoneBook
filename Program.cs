using PhoneBook.Data;
using PhoneBook.Interfaces;
using PhoneBook.Middleware;
using PhoneBook.Models;

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

app.UseAuthentication(); //Включает возможности проверки подлинности
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contacts}/{action=Index}/{id?}");

app.Run();
