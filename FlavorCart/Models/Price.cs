using FlavorCart.Interfaces;
using Google.Cloud.Firestore;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class Price : IBaseFirestoreData
    {
      
        public string Id { get ; set ; }
       
        [FirestoreProperty]
        public string PriceDate { get;  set; }
        [FirestoreProperty]
        public string ArticleId { get; set; }
        [FirestoreProperty]
        public float Cost { get; set; }
        [FirestoreProperty]
        public string Currency { get; set; }
        [FirestoreProperty]
        public string Shop { get; set; }
       
        
        public void setPriceDate()
        {
           this.PriceDate =  DateTime.Now.ToString();

        }
        public void setDefaultCurrency()
        {
            this.Currency = "€";
        }


    }

   
  

}
