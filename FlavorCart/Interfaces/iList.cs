using FlavorCart.Models;

namespace FlavorCart.Interfaces
{
    public interface iList
    {
      
        
        List<ListItem> ArticleList { get; set; }
        
        string UserId { get; set; }
     
        float TotalPrice { get; set; }


       
    }
}
