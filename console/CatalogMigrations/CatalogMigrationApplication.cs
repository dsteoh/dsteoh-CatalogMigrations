using System;
using System.IO;
using System.Linq;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Jobs;

namespace CatalogMigrations
{
    public class CatalogMigrationApplication
    {
        private ICsvParser _csvParser;
        private ITransformCatalogJob _transformCatalogJob;
        private string workingDirectory = Environment.CurrentDirectory;

        public CatalogMigrationApplication(ICsvParser csvParser, ITransformCatalogJob catalogJob)
        {
            _csvParser = csvParser;
            _transformCatalogJob = catalogJob;
        }

        public void Run()
        {
            Console.WriteLine("Enter your Csv full path for barcodesA" +
                              $"\nTo quit type exit");

            try
            {
                var input = Console.ReadLine();
                while (input != "exit")
                {
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        var barcodesA = _csvParser.ParseToSupplierProductBarcodeToList(input);

                        Console.WriteLine("Enter Enter your Csv full path for catalogA");
                        input = Console.ReadLine();
                        var catalogA = _csvParser.ParseToCatalogsToList(input);

                        Console.WriteLine("Enter Enter your Csv full path for barcodesB");
                        input = Console.ReadLine();
                        var barcodesB = _csvParser.ParseToSupplierProductBarcodeToList(input);

                        Console.WriteLine("Enter Enter your Csv full path for catalogB");
                        input = Console.ReadLine();
                        var catalogB = _csvParser.ParseToCatalogsToList(input);

                        var superCatalogs = _transformCatalogJob
                            .TransformCatalog(barcodesA, catalogA, barcodesB, catalogB).ToList();

                        _csvParser.WriteSuperCatalogToFile("C:\\Users\\DS\\Desktop", "SuperCatalog.csv", superCatalogs);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}