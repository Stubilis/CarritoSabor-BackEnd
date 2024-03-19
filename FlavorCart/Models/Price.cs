using FlavorCart.Interfaces;
using Google.Cloud.Firestore;

namespace FlavorCart.Models
{
    public class Price : IBaseFirestoreData
    {
        //id
        //fecha
        //articulo
        //precio
        //tienda
        //moneda
        string IBaseFirestoreData.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IBaseFirestoreData.Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); } 

        [FirestoreProperty]
        public DateOnly Date { get; set; }
        [FirestoreProperty]
        public Article Article { get; set; }
        [FirestoreProperty]
        public decimal PriceCost { get; set; }
       
    }
}
