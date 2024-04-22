using FlavorCart.Interfaces;
using Google.Cloud.Firestore;

namespace FlavorCart.Models
{

    [FirestoreData]
    public class Category : IBaseFirestoreData
    {
        /// <inheritdoc />
        /// [FirestoreProperty]
        public string Id { get; set; }
        ///<inheritdoc/>
        [FirestoreProperty]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the icon for the Category.
        /// </summary>
        [FirestoreProperty]
        public string IconURL { get; set; } = string.Empty; //URL to the icon
    }
}
