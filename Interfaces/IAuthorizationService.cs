using PaymentService.DTOs;

namespace PaymentService.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> ValidateToken(HttpRequest request);
    }
}
