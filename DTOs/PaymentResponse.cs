namespace PaymentService.DTOs
{
    public class PaymentResponse
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
        /// Is the transaction Success
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? FailureReason { get; set; }

        /// <summary>
        /// Transaction Identifier.
        /// </summary>
        public string? TransactionId { get; set; }

    }
}
