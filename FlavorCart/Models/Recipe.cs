using FlavorCart.Interfaces;
using Google.Cloud.Firestore;
using System.Collections;
using IList = FlavorCart.Interfaces.IList;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class Recipe : IList , IBaseFirestoreData
    {
        
        public string Id { get; set; }
        [FirestoreProperty]
        public List<ListItem> ArticleList { get ; set ; }
        [FirestoreProperty]
        public string Name { get ; set ; }
        [FirestoreProperty]
        public string UserId { get ; set ; }
    
        [FirestoreProperty]
        public float TotalPrice { get; set ; }

        [FirestoreProperty]
        public string Description { get; set; } 
    }
}
