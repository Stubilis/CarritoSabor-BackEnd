using FlavorCart.Interfaces;
using Google.Cloud.Firestore;
using System.Globalization;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class Lists : iList, IBaseFirestoreData
    {
        public string Id { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string[] ArticleList { get; set; } //Save here the articles id

        [FirestoreProperty]
        public float TotalPrize { get; set; } = 0;

        [FirestoreProperty]
        public string UserId { get; set; }

        [FirestoreProperty]
        public string CreationDate { get;private set; }

        [FirestoreProperty]
        public bool IsPublic { get; set; }
        private void setCreationDate()
        {
            DateTime localDate = DateTime.Now;
            //Get user language and set the date format

            this.CreationDate = localDate.ToString(new CultureInfo("en-US"));

        }
        //constructor
        public Lists()
        {
            setCreationDate();
        }
    }

}
