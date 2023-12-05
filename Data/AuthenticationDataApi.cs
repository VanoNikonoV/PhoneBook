using Newtonsoft.Json;
using PhoneBook.Interfaces;
using PhoneBook.Models;
using System.Net;
using System.Text;

namespace PhoneBook.Data
{

    public class AuthenticationDataApi :IAuthenticationData
    {
        public AuthenticationDataApi() { }

        private static readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://a22508-0df2.k.d-f.pw/") 
        };
        
        public async Task<HttpStatusCode> Login(RequestLogin request)
        {
            string serelizeContact = JsonConvert.SerializeObject(request);

            var response = await httpClient.PostAsync(
                requestUri: "authentication/login",
                content: new StringContent(serelizeContact, Encoding.UTF8,
                mediaType: "application/json"));

            if (response.IsSuccessStatusCode) { return response.StatusCode; }
            else { return response.StatusCode; }
        }

        public async Task<HttpStatusCode> Register(User user)
        {
            UserDto userDto = new UserDto();

            userDto.FirstName = user.FirstName;
            userDto.LastName = user.LastName;

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            userDto.PasswordHash = passwordHash;

            userDto.Email = user.Email;

            string serelizeUser = JsonConvert.SerializeObject(userDto);

            var response = await httpClient.PostAsync(
                requestUri: "authentication/register",
                content: new StringContent(serelizeUser, Encoding.UTF8,
                mediaType: "application/json"));

            if (response.IsSuccessStatusCode) { return response.StatusCode; }
            else { return response.StatusCode; }

        }
    }
}
