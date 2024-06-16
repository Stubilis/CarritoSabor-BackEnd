using FlavorCart.Models;

namespace FlavorCart.Interfaces
{
    /// <summary>
    /// Represents the base data that will exists on all lists and recipes.
    /// </summary>
    public interface IList
    {
     
        
        List<ListItem> ArticleList { get; set; }
        
        string UserId { get; set; }
     
        float TotalPrice { get; set; }


       
    }
}
