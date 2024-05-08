using FlavorCart.Interfaces;
using Google.Cloud.Firestore;
using System.Data;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class UserToken : IBaseFirestoreData
    {
        
        public string Id { get; set; }

        [FirestoreProperty]
        public string Email { get; set; } // user email
        
        [FirestoreProperty]
        public string Token { get; set; } // user's token
        
        [FirestoreProperty]
        public long ExpirationTimeSeconds { get; set; } // expiration time in seconds

    
    }
}
