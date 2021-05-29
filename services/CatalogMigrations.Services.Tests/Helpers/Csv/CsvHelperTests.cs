using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using Xunit;
using FluentAssertions;

namespace CatalogMigrations.Services.Tests.Helpers
{
    public class CsvHelperTests
    {
        private CsvParser _csvHelper;
        private string _projectDirectory;

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
        public void ShouldConvertListToCsv()
        {
            var catalogList = new List<Catalog>
            {
                new()
                {
                    Sku = "1111-111-111",
                    Description = "Test Item"
                }
            };
            
            // var x = _csvHelper.ConvertListToCsv(catalogList, "path");
            // File.Exists("path").Should().BeTrue();
        }
    }
}