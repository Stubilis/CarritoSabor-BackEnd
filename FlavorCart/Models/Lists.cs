using FlavorCart.Interfaces;

namespace FlavorCart.Models
{
    public class Lists : iList
    {
        //id
        //articulos - cantidad
        //bool public/priv
        //Owner
        //Nombre
        //Fecha creacion
        public Article[] ArticleList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float TotalPrize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public User User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateOnly CreationDate { get;set; }
        public bool IsPublic { get; set; }
    }
}
