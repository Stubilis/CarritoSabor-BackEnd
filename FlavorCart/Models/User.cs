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
        /// Gets or sets the gender of the user.
        /// </summary>
        [FirestoreProperty]
        public string Gender { get; set; }

              

        /// <summary>
        /// Gets or sets a property that will not be saved.
        /// </summary> 
        public string NotSavedProperty { get; set; }
        /*
          public int Id { get; set; }
          [FirestoreProperty]
          public string Name { get; set; }
          [FirestoreProperty]
          public string LastName { get; set; }
          [FirestoreProperty]
          public string Nickname { get; set; }
          [FirestoreProperty]
          public string Email { get; set; }
          [FirestoreProperty]
          public string Password { get; set; }
        */

    }
}
