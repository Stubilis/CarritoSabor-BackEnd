using FlavorCart.Interfaces;
using Google.Cloud.Firestore;
using System.Globalization;

namespace FlavorCart.Models
{

    [FirestoreData]
    public class Category : IBaseFirestoreData
    {
        private string _name;
        /// <inheritdoc />
        /// [FirestoreProperty]
        public string Id { get; set; }
        ///<inheritdoc/>
        [FirestoreProperty]
        public string Name
        {
            get => _name; set
            {
                TextInfo textInfo = new CultureInfo("es-ES", false).TextInfo;
                _name = textInfo.ToTitleCase(value);
            }
        }
        /// <summary>
        /// Gets or sets the icon for the Category.
        /// </summary>
        [FirestoreProperty]
        public string IconURL { get; set; } = string.Empty; //URL to the icon
    }
}
