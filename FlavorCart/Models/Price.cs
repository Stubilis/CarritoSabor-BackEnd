using FlavorCart.Interfaces;
using Google.Cloud.Firestore;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class Price : IBaseFirestoreData
    {
      
        public string Id { get ; set ; }
       
        [FirestoreProperty]
        public string PriceDate { get; private set; }
        [FirestoreProperty]
        public string ArticleId { get; set; }
        [FirestoreProperty]
        public float Cost { get; set; }
        [FirestoreProperty]
        public string Currency { get; set; } = "€";
        [FirestoreProperty]
        public string Shop { get; set; }
       

        public void setPriceDate()
        {
           this.PriceDate =  DateTime.Now.ToString();

        }


       
        public void updateMediumPrice (string artId)
        {


        }
    }

    //TODO
    //When a price is added, the average price of the article should be updated
    //When a price is deleted, the average price of the article should be updated
    //When a price is updated, the average price of the article should be updated
    //When a price is added, the medium price of the article should be updated
    //When a price is deleted, the medium price of the article should be updated
    //When a price is updated, the medium price of the article should be updated

}
