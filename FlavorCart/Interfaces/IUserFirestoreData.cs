using Google.Cloud.Firestore;

namespace FlavorCart.Interfaces
{
    /// <summary>
    /// Represents the base data that will exists on all records.
    /// </summary>
    public interface IUserFirestoreData
    {
        /// <summary>
        /// Gets and set the Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets and set the email
        /// </summary>
        public string Email { get; set; } // user
        /// <summary>
        /// Gets and set the token
        /// </summary>
        public string Token { get; set; } // user's token
    }
}
