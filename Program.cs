using PhoneBook.Data;
using PhoneBook.Interfaces;

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

builder.Services.AddControllersWithViews();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //�������� ����������� �������� �����������
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contacts}/{action=Index}/{id?}");

app.Run();
