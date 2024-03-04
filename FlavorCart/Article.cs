namespace FlavorCart
{
    public class Article
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }
        public float[] Prize { get; set; }
        public string[] Categories { get; set; }
        public float AveragePrize { get; set; } 
      
    }
}
