using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using FlavorCart.Utility;
using System.IO;
using FlavorCart.Models;
using FlavorCart.Repositories;
using Firestore.Controllers;

namespace PDF_Generator.Controllers
{
    [Route("api/pdfcreator")]
    [ApiController]
    public class PdfCreatorController : ControllerBase
    {
        private readonly ListsRepository _listsRepository = new();
        private readonly RecipeRepository _recipeRepository = new();
        private readonly ArticleRepository _articleRepository = new();
        private UserTokenController _usertokenFirestoreController;
        private IConverter _converter;

        public PdfCreatorController(IConverter converter)
        {
            _converter = converter;
        }/*
        public PdfCreatorController(ILogger<UserFirestoreController> logger)
        {
            _usertokenFirestoreController = new UserTokenController(logger);
        }*/

        [HttpGet]
        [Route("list/{id}")]
        
        public async Task<IActionResult> ListsCreatePDFAsync(string  id)
        {
    
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
            List <Article> articles = new List<Article>();
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
                HtmlContent = TemplateGenerator.ListsGetHTMLString(_list, articles),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
          
            var file = _converter.Convert(pdf);

            //return Ok("Successfully created PDF document.");
            return File(file, "application/pdf", _list.Name+".pdf");
            //return File(file, "application/pdf");
        }

        [HttpGet]
        [Route("recipe/{id}")]
        public async Task<IActionResult> RecipeCreatePDFAsync(string id)
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
                HtmlContent = TemplateGenerator.RecipeGetHTMLString(_recipe, articles),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

           
            return File(file, "application/pdf", _recipe.Name + ".pdf");
            
        }
    }
}