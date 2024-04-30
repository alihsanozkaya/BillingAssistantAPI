using BillingAssistant.Ocr;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        List<string> lines = new List<string>();
        List<Product> products = new List<Product>(); 

        string file = @"C:\Users\alihs\Desktop\KTÜN\KTÜN 4.Sınıf\Güz\Bitirme Projesi\OrnekFaturalar\ornekfatura1.pdf";

        using (PdfReader reader = new PdfReader(file))
        {
            for (int pageNo = 1; pageNo <= reader.NumberOfPages; pageNo++)
            {
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                string text = PdfTextExtractor.GetTextFromPage(reader, pageNo, strategy);
                text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                text = text.Replace("₺", "TL");
                string[] textLines = text.Split('\n');
                foreach (string line in textLines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        lines.Add(line.Trim());
                    }
                }
            }
        }
        for (int i = 0; i < lines.Count; i++)
        {
            Console.WriteLine("lines[{0}]: {1}", i, lines[i]);
        }
        Console.WriteLine("-----------------------------------------------");
        int startIndex = lines.IndexOf("ÜRÜN AÇIKLAMASI ADET FİYAT TOPLAM") + 1;
        int endIndex = lines.IndexOf("ARA TOPLAM");
        int productCount = (endIndex - startIndex) / 4;

        if (startIndex != -1 && endIndex != -1)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                string name = lines[i];
                int unit = 0;
                double price = 0;

                if (i + 5 < endIndex)
                {
                    if (int.TryParse(lines[i + productCount], out unit))
                    {
                        if (i + productCount*2 < endIndex)
                        {
                            if (double.TryParse(lines[i + productCount*2].Replace("TL", "").Trim(), out price))
                            {
                                Product product = new Product
                                {
                                    Name = name,
                                    Unit = unit,
                                    Price = price
                                };

                                products.Add(product);
                            }
                        }
                    }
                }
            }
            foreach (var product in products)
            {
                Console.WriteLine($"Ürün Adı: {product.Name} Adeti: {product.Unit} Fiyatı: {product.Price.ToString("0.00 TL", CultureInfo.InvariantCulture)}");
            }
        }
        else
        {
            Console.WriteLine("Ürünler bulunamadı.");
        }
    }
}
