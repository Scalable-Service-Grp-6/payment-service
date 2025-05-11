using PaymentService.DTOs;
using PaymentService.Interfaces;
using System.Net;

namespace PaymentService.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings? _appSettings;

        public AuthorizationMiddleware(RequestDelegate next, AppSettings? appSettings)
        {
            _appSettings = appSettings;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Example: Check for a token in the Authorization header
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Authorization header missing or invalid.");
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            // Validate token (stubbed for demo - replace with real logic)
            if (! (await ValidateToken(context.Request)).IsValidToken)
            {
                context.Response.StatusCode = 403; // Forbidden
                await context.Response.WriteAsync("Invalid or expired token.");
                return;
            }

            // If everything is okay, continue to the next middleware
            await _next(context);
        }

        public async Task<AuthResponse> ValidateToken(HttpRequest request)
        {
            var authReponse = new AuthResponse();
            var authHeader = request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var response = await VerifyTokenAsync(token);

                if (bool.TryParse(response, out bool isValid))
                {
                    authReponse.IsValidToken = isValid;
                }
            }
            return authReponse;
        }

        private static readonly HttpClient _client = new HttpClient();

        public async Task<string> VerifyTokenAsync(string token)
        {
            try
            {
                var authurl = _appSettings?.AUTH_URL;
                _client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                authurl = $"http://{authurl}/users/verify/user?role=admin";
                await Console.Out.WriteLineAsync($"authUrl: {authurl}");
                await Console.Out.WriteLineAsync($"token : {token}");
                var response = await _client.GetAsync(authurl);
                response.EnsureSuccessStatusCode(); // throws if not 2xx

                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (HttpRequestException ex)
            {
                switch (ex.Message?.Contains(nameof(HttpStatusCode.Unauthorized)))
                {
                    case true:
                        return false.ToString();
                    default:
                        {
                            await Console.Out.WriteLineAsync($"{nameof(HttpRequestException)}: {Convert.ToString(ex)}");

                            throw;
                        }
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"{nameof(Exception)}: {Convert.ToString(ex)}");
                throw;
            }

        }
    }
}
