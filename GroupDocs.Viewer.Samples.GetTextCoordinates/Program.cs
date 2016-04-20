using System;
using System.Collections.Generic;
using System.IO;
using GroupDocs.Viewer.Config;
using GroupDocs.Viewer.Domain;
using GroupDocs.Viewer.Domain.Containers;
using GroupDocs.Viewer.Domain.Image;
using GroupDocs.Viewer.Domain.Options;
using GroupDocs.Viewer.Handler;

namespace GroupDocs.Viewer.Samples.GetTextCoordinates
{
    class Program
    {
        static void Main(string[] args)
        {
            SetLicense();

            string executionDirectory = Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);

            ViewerConfig viewerConfig = new ViewerConfig();
            viewerConfig.StoragePath = executionDirectory;
            viewerConfig.UsePdf = true;

            ViewerImageHandler viewerImageHandler = new ViewerImageHandler(viewerConfig);

            string guid = "Resources\\sample.pdf";

            DocumentInfoOptions documentInfoOptions =
                new DocumentInfoOptions(guid);
            DocumentInfoContainer documentInfoContainer =
                viewerImageHandler.GetDocumentInfo(documentInfoOptions);

            foreach (PageData pageData in documentInfoContainer.Pages)
            {
                Console.WriteLine("Page number: " + pageData.Number);

                for (int i = 0; i < pageData.Rows.Count; i++)
                {
                    RowData rowData = pageData.Rows[i];

                    Console.WriteLine("Row: " + (i + 1));
                    Console.WriteLine("Text: " + rowData.Text);
                    Console.WriteLine("Text width: " + rowData.LineWidth);
                    Console.WriteLine("Text height: " + rowData.LineHeight);
                    Console.WriteLine("Distance from left: " + rowData.LineLeft);
                    Console.WriteLine("Distance from top: " + rowData.LineTop);

                    string[] words = rowData.Text.Split(' ');

                    for (int j = 0; j < words.Length; j++)
                    {
                        int coordinateIndex = j == 0 ? 0 : j + 1;

                        Console.WriteLine(string.Empty);
                        Console.WriteLine("Word: '" + words[j] + "'");
                        Console.WriteLine("Word distance from left: " + rowData.TextCoordinates[coordinateIndex]);
                        Console.WriteLine("Word width: " + rowData.TextCoordinates[coordinateIndex + 1]);
                        Console.WriteLine(string.Empty);
                    }
                }

                Console.WriteLine(string.Empty);
            }

            Console.ReadKey();
        }

        private static void SetLicense()
        {
            const string licensePath = "c:\\licenses\\GroupDocs.Viewer.lic";
            License license = new License();
            using (FileStream fileStream = new FileStream(licensePath, FileMode.Open))
                license.SetLicense(fileStream);
        }
    }
}
