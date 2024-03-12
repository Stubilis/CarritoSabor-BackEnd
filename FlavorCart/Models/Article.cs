namespace FlavorCart.Models
{
    public class Article
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private string Image { get; set; }
        private string Brand { get; set; }
        private float[] Prize { get; set; }
        private string[] Categories { get; set; }
        private float AveragePrize { get; set; }
        //Cantidad

    }
}
