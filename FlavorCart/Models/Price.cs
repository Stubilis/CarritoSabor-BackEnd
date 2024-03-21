using FlavorCart.Interfaces;
using Google.Cloud.Firestore;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class Price 
    {
      
        public string Id { get ; set ; }
       
        [FirestoreProperty]
        public DateOnly Date { get; private set; }
        [FirestoreProperty]
        public string ArticleId { get; set; }
        [FirestoreProperty]
        public float Cost { get; set; }
        [FirestoreProperty]
        public string Currency { get; set; } = "€";
        [FirestoreProperty]
        public string Shop { get; set; }

        private void setDate()
        {
            this.Date = DateOnly.FromDateTime(DateTime.Now);
        } 

    //Constructor
    public Price(string articleId, float cost, string currency, string shop)
        {
        this.ArticleId = articleId;
        this.Cost = cost;
        this.Currency = currency;
        this.Shop = shop;
        setDate();
        }
    }
}
