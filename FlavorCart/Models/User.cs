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
        public string Name { get; set; } = "Nombre de usuario";

        
        [FirestoreProperty]
        public string Surname { get; set; } = "Apellido de usuario";

        
        [FirestoreProperty]
        public string Nickname { get; set; } = "Apodo de usuario";
        
        [FirestoreProperty]
        public string Email { get; set; } = "email@ejemplo.com";
     
        public string Password { get; set; }


    }
}
