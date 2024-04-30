using System;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Tesseract;

namespace BillingAssistant.Deneme
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            List<Product> products = new List<Product>();

            string imagePath = @"C:\Users\alihs\Desktop\KTÜN\KTÜN 4.Sınıf\Güz\Bitirme Projesi\OrnekFaturalar\ornekfatura2.jpg"; // OCR yapılacak resmin yolu
            string tessdataPath = @"C:\Users\alihs\.nuget\packages\tesseract\5.2.0\";

            using (var engine = new TesseractEngine(tessdataPath, "tur", EngineMode.Default)) // "tur" Türkçe için kullanılan dil kodudur.
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        string text = page.GetText();
                        text = text.Replace("TL", "");
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
            }
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine("lines[{0}]: {1}", i, lines[i]);
            }
            int startIndex = lines.IndexOf("ÜRÜN AÇIKLAMASI") + 1;
            int endIndex = lines.IndexOf("ÖDEME BİLGİSİ");
            int startIndex2 = lines.IndexOf("ADET FİYAT TOPLAM") + 1;

            if (startIndex != -1 && endIndex != -1 && startIndex2 != -1)
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    string name = lines[i];
                    string[] parts = lines[startIndex2].Split(' ');
                    int unit = int.Parse(parts[0]);
                    double price = double.Parse(parts[1]);

                    Product product = new Product
                    {
                        Name = name,
                        Unit = unit,
                        Price = price,
                    };
                    products.Add(product);
                    startIndex2++;
                }
            }
            // Ürünleri konsola yazdır
            foreach (var product in products)
            {
                Console.WriteLine($"Ürün: {product.Name}, Miktar: {product.Unit}, Fiyat: {product.Price}");
            }
        }
    }
}
