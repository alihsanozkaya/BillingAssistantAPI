using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tesseract;

namespace BillingAssistant.Deneme
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            List<string> lines = new List<string>();

            // OCR yapılacak resmin yolu
            string imagePath = @"C:\Users\alihs\Desktop\KTÜN\KTÜN 4.Sınıf\Güz\Bitirme Projesi\OrnekFaturalar\migros.jpg";

            // Tesseract OCR motorunun dosya yolu (tessdata klasörü içeren yol)
            string tessdataPath = @"C:\Users\alihs\.nuget\packages\tesseract\5.2.0\";

            // OCR motorunun başlatılması
            using (var engine = new TesseractEngine(tessdataPath, "tur", EngineMode.Default))
            {
                // OCR işlemi için resmin yüklenmesi ve işlenmesi
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        string text = page.GetText();
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
            // A101 tespit edildikten sonra TOPKDV'ye kadar olan kısmı almak
            List<string> filteredLines = new List<string>();
            bool foundA101 = false;
            bool reachedEnd = false;
            foreach (string line in lines)
            {
                if (line.Contains("A101") || line.Contains("FILE") || line.Contains("MIGROS"))
                {
                    foundA101 = true;
                }

                if (foundA101)
                {
                    if (line.Contains("TOPKDV"))
                    {
                        reachedEnd = true;
                    }

                    if (!reachedEnd)
                    {
                        filteredLines.Add(line);
                    }
                }
            }

            // İstenen satırların ürün adı ve fiyatını almak
            List<Product> products = new List<Product>();
            foreach (string line in filteredLines)
            {
                if (line.Contains("*"))
                {
                    string[] parts = line.Split('*');
                    string productName = parts[0].Trim();

                    // % işaretinden öncesini almak
                    int percentIndex = productName.IndexOf('%');
                    if (percentIndex != -1)
                    {
                        productName = productName.Substring(0, percentIndex).Trim();
                    }

                    string pricePart = parts[1].Trim();

                    // Fiyat kısmının sonunda nokta (.) yerine virgül (,) kullanılmış olabilir, bunu dikkate alarak işlem yapalım
                    pricePart = pricePart.Replace(".", ",");

                    // Fiyat kısmını alıp boşlukları temizleyelim
                    string priceString = new string(pricePart.Where(c => char.IsDigit(c) || c == ',').ToArray());
                    double price = double.Parse(priceString);

                    products.Add(new Product { Name = productName, Price = price });
                }
            }

            // Sonuçların konsola yazdırılması
            foreach (var product in products)
            {
                Console.WriteLine("Product Name: " + product.Name);
                Console.WriteLine("Price: " + product.Price);
                Console.WriteLine();
            }
        }
    }
}
