using PhoneBook.Interfaces;

namespace PhoneBook.Middleware
{
    public class AddTokenHeadersMiddleware: IAddTokenHeaders
    {
        private readonly RequestDelegate _next;

        public AddTokenHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public bool IsToken { get; set; } = false;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var JWTtoken = httpContext.Session.GetString("JWTtoken");

            if (!string.IsNullOrEmpty(JWTtoken))
            {
                httpContext.Response.Headers.Add("Authorization", "bearer " + JWTtoken);
                IsToken = true;
            }

            await _next(httpContext);
        }
    }

    public static class AddTokenHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseAddTokenHeadersMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AddTokenHeadersMiddleware>();
        }
    }
}
