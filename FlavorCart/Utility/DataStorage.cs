using FlavorCart.Models;
using Microsoft.AspNetCore.Components.HtmlRendering.Infrastructure;

namespace FlavorCart.Utility
{
    public class DataStorage
    {
        public static List<Lists> GetTestLists() =>
            new List<Lists>
            {
   new Lists { Id = "1", Name = "List 1", ArticleList = new List<ListItem>(), TotalPrice = 0, UserId = "1", CreationDate = "2021-06-01", IsPublic = true },
                new Lists { Id = "2", Name = "List 2", ArticleList = new List<ListItem>(), TotalPrice = 0, UserId = "2", CreationDate = "2021-06-02", IsPublic = false },
                new Lists { Id = "3", Name = "List 3", ArticleList = new List<ListItem>(), TotalPrice = 0, UserId = "3", CreationDate = "2021-06-03", IsPublic = true },
                new Lists { Id = "4", Name = "List 4", ArticleList = new List<ListItem>(), TotalPrice = 0, UserId = "4", CreationDate = "2021-06-04", IsPublic = false },
                new Lists { Id = "5", Name = "List 5", ArticleList = new List<ListItem>(), TotalPrice = 0, UserId = "5", CreationDate = "2021-06-05", IsPublic = true }
            };
    }
}
