using FlavorCart.Interfaces;
using Google.Cloud.Firestore;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class Lists : iList 
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [FirestoreProperty]
        public string [] ArticleList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [FirestoreProperty]
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
  
        [FirestoreProperty]
        public float TotalPrize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [FirestoreProperty]
        public string UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [FirestoreProperty]
        public DateOnly CreationDate { get;set; }
        [FirestoreProperty]
        public bool IsPublic { get; set; }
    }
}
