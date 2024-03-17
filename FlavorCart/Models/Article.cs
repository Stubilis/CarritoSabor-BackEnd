using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace FlavorCart.Models
{
    [PageModel]
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
        public int Count { get; set; } //cantidad de articulos
        public string Unit { get; set; } //unidad de medida como gramos, kilos, litros, etc
        

        //Set average prize with the prize array
        public void SetAveragePrize()
        {
            float sum = 0;
            foreach (float prize in Prize)
            {
                sum += prize;
            }
            this.AveragePrize = sum / Prize.Length;
        }


    }
}
