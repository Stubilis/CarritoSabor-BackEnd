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
        string IBaseFirestoreData.Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private void setPriceDate()
        {
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            this.PriceDate = now.ToLongDateString();
          
        }

        //Constructor  
            public Price()
            {
           
            setPriceDate();
            }
        
    }
}
