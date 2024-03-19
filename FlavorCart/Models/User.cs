using FlavorCart.Interfaces;
using Google.Cloud.Firestore;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class User : IBaseFirestoreData
    {
        /// <inheritdoc />
        public string Id { get; set; }

        /// <inheritdoc />
        [FirestoreProperty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname of the user.
        /// </summary>
        [FirestoreProperty]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the nickname of the user.
        /// </summary>
        [FirestoreProperty]
        public string Nickname { get; set; }
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        [FirestoreProperty]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets a password that will not be saved.
        /// </summary> 
        public string Password { get; set; }


    }
}
