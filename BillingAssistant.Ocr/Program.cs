using IronOcr;

IronOcr.License.LicenseKey = "IRONSUITE.IHSANOZKAYA1903.GMAIL.COM.16554-55802DE2A5-BVSVHP5-TT6QALTILPTZ-43M4K2FMH2MC-EH4PXQEKJM5C-5I3O6LTEDY6C-2SVKVKNUWDS2-NU2JUPRSG6HD-HEPHWB-TYTYVA4DOMKLUA-DEPLOYMENT.TRIAL-SO3UTD.TRIAL.EXPIRES.05.FEB.2024";

IronTesseract ocr = new();

ocr.Language = OcrLanguage.English;
ocr.AddSecondaryLanguage(OcrLanguage.Turkish);

using (var input = new OcrInput(@"C:\Users\Ali\source\repos\BillingAssistant.Ocr\samsungFatura.pdf"))
{
    var Result = ocr.Read(input);
    try
    {
        Result.SaveAsTextFile(@"C:\Users\Ali\source\repos\BillingAssistant.Ocr\Text.txt");
        for (int i = 0; i < Result.Words.Length; i++)
        {
            if (Result.Words[i].Text.ToLower() == "tutar")
            {
                Console.WriteLine(i + " Buldu");

            }
        }
        //Console.WriteLine(Result.Text);

    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occured: {ex.Message}");
    }
}