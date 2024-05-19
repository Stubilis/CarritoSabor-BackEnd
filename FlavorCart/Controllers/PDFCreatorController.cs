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
        private readonly ArticleRepository _articleRepository = new();
        private IConverter _converter;

        public PdfCreatorController(IConverter converter)
        {
            _converter = converter;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> CreatePDFAsync(string  id)
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
            foreach (var item in _list.ArticleList)
            {
                Article article = new Article()
                {
                    Id = item.ArticleId
                };
                articles.Add(await _articleRepository.GetAsync(article));
                
            }
                     

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
               // Out = @"C:\PDFCreator\"+_list.Name+".pdf"  //USE THIS PROPERTY TO SAVE PDF TO A PROVIDED LOCATION
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(_list, articles),
               // Page = "https://code-maze.com/", //USE THIS PROPERTY TO GENERATE PDF CONTENT FROM AN HTML PAGE
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            //_converter.Convert(pdf);// IF WE USE Out PROPERTY IN THE GlobalSettings CLASS, THIS IS ENOUGH FOR CONVERSION

            var file = _converter.Convert(pdf);

            //return Ok("Successfully created PDF document.");
            return File(file, "application/pdf", _list.Name+".pdf");
            //return File(file, "application/pdf");
        }
    }
}