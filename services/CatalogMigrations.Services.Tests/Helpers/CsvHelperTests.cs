using System;
using System.Collections.Generic;
using System.IO;
using CatalogMigrations.DataModels.Models;
using Xunit;
using FluentAssertions;

namespace CatalogMigrations.Services.Tests.Helpers
{
    public class CsvHelperTests
    {
        private CsvHelper _csvHelper;
        private string _path;

        public CsvHelperTests()
        {
            _csvHelper = new CsvHelper();
            var workingDirectory = Environment.CurrentDirectory;
            var _projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            _path = Path.Combine(Path.GetDirectoryName(_projectDirectory),"TestFiles", "TestData.csv");
        }

        [Fact]
        public void ShouldDeserialize_CatalogCsv_ToList()
        {
            var catalogList = _csvHelper.ConvertCatalogCsvToList(_path + "filename");
            catalogList.Should().BeOfType<Catalog>();
        } 
        
        [Fact]
        public void ShouldDeserialize_SupplierCsv_ToList()
        {
            var supplierList = _csvHelper.ConvertCatalogCsvToList(_path + "filename");
            supplierList.Should().BeOfType<Supplier>();
        }
        
        [Fact]
        public void ShouldDeserialize_SupplierProductBarcodeCsv_ToList()
        {
            var supplierProductBarcodeList = _csvHelper.ConvertCatalogCsvToList(_path + "filename");
            supplierProductBarcodeList.Should().BeOfType<SupplierProductBarcode>();
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
            
            var x = _csvHelper.ConvertListToCsv(catalogList, "path");
            File.Exists("path").Should().BeTrue();
        }
        
        [Fact]
        public void ShouldContinueToPopulateWhenErrorIsThrown()
        {
            throw new NotImplementedException();
        }
    }
}