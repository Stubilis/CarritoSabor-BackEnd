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
        public string Name { get; set; } 
        
        [FirestoreProperty]
        public string Nickname { get; set; } 

        [FirestoreProperty]
        public string Email { get; set; } 

        [FirestoreProperty]
        public string Languaje { get; set; }


    }
}
