using FlavorCart.Interfaces;

namespace FlavorCart.Models
{
    public class Recipe : iList
    {
        public Article[] ArticleList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public User User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float TotalPrize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
