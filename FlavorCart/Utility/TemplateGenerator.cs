using FlavorCart.Models;
using FlavorCart.Repositories;
using FlavorCart.Enums;
using System.Text;
using System.Collections.Generic;

namespace FlavorCart.Utility
{
    public class TemplateGenerator
    {

        public static string ListsGetHTMLString(Lists list, List<Article> articles, string logoPath, string username)
        {
            //TO DO: split the list each 35 items to avoid overflow in the pdf

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
                                        <table  align='center'>
                                            <tr>
                                                <th></th>
                                                <th>Nombre</th>
                                                <th>Cantidad</th> 
                                                <th>Precio</th>
                                            </tr>", logoPath, username, list.Name);

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
                                          </tr>", article.Name, item.Amount, item.Unit, Math.Round(article.AveragePrice, 2));
                    }
                    else
                    {
                        sb.AppendFormat(@"<tr>
                                            <td><input type=""checkbox"" checked></td>
                                            <td>{0}</td>
                                            <td>{1} {2}</td>
                                            <td>{3} €</td>
                                          </tr>", article.Name, item.Amount, item.Unit, Math.Round(article.AveragePrice, 2));
                    }
                }
                sb.AppendFormat(@" 
                                      <tr>
                                        <th></th>
                                        <th></th>
                                        <th>Total</th>
                                        <td class ='total'>{0} €</td>
                                        </tr>
                                        </table>
                                       </div>
                                        
                                    </body>
                                </html>", Math.Round(list.TotalPrice, 2));
                return sb.ToString();
         
        }

        

        public static string RecipeGetHTMLString(Recipe recipe, List<Article> articles, string logoPath)
        {

            var sb = new StringBuilder();
            sb.AppendFormat(@"
                                 <html>
                                    <head>
                                        <div class='logo'>
                                        <img src='{0}' alt='FlavorCart Logo''>
                                        
                                        </div>
                                    </head>
                                    <body>
                                        <div class='header'><h1>{1}</h1></div>
                                        <div class='table'>
                                            <table  align='center'>
                                            <tr>
                                                <th>Nombre</th>
                                                <th>Cantidad</th>
                                                <th>Precio</th>
                                            </tr>", logoPath, recipe.Name);

            foreach (var item in recipe.ArticleList)
            {
                var article = articles.Find(a => a.Id == item.ArticleId);
                
                
                    sb.AppendFormat(@"<tr>
                                           
                                           <td>{0}</td>
                                           <td>{1} {2}</td>
                                           <td>{3} €</td>
                                          </tr>", article.Name, item.Amount, item.Unit, Math.Round(article.AveragePrice, 2));
               
              
            }
          
            sb.AppendFormat(@"
                                       <tr>
                                        <th></th>
                                        <th>Total</th>
                                        <td class ='total'>{0} €</td>
                                        </tr>
                                        </table>
                                        </table>
                         <div class='description'><h2>Description</h2>
                            <p>{1}</p>                            
                           </div>

                    </body>
                 </html>", Math.Round(recipe.TotalPrice,2), recipe.Description);
            return sb.ToString();
        }
    }
}
    

