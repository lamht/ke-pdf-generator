using System;
using System.Collections.Generic;

namespace ke_pdf_generator
{
    public class RequestPdfModel
    {
        public String Header { get; set; }
        public List<String> Pages { get; set; }
        public String Footer { get; set; }
        public String BackgroundUrl { get; set; }
        public Margins Margins { get; set; } = new Margins();
    }
    public class Margins
    {
        public int Top { get; set; } = 70;

        public int Bottom { get; set; } = 25;

        public int Left { get; set; } = 20;

        public int Right { get; set; } = 20;
    }
}
