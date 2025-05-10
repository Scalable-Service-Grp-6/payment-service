using PaymentService.DTOs;
using PaymentService.Interfaces;
using PaymentService.Models;
using PaymentService.Module;
using System.Reflection.Metadata;

namespace PaymentService.Services
{
    public enum PaymentStaus
    {
        PENDING, PAID, FAILED
    }
    /// <summary>
    /// 
    /// </summary>
    public class PaymentService : IPaymentService

    {
        private readonly MongoContext _context;

        public PaymentService(MongoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request) 
        {
            ValidatePaymentRequest(request);

            try
            {
                var document = new PaymentTransaction
                {
                    Amount = request.Amount,
                    BankDetails = request.BankDetails,
                    MethodType = request.MethodType,
                    //MovieId = request.MovieId,
                    BookRequestId = request.BookingRequestId,
                    PaymentDetails = request.PaymentDetails,
                    Timestamp = request.Timestamp,
                    //UserId = request.UserId,
                    PaymentStatus = nameof(PaymentStaus.PAID),
                    trasnsactionId = Guid.NewGuid().ToString()
                };
                await _context.Payments.InsertOneAsync(document);

                var response = new PaymentResponse
                {
                    TransactionId = document.trasnsactionId,
                    PaymentStatus = nameof(PaymentStaus.PAID),
                    //MovieId = request.MovieId,
                    //UserId = request.UserId,
                    BookRequestId = request.BookingRequestId
                };
                return response;

            }
            catch (Exception ex)
            {
                var response = new PaymentResponse
                {
                    FailureReason = Convert.ToString(ex),
                    PaymentStatus = nameof(PaymentStaus.FAILED),
                    BookRequestId = request.BookingRequestId
                };
                return response;
            }
            
        }

        private void ValidatePaymentRequest(PaymentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Payment request cannot be null.");

            if (string.IsNullOrWhiteSpace(request.BookingRequestId))
                throw new ArgumentException("Booking Request id is required.", nameof(request.BookingRequestId));

            //if (string.IsNullOrWhiteSpace(request.UserId))
            //    throw new ArgumentException("User ID is required.", nameof(request.UserId));

            //if (string.IsNullOrWhiteSpace(request.MovieId))
            //    throw new ArgumentException("Movie ID is required.", nameof(request.MovieId));

            if (request.Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(request.Amount));

            if (string.IsNullOrWhiteSpace(request.PaymentDetails))
                throw new ArgumentException("Payment details are required (e.g., card number, UPI ID, bank account number).", nameof(request.PaymentDetails));

            if (string.IsNullOrWhiteSpace(request.MethodType))
                throw new ArgumentException("Payment method type is required (e.g., credit card, debit card, IMPS, NEFT, RTGS, UPI).", nameof(request.MethodType));

            if (string.IsNullOrWhiteSpace(request.BankDetails))
                throw new ArgumentException("Bank details are required (e.g., bank name or branch location).", nameof(request.BankDetails));
        }

    }
}
