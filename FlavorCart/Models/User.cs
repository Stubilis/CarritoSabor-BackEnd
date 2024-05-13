using FlavorCart.Interfaces;
using Google.Cloud.Firestore;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class User : IBaseFirestoreData
    {
        /// <inheritdoc />
        public string Id { get; set; }
        //Check token for user
        
        /// <inheritdoc />
        [FirestoreProperty]
        public string Name { get; set; } = string.Empty;
        
        [FirestoreProperty]
        public string Nickname { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Email { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Languaje { get; set; } = "ES";

        public string Password { get; set; } = string.Empty;


    }
}
