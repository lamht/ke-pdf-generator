using iTextSharp.text.pdf;
using System;
using System.IO;

namespace ke_pdf_generator.Services
{
    public class BackgroundPdf
    {
        public void AddBackground(string file, string backgroundUrl)
        {
            Uri uri;
            try
            {
                uri = new Uri(backgroundUrl);
            }
            catch (Exception)
            {
                Console.WriteLine("Can not get backgroundUrl " + backgroundUrl);
                return;
            }

            PdfReader pdfReader = new PdfReader(file);
            PdfStamper stamp = new PdfStamper(pdfReader, new FileStream(file.Replace(".pdf", "[temp][file].pdf"), FileMode.Create));
            //optional: if image is wider than the page, scale down the image to fit the page
            var sizeWithRotation = pdfReader.GetPageSizeWithRotation(1);
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(uri);
            if (img.Width > sizeWithRotation.Width)
                img.ScalePercent(sizeWithRotation.Width / img.Width * 100);

            //set image position in top left corner
            //in pdf files, cooridinates start in the left bottom corner
            img.SetAbsolutePosition(0, sizeWithRotation.Height - img.ScaledHeight);
            // set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
            Console.WriteLine("pdfReader.NumberOfPages " + pdfReader.NumberOfPages);
            PdfContentByte waterMark;
            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            {
                Console.WriteLine("Set bg page " + page);
                waterMark = stamp.GetUnderContent(page);
                waterMark.AddImage(img);
            }
            stamp.FormFlattening = true;
            stamp.Close();

            // now delete the original file and rename the temp file to the original file
            File.Delete(file);
            File.Move(file.Replace(".pdf", "[temp][file].pdf"), file);
        }
    }
}
