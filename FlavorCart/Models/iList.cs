namespace FlavorCart.Models
{
    public interface iList
    {
        //id,name,array of articles,prize
        Article[] ArticleList { get; set; }
        string Name { get; set; }

        User User { get; set; }
        int Id { get; set; }
        float TotalPrize { get; set; }


        //Set total prize with the prize array of articles
        void SetTotalPrize()
        {
            foreach (Article article in ArticleList)
            {
                this.TotalPrize += article.AveragePrize;
            }
           
        }
       
      
    }
}
