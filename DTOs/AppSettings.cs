using PaymentService.Models;

namespace PaymentService.DTOs
{
    public class AppSettings
    {
        /// <summary>
        /// Db Settings
        /// </summary>
        public string? MONGODB_URL { get; set; }

        /// <summary>
        /// Db Settings
        /// </summary>
        public string? MONGODB_DB_NAME { get; set; }

        /// <summary>
        /// Auth service url.
        /// </summary>
        public string? AUTH_URL { get; set; }
    }
}
