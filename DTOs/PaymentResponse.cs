using System.Text.Json.Serialization;

namespace PaymentService.DTOs
{
    public class PaymentResponse
    {
        ///// <summary>
        ///// Identifier for the user involved in the transaction.
        ///// </summary>
        //public string? UserId { get; set; }

        /// <summary>
        /// primary key
        /// </summary>
        [JsonPropertyName("bookRequestId")]
        public string? BookRequestId { get; set; }

        ///// <summary>
        ///// THe Movie/Show against  which the payment is made
        ///// </summary>
        //public string? MovieId { get; set; }

        /// <summary>
        /// Is the transaction Success
        /// </summary>
        [JsonPropertyName("paymentStatus")]
        public string? PaymentStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("failureReason")]
        public string? FailureReason { get; set; }

        /// <summary>
        /// Transaction Identifier.
        /// </summary>
        [JsonPropertyName("transactionId")]
        public string? TransactionId { get; set; }

    }
}
