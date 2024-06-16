using FlavorCart.Interfaces;
using Google.Cloud.Firestore;


namespace FlavorCart.Models
{
    [FirestoreData]
    public class ListItem 
    {
     //This class is used to create the list   
     
         [FirestoreProperty]
        public string ArticleId { get; set; }
        
        [FirestoreProperty]
        public int Amount { get; set; } 

        [FirestoreProperty]
        public string Unit { get; set; }  // Unit of measure for the quantity of articles

        [FirestoreProperty]
        public bool IsActive { get; set; }
    }
}
