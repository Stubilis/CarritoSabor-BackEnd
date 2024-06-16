using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using FlavorCart.Utility;
using System.IO;
using FlavorCart.Models;
using FlavorCart.Repositories;
using Firestore.Controllers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PDF_Generator.Controllers
{
    [Route("api/pdfcreator")]
    [ApiController]
    public class PdfCreatorController : ControllerBase
    {
        private readonly ListsRepository _listsRepository = new();
        private readonly RecipeRepository _recipeRepository = new();
        private readonly ArticleRepository _articleRepository = new();
        private readonly UserRepository _userRepository = new();
        private UserTokenController _usertokenFirestoreController;
        private readonly ILogger<UserFirestoreController> _logger;
        private IConverter _converter;

        public PdfCreatorController(IConverter converter, ILogger<UserFirestoreController> logger)
        {
            _converter = converter;
            _logger = logger;
            _usertokenFirestoreController = new UserTokenController(logger);
        }
        private bool Verified(string token)
        {
            try
            {
                var ok = _usertokenFirestoreController.Verify(token.Remove(0, 7));
                if (ok != null)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        [HttpGet]
        [Route("list/{id}")]

        public async Task<IActionResult> ListsCreatePDFAsync(string id)
        {
            if (Verified(Request.Headers["Authorization"].ToString())) { 
                //Get the data from the database
                Lists lists = new Lists()
                {
                    Id = id
                };
            var _list = await _listsRepository.GetAsync(lists);
            if (_list == null)
            {
                return NotFound();
            }
            //Get the list of articles from the list
            List<Article> articles = new List<Article>();
            if (_list.ArticleList.Count > 0)
            {

                foreach (var item in _list.ArticleList)
                {
                    Article article = new Article()
                    {
                        Id = item.ArticleId
                    };
                    try
                    {
                        articles.Add(await _articleRepository.GetAsync(article));
                    } catch (NullReferenceException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
                //Get the user name 
                User user = await _userRepository.GetAsync(new User() { Id = _list.UserId });

            //Set the PDF settings
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = _list.Name,

            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                //Get the HTML string from the template generator
                HtmlContent = TemplateGenerator.ListsGetHTMLString(_list, articles, Path.Combine(Directory.GetCurrentDirectory(), "assets", "logo.png"),user.Nickname ?? ""),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Verdana, Geneva, Tahoma, sans-serif", FontSize = 9, Right = "Página [page] de [toPage]", Line = true },
               
                
                FooterSettings = 
                { FontName = "Verdana, Geneva, Tahoma, sans-serif",
                    FontSize = 9,
                    Line = true, 
                    Center = "Carrito de Sabor 2024©",
                    
                    }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return File(file, "application/pdf", _list.Name + ".pdf");
        }
            return BadRequest("Invalid token");
        
    }

        [HttpGet]
        [Route("recipe/{id}")]
        public async Task<IActionResult> RecipeCreatePDFAsync(string id)
        {
            if (Verified(Request.Headers["Authorization"].ToString()))
            {
                //Get the data from the database
                Recipe recipe = new Recipe()
                {
                    Id = id
                };
                var _recipe = await _recipeRepository.GetAsync(recipe);
                if (_recipe == null)
                {
                    return NotFound();
                }
                //Get the list of articles from the list
                List<Article> articles = new List<Article>();
                if (_recipe.ArticleList.Count > 0)
                {

                    foreach (var item in _recipe.ArticleList)
                    {
                        Article article = new Article()
                        {
                            Id = item.ArticleId
                        };
                        try
                        {
                            articles.Add(await _articleRepository.GetAsync(article));
                        }
                        catch (NullReferenceException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = recipe.Name,

                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = TemplateGenerator.RecipeGetHTMLString(_recipe, articles, Path.Combine(Directory.GetCurrentDirectory(), "assets", "logo.png")),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                    HeaderSettings = { FontName = "Verdana, Geneva, Tahoma, sans-serif", FontSize = 9, Right = "Página [page] de [toPage]", Line = true },
                    FooterSettings = { FontName = "Verdana, Geneva, Tahoma, sans-serif", FontSize = 9, Line = true, Center = "Carrito de Sabor 2024©" }
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

                var file = _converter.Convert(pdf);


                return File(file, "application/pdf", _recipe.Name + ".pdf");

            }
            return BadRequest("Invalid token");
        }
    }
}