

using PaymentService.DTOs;
using PaymentService.Interfaces;
using System.Text.Json;
using System.Text;
using MongoDB.Driver;
using System.Net;

namespace PaymentService.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings? _appSettings;

        public AuthService(AppSettings appSettings)
        {
            _appSettings = appSettings;

        }
        public async Task<AuthResponse> ValidateToken(HttpRequest request)
        {
            var authReponse = new AuthResponse();
            var authHeader = request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var response  = await VerifyTokenAsync(token);

                if(bool.TryParse(response, out bool isValid)){
                    authReponse.IsValidToken = isValid;
                }
            }
            return authReponse;
        }

        private static readonly HttpClient _client = new HttpClient();

        public async Task<string> PostDataAsync(object data)
        {
            string json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"https://api.example.com/create", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> VerifyTokenAsync(string token)
        {
            try
            {
                var authurl = _appSettings?.AUTH_URL;
                _client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _client.GetAsync($"http://{authurl}/users/verify/user?role=admin");
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
                        throw;
                }
            }
        }
    }
}
