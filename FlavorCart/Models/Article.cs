using FlavorCart.Interfaces;
using Google.Cloud.Firestore;


namespace FlavorCart.Models
{
    [FirestoreData]
    public class Article : IBaseFirestoreData
    {
       
        public string Id { get; set; }
     
         [FirestoreProperty]
        public string Name { get; set; }
        
        [FirestoreProperty]
        public string Description { get; set; } = string.Empty;
      
        [FirestoreProperty]
        public string ImageUrl { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Brand { get; set; } = string.Empty;

        [FirestoreProperty]
        public string[] Categories { get; set; } = { string.Empty };//Save here the categories id

        [FirestoreProperty]
        public float AveragePrice { get; set; } = 0;

        [FirestoreProperty]
        public int Quantity { get; set; } = 0; //Amount of articles 

        [FirestoreProperty]
        public string Unit { get; set; } = ""; // Unit of measure for the quantity of articles
        
    }
}
