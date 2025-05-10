using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace PaymentService.Models
{
    public class PaymentTransaction
    {

        /// <summary>
        /// primary key
        /// </summary>
        [BsonElement("bookRequestId")]
        public string? BookRequestId { get; set; }

        /// <summary>
        /// primary key
        /// </summary>
        [BsonId()]
        public string? trasnsactionId { get; set; }

        ///// <summary>
        ///// Identifier for the user involved in the transaction.
        ///// </summary>
        //public string? UserId { get; set; }

        ///// <summary>
        ///// THe Movie/Show against  which the payment is made
        ///// </summary>
        //public string? MovieId { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        [BsonElement("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// PENDING, PAID, FAILED
        /// </summary>
        [BsonElement("paymentStatus")]
        public string? PaymentStatus { get; set; }

        /// <summary>
        /// Type of payment method (e.g., credit card, debit card, IMPS, NEFT, RTGS, UPI )
        /// </summary>
        [BsonElement("methodType")]
        public string? MethodType { get; set; }

        /// <summary>
        /// Bank Name (e.g: XYZ Bank, ABC Location)
        /// </summary>
        [BsonElement("bankDetails")]
        public string? BankDetails { get; set; }

        /// <summary>
        /// Payment method details (e.g., card number, bank account number, upi id)
        /// </summary>
        [BsonElement("paymentDetails")]
        public string? PaymentDetails { get; set; }
        /// <summary>
        /// Date and time of the transaction.
        /// </summary>
        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    }
}
