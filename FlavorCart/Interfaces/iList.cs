using FlavorCart.Models;

namespace FlavorCart.Interfaces
{
    public interface iList
    {
        //id,name,array of articles,prize
        string[] ArticleList { get; set; }
        

        string UserId { get; set; }
     
        float TotalPrize { get; set; }


        //Set total prize with the prize array of articles
        /*
        void SetTotalPrize()
        {
            foreach (Article article in ArticleList)
            {
                TotalPrize += article.AveragePrize;
            }

        }

        */
    }
}
