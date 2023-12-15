using FluentValidation;
using PhoneBook.Data;
using PhoneBook.Interfaces;
using PhoneBook.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IContactData, ContactDataApi>();

builder.Services.AddTransient<IAuthenticationData, AuthenticationDataApi>();

builder.Services.AddSingleton<IRequestLogin, RequestLogin>(); 

builder.Services.AddScoped<IValidator<Contact>, ContactValidator>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contacts}/{action=Index}/{id?}");

app.Run();
