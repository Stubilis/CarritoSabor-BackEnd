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
        public string ImageUrl { get; set; }

        [FirestoreProperty]
        public string Brand { get; set; } = string.Empty;
        
        public Price [] Prices { get; set; }
        
        [FirestoreProperty]
        public Category[] Categories { get; set; }

        [FirestoreProperty]
        public decimal AveragePrize { get; private set; } = 0;
        
        [FirestoreProperty]
        public int Count { get; set; } //cantidad de articulos
      
        [FirestoreProperty]
        public string Unit { get; set; } //unidad de medida como gramos, kilos, litros, etc
        

        //Set average prize with the prize array
        public void SetAveragePrize()
        {
            decimal sum = 0;
            foreach (Price Price in Prices) 
            {
                sum += Price.PriceCost;
            }
            this.AveragePrize =Decimal.Round(sum / Prices.Length,2);
        }


    }
}
