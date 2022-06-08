using ke_pdf_generator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System;

namespace ke_pdf_generator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PdfController : ControllerBase
    {

        private readonly ILogger<PdfController> _logger;
        private readonly Pdf _pdf;

        public PdfController(ILogger<PdfController> logger, Pdf pdf)
        {
            _logger = logger;
            _pdf = pdf;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(200, "Hello, Pdf Generator!");
        }

        [HttpPost]
        public IActionResult Post(RequestPdfModel requestModel)
        {
            Console.WriteLine("Hello, Pdf Generator!");
            if (requestModel.Pages == null || requestModel.Pages.Count == 0) return StatusCode(400, "Pages is empty");

            var result = _pdf.ConvertPdf(requestModel);
            string pathDir = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "pdf");
            string path = System.IO.Path.Combine(pathDir, $"pdf_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf");
            if (!System.IO.Directory.Exists(pathDir))
            {
                System.IO.Directory.CreateDirectory(pathDir);
            }
            System.IO.File.WriteAllBytes(path, result);
            Console.WriteLine("Add background");
            if (!String.IsNullOrEmpty(requestModel.BackgroundUrl))
            {
                new BackgroundPdf().AddBackground(path, requestModel.BackgroundUrl);
            }
            Console.WriteLine(path);
            return new FileContentResult(System.IO.File.ReadAllBytes(path), GetContentType(path));
        }

        private string GetContentType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return "application/octet-stream";
            fileName = System.IO.Path.GetFileName(fileName);
            if (!new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
