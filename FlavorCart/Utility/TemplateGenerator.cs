using FlavorCart.Models;
using FlavorCart.Repositories;
using FlavorCart.Enums;
using System.Text;

namespace FlavorCart.Utility
{
    public class TemplateGenerator
    {
                
        public static string GetHTMLString(Lists list, List<Article> articles)
        {

            var sb = new StringBuilder();
            sb.AppendFormat(@"
                            <html>
                                <head>
                                </head>
                                <body>
                                    <div class='header'><h1>{0}</h1></div>
                                    <table align='center'>
                                        <tr>
                                            <th>Name</th>
                                            <th>Amount</th>
                                            <th>Unit</th>
                                            <th>AvgPrice</th>
                                        </tr>", list.Name);

            foreach (var item in list.ArticleList)
            {
                var article = articles.Find(a => a.Id == item.ArticleId);
                sb.AppendFormat(@"<tr>
                                        <td>{0}</td>
                                        <td>{1}</td>
                                        <td>{2}</td>
                                        <td>{3}</td>
                                      </tr>", article.Name,item.Amount,item.Unit, article.AveragePrice);
            }
            sb.Append(@"
                                    </table>
                                </body>
                            </html>");
            return sb.ToString();
        }
    }
}
    

