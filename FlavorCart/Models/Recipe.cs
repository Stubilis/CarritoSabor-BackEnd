using FlavorCart.Interfaces;
using Google.Cloud.Firestore;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class Recipe : iList
    {
        [FirestoreProperty]
        public Article[] ArticleList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [FirestoreProperty]
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [FirestoreProperty]
        public User User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [FirestoreProperty]
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [FirestoreProperty]
        public float TotalPrize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
