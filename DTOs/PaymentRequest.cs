namespace PaymentService.DTOs
{
    public class PaymentRequest
    {

        /// <summary>
        /// Identifier for the user involved in the transaction.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// THe Movie/Show against  which the payment is made
        /// </summary>
        public string? MovieId { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Type of payment method (e.g., credit card, debit card, IMPS, NEFT, RTGS, UPI )
        /// </summary>
        public string? MethodType { get; set; }

        /// <summary>
        /// Bank Name (e.g: XYZ Bank, ABC Location)
        /// </summary>
        public string?  BankDetails { get; set; }

        /// <summary>
        /// Payment method details (e.g., card number, bank account number, upi id)
        /// </summary>
        public string? PaymentDetails { get; set; }
        /// <summary>
        /// Date and time of the transaction.
        /// </summary>
        public DateTime Timestamp { get; set; }  = DateTime.UtcNow;
    }
}
