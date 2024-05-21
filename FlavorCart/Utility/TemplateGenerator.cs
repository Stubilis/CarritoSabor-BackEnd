using FlavorCart.Models;
using FlavorCart.Repositories;
using FlavorCart.Enums;
using System.Text;

namespace FlavorCart.Utility
{
    public class TemplateGenerator
    {

        public static string ListsGetHTMLString(Lists list, List<Article> articles, string logoPath)
        {

            var sb = new StringBuilder();
            sb.AppendFormat(@"
                                <html>
                                    <head>
                                        <div class='logo'>
                                        <img src='{0}' alt='FlavorCart Logo' style='height:100px;'>
                                        </div>
                                    </head>
                                    <body>
                                        <div class='header'><h1>{1}</h1></div>
                                        <table align='center'>
                                            <tr>
                                                <th></th>
                                                <th>Name</th>
                                                <th>Amount</th>
                                                <th>Unit</th>
                                                <th>AvgPrice</th>
                                            </tr>", logoPath, list.Name);

            foreach (var item in list.ArticleList)
            {
                var article = articles.Find(a => a.Id == item.ArticleId);
                if (item.IsActive)
                {
                    sb.AppendFormat(@"<tr>
                                            <td><input type=""checkbox""></td>
                                            <td>{0}</td>
                                            <td>{1}</td>
                                            <td>{2}</td>
                                            <td>{3}</td>
                                          </tr>", article.Name, item.Amount, item.Unit, article.AveragePrice);
                }
                else
                {
                    sb.AppendFormat(@"<tr>
                                            <td><input type=""checkbox"" checked></td>
                                            <td>{0}</td>
                                            <td>{1}</td>
                                            <td>{2}</td>
                                            <td>{3}</td>
                                          </tr>", article.Name, item.Amount, item.Unit, article.AveragePrice);
                }
            }
            sb.Append(@"
                                        </table>
                                    </body>
                                </html>");
            return sb.ToString();
        }

        public static string RecipeGetHTMLString(Recipe recipe, List<Article> articles)
        {

            var sb = new StringBuilder();
            sb.AppendFormat(@"
                                <html>
                                    <head>
                    <img src='https://flavorcart.com/assets/images/logo.png' alt='FlavorCart Logo' style='width:100px;height:100px;'>
                                    </head>
                                    <body>
                                        <div class='header'><h1>{0}</h1></div>
                                        <table align='center'>
                                            <tr>
                                                <th></th>
                                                <th>Name</th>
                                                <th>Amount</th>
                                                <th>Unit</th>
                                                <th>AvgPrice</th>
                                            </tr>", recipe.Name);

            foreach (var item in recipe.ArticleList)
            {
                var article = articles.Find(a => a.Id == item.ArticleId);
                if (item.IsActive)
                {
                    sb.AppendFormat(@"<tr>
                                            <td><input type=""checkbox""></td>
                                            <td>{0}</td>
                                            <td>{1}</td>
                                            <td>{2}</td>
                                            <td>{3}</td>
                                          </tr>", article.Name, item.Amount, item.Unit, article.AveragePrice);
                }
                else
                {
                    sb.AppendFormat(@"<tr>
                                            <td><input type=""checkbox"" checked></td>
                                            <td>{0}</td>
                                            <td>{1}</td>
                                            <td>{2}</td>
                                            <td>{3}</td>
                                          </tr>", article.Name, item.Amount, item.Unit, article.AveragePrice);
                }
            }
               
            sb.AppendFormat(@"
                                        </table>
                         <div class='description'><h2>Description</h2>
                            <p>{0}</p>                            
                           </div>
                    </body>
                 </html>", recipe.Description);
            return sb.ToString();
        }
    }
}
    

