using FlavorCart.Interfaces;
using Google.Cloud.Firestore;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class Lists : iList, IBaseFirestoreData
    {
        public string Id { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string[] ArticleList { get; set; }

        [FirestoreProperty]
        public float TotalPrize { get; set; }

        [FirestoreProperty]
        public string UserId { get; set; }

        [FirestoreProperty]
        public string CreationDate { get;private set; }

        [FirestoreProperty]
        public bool IsPublic { get; set; }
        private void setCreationDate()
        {
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            this.CreationDate = now.ToLongDateString();

        }
        //constructor
        public Lists()
        {
            setCreationDate();
        }
    }

}
