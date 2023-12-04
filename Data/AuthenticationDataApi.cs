using Azure.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using PhoneBook.Interfaces;
using PhoneBook.Models;
using System.Net.Http;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PhoneBook.Data
{
    
    public class AuthenticationDataApi :IAuthenticationData
    {
        public AuthenticationDataApi() { }

        private static readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://a22508-0df2.k.d-f.pw/") 
        };
        
        public async Task<string> Login(RequestLogin request)
        {
            try
            {
                string serelizeContact = JsonConvert.SerializeObject(request);

                var response = await httpClient.PostAsync(
                   requestUri: "authentication/login",
                   content: new StringContent(serelizeContact, Encoding.UTF8,
                   mediaType: "application/json"));

                HttpResponseMessage httpResponseMessage = response.EnsureSuccessStatusCode();

                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string token = await response.Content.ReadAsStringAsync();

                    return token;
                } 
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Error? error = await response.Content.ReadFromJsonAsync<Error>();

                    return error.ToString();
                }
                else return string.Empty;
                
            }
            catch (HttpRequestException httpEx)
            {
                var ex = httpEx.Message; //log?

                return string.Empty;
            }
        }

        public async Task Register(User user)
        {
            UserDto userDto = new UserDto();

            userDto.FirstName = user.FirstName;
            userDto.LastName = user.LastName;
            userDto.Password = user.Password;
            userDto.Email = user.Email;

            string serelizeUser = JsonConvert.SerializeObject(userDto);

            var response = await httpClient.PostAsync(
               requestUri: "authentication/register",
               content: new StringContent(serelizeUser, Encoding.UTF8,
               mediaType: "application/json"));

            try
            {
                response.EnsureSuccessStatusCode();

            }
            catch (HttpRequestException httpEx)
            {
               
            }

        }
    }
}
