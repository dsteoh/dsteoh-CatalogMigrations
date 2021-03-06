using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using FluentAssertions;
using Xunit;

namespace CatalogMigrations.Services.Tests.Helpers.Csv
{
    public class CsvHelperTests
    {
        private readonly CsvParser _csvHelper;
        private readonly string _projectDirectory;

        public CsvHelperTests()
        {
            _csvHelper = new CsvParser();
            var workingDirectory = Environment.CurrentDirectory;
            _projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
        }

        [Fact]
        public void ShouldDeserialize_CatalogCsv_ToList()
        {
            var path = Path.Combine(Path.GetDirectoryName(_projectDirectory),"TestData", "Catalogs","catalogA.csv");
            var catalogList = _csvHelper.ParseToCatalogsToList(path);
            catalogList.ToList().Should().BeOfType<List<Catalog>>();
        }

        [Fact]
        public void ShouldDeserialize_SupplierCsv_ToList()
        {
            var path = Path.Combine(Path.GetDirectoryName(_projectDirectory),"TestData", "Suppliers","suppliersA.csv");
            var supplierList = _csvHelper.ParseToSupplierToList(path);
            supplierList.Should().BeOfType<List<Supplier>>();
        }

        [Fact]
        public void ShouldDeserialize_SupplierProductBarcodeCsv_ToList()
        {
            var path = Path.Combine(Path.GetDirectoryName(_projectDirectory),"TestData", "Barcodes","barcodesA.csv");
            var supplierProductBarcodeList = _csvHelper.ParseToSupplierProductBarcodeToList(path);
            supplierProductBarcodeList.Should().BeOfType<List<SupplierProductBarcode>>();
        }

        [Fact]
        public void ShouldWriteToFile()
        {
            var superCatalogList = new List<SuperCatalog>
            {
                new()
                {
                    Sku = "1111-111-111",
                    Description = "Test Item",
                    Source = "A"
                }
            };
            var path = Path.Combine(Path.GetDirectoryName(_projectDirectory),"TestResults");
            _csvHelper.WriteSuperCatalogToFile(path, "SuperCatalog.csv", superCatalogList);
            var writeFilePath = Path.Combine(path, "SuperCatalog.csv");
            File.Exists(writeFilePath).Should().BeTrue();
        }
    }
}