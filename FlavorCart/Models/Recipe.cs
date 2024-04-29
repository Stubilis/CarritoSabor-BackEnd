using FlavorCart.Interfaces;
using Google.Cloud.Firestore;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class Recipe : iList
    {
        
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [FirestoreProperty]
        public List<ListItem> ArticleList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [FirestoreProperty]
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [FirestoreProperty]
        public string UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    
        [FirestoreProperty]
        public float TotalPrize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
