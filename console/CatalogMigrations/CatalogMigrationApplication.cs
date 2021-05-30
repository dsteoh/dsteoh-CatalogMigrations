using System;
using System.IO;
using System.Linq;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Jobs;

namespace CatalogMigrations
{
    public class CatalogMigrationApplication
    {
        private readonly ICsvParser _csvParser;
        private readonly ITransformCatalogJob _transformCatalogJob;

        public CatalogMigrationApplication(ICsvParser csvParser, ITransformCatalogJob catalogJob)
        {
            _csvParser = csvParser;
            _transformCatalogJob = catalogJob;
        }

        public void Run()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.Parent.FullName;

            var sampleDataPath = Path.Combine(projectDirectory, "services", "CatalogMigrations.Services.Tests", "TestData");
            var resultPath = Path.Combine(projectDirectory, "services", "CatalogMigrations.Services.Tests", "TestResults");

            while (true)
            {
                Console.WriteLine($@"Default files path is set to {sampleDataPath}" +
                                  $"\n****Option 1****: If you want to set run the test files under {sampleDataPath} press enter" +
                                  $"\n****Option 2****: Enter full path of barcodeA.csv to start" +
                                  $"\n****Option 3****: Place custom Barcodes/Catalogs/Suppliers.csv files under the path {sampleDataPath}" +
                                  $"\n****Note**** Files must follow the naming schema barcodesA.csv, catalogA.csv, supplierA.csv");

                var input = Console.ReadLine();

                try
                {
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        var barcodesA = _csvParser.ParseToSupplierProductBarcodeToList(input);

                        Console.WriteLine("Enter your Csv full path for catalogA");
                        input = Console.ReadLine();
                        var catalogA = _csvParser.ParseToCatalogsToList(input);

                        Console.WriteLine("Enter your Csv full path for barcodesB");
                        input = Console.ReadLine();
                        var barcodesB = _csvParser.ParseToSupplierProductBarcodeToList(input);

                        Console.WriteLine("Enter your Csv full path for catalogB");
                        input = Console.ReadLine();
                        var catalogB = _csvParser.ParseToCatalogsToList(input);

                        Console.WriteLine("Enter your output path");
                        var outputPath = input = Console.ReadLine();

                        var superCatalogs = _transformCatalogJob
                            .TransformCatalog(barcodesA, catalogA, barcodesB, catalogB).ToList();

                        _csvParser.WriteSuperCatalogToFile(outputPath, "SuperCatalog.csv", superCatalogs);

                        Console.WriteLine("File" + "SuperCatalog.csv " + "created at " + outputPath);
                    }
                    else if (input != null && !input.Equals("exit"))
                    {
                        var barcodesA =
                            _csvParser.ParseToSupplierProductBarcodeToList(sampleDataPath + "\\Barcodes\\barcodesA.csv");
                        var barcodesB =
                            _csvParser.ParseToSupplierProductBarcodeToList(sampleDataPath + "\\Barcodes\\barcodesB.csv");
                        var catalogA = _csvParser.ParseToCatalogsToList(sampleDataPath + "\\Catalogs\\catalogA.csv");
                        var catalogB = _csvParser.ParseToCatalogsToList(sampleDataPath + "\\Catalogs\\catalogB.csv");

                        var superCatalogs = _transformCatalogJob
                            .TransformCatalog(barcodesA, catalogA, barcodesB, catalogB).ToList();
                        _csvParser.WriteSuperCatalogToFile(resultPath, "SuperCatalog.csv", superCatalogs);

                        Console.WriteLine("File" + "SuperCatalog.csv " + "created at " + resultPath);
                        Console.WriteLine(
                            "Enter path for barcodeA.csv again or type exit to stop application.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}