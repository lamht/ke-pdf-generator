using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.IO;

namespace ke_pdf_generator.Services
{
    public class Pdf
    {
        IConverter _converter;
        public Pdf(IConverter converter)
        {
            _converter = converter;
        }
        public byte[] ConvertPdf(RequestPdfModel requestModel)
        {
            var margins = requestModel.Margins;
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = { Top = margins.Top, Bottom = margins.Bottom, Left = margins.Left, Right = margins.Right, Unit = DinkToPdf.Unit.Millimeters }
                },
            };

            
            //setting header, footer
            string headerPath = String.Empty;
            string footerPath = String.Empty;
            if (!String.IsNullOrEmpty(requestModel.Header))
            {
                Console.WriteLine("Add Header");
                headerPath = GetHeaderPath(requestModel.Header);
                
            }
            if (!String.IsNullOrEmpty(requestModel.Footer))
            {
                Console.WriteLine("Add Footer");
                footerPath = GetFooterPath(requestModel.Footer);
                
            }
            //setting content
            foreach(var pageContent in requestModel.Pages)
            {
                var page = new ObjectSettings()
                {
                    PagesCount = true,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HtmlContent = pageContent,
                };
                if (!String.IsNullOrEmpty(headerPath)) page.HeaderSettings = new HeaderSettings { HtmUrl = headerPath };
                if (!String.IsNullOrEmpty(footerPath)) page.FooterSettings = new FooterSettings { HtmUrl = footerPath };
                //gen pdf
                pdf.Objects.Add(page);
            }
            
            byte[] data = _converter.Convert(pdf);
            //clear
            if (!String.IsNullOrEmpty(headerPath)) File.Delete(headerPath);
            if (!String.IsNullOrEmpty(footerPath)) File.Delete(footerPath);
            return data;
        }

        public string GetHeaderPath(String data)
        {
            string html = data;
            //html.Replace
            string pathDir = Path.Combine(Directory.GetCurrentDirectory(), "html");
            string path = Path.Combine(pathDir, $"header_{DateTime.UtcNow:yyyyMMddHHmmss}.html");
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            File.WriteAllText(path, html);

            return path;
        }

        public string GetFooterPath(String data)
        {
            string html = data;
            string pathDir = Path.Combine(Directory.GetCurrentDirectory(), "html");
            string path = Path.Combine(pathDir, $"footer_{DateTime.UtcNow:yyyyMMddHHmmss}.html");
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            File.WriteAllText(path, html);

            return path;
        }
    }
}
