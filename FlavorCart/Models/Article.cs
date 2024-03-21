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
        
        public string [] Prices { get; set; }
        
        [FirestoreProperty]
        public string[] Categories { get; set; } //Save here the categories id

        [FirestoreProperty]
        public float AveragePrize { get; private set; } = 0;
        
        [FirestoreProperty]
        public int Count { get; set; } //cantidad de articulos
      
        [FirestoreProperty]
        public string Unit { get; set; } //unidad de medida como gramos, kilos, litros, etc
        

        //Set average prize with the prize array
        /*
        public void SetAveragePrize()
        {
            float sum = 0;
            foreach (Price Price in Prices) 
            {
                sum += Price.Cost;
            }
            this.AveragePrize =float.Round(sum / Prices.Length,2);
        }

        */
    }
}
