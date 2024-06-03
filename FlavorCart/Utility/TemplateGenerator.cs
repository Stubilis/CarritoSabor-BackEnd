using FlavorCart.Models;
using FlavorCart.Repositories;
using FlavorCart.Enums;
using System.Text;

namespace FlavorCart.Utility
{
    public class TemplateGenerator
    {

        public static string ListsGetHTMLString(Lists list, List<Article> articles, string logoPath,string username)
        {

            var sb = new StringBuilder();
            sb.AppendFormat(@"
                                <html>
                                    <head>
                                        <div class='logo'>
                                        <img src='{0}' alt='FlavorCart Logo''>
                                        <p> Lista de {1} </p>
                                        </div>
                                    </head>
                                    <body>
                                        <div class='header'><h1>{2}</h1></div>
                                        <div class='table'>
                                        <table>
                                            <tr>
                                                <th></th>
                                                <th>Nombre</th>
                                                <th>Cantidad</th> 
                                                <th>Precio</th>
                                            </tr>", logoPath,username, list.Name);

            foreach (var item in list.ArticleList)
            {
                var article = articles.Find(a => a.Id == item.ArticleId);
                if (item.IsActive)
                {
                    sb.AppendFormat(@"<tr>
                                            <td><input type=""checkbox""></td>
                                            <td>{0}</td>
                                            <td>{1} {2}</td>
                                            <td>{3} €</td>
                                          </tr>", article.Name, item.Amount, item.Unit, article.AveragePrice);
                }
                else
                {
                    sb.AppendFormat(@"<tr>
                                            <td><input type=""checkbox"" checked></td>
                                            <td>{0}</td>
                                            <td>{1} {2}</td>
                                            <td>{3} €</td>
                                          </tr>", article.Name, item.Amount, item.Unit, article.AveragePrice );
                }
            }
            sb.AppendFormat(@" 
                                      <tr>
                                        <td></td>
                                        <td></td>
                                        <th>Total</th>
                                        <td class ='total'>{0}</td>
                                        </tr>
                                        </table>
                                       </div>
                                        
                                    </body>
                                </html>", list.TotalPrice);
            return sb.ToString();
        }

        public static string RecipeGetHTMLString(Recipe recipe, List<Article> articles)
        {

            var sb = new StringBuilder();
            sb.AppendFormat(@"
                                <html>
                                    <head>
                    <img src='https://flavorcart.com/assets/images/logo.png' alt='FlavorCart Logo''>
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
    

