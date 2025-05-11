using PaymentService.DTOs;

namespace PaymentService.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<PaymentResponse>> GetPaymentsAsync();
    }
}
