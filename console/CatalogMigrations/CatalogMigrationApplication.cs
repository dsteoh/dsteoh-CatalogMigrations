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
            var barcodeAPath = Path.Combine(sampleDataPath, "Barcodes", "barcodesA.csv");
            var catalogAPath = Path.Combine(sampleDataPath, "Catalogs", "catalogA.csv");
            var barcodeBPath = Path.Combine(sampleDataPath, "Barcodes", "barcodesB.csv");
            var catalogBPath = Path.Combine(sampleDataPath, "Catalogs", "catalogB.csv");
            
            Console.WriteLine(
                $"\n=====================================" +
                $"\n[ Migration Catalog Project started1 ]" +
                $"\n=====================================\n" +
                $"\n****Option 1****: Enter full path of barcodeA.csv to start" +
                $"\n****Option 2****: If you want to set run the test files under {sampleDataPath} press enter" +
                $"\n****Option 3****: Place custom Barcodes\\Catalogs\\Suppliers.csv files under the path {sampleDataPath}" +
                $"\n++++Note++++" +
                $"\nFiles must follow the naming schema barcodesA.csv, catalogA.csv, supplierA.csv for option 3\n"
                );

            while (true)
            {
                Console.WriteLine("Waiting for input... Please enter barcodeA.csv for option 1");
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

                        Console.WriteLine("Enter csv file name with extension (.csv)");
                        var fileName = input = Console.ReadLine();

                        var superCatalogs = _transformCatalogJob
                            .TransformCatalog(barcodesA, catalogA, barcodesB, catalogB).ToList();

                        _csvParser.WriteSuperCatalogToFile(outputPath, fileName ?? "SuperCatalog.csv", superCatalogs);

                        Console.WriteLine("Migrated files to " + fileName + " created at " + outputPath + "\n");
                    }
                    else if (input != null && !input.Equals("exit"))
                    {
                        var barcodesA =
                            _csvParser.ParseToSupplierProductBarcodeToList(barcodeAPath);
                        var barcodesB =
                            _csvParser.ParseToSupplierProductBarcodeToList(barcodeBPath);
                        var catalogA = _csvParser.ParseToCatalogsToList(catalogAPath);
                        var catalogB = _csvParser.ParseToCatalogsToList(catalogBPath);

                        var superCatalogs = _transformCatalogJob
                            .TransformCatalog(barcodesA, catalogA, barcodesB, catalogB).ToList();
                        _csvParser.WriteSuperCatalogToFile(resultPath, "SuperCatalog.csv", superCatalogs);

                        Console.WriteLine("Migrated file SuperCatalog.csv created at " + resultPath + "\n");
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