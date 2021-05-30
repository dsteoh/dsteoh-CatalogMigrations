using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Mapper;
using FluentAssertions;
using Xunit;

namespace CatalogMigrations.Services.Tests.Mapper
{
    public class SuperCatalogMapperTests
    {
        private CsvParser _csvHelper;
        private SuperCatalogMapper _superCatalogMapper;
        private string _projectDirectory;

        public SuperCatalogMapperTests()
        {
            _superCatalogMapper = new SuperCatalogMapper();
            _csvHelper = new CsvParser();

            var workingDirectory = Environment.CurrentDirectory;
            _projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
        }
        
        [Fact]
        public void GetSuperCatalogFormat()
        {
            var catalogPath = Path.Combine(Path.GetDirectoryName(_projectDirectory),"TestData", "Catalogs","catalogA.csv");
            var supplierProductBarcodePath = Path.Combine(Path.GetDirectoryName(_projectDirectory),"TestData", "Barcodes","barcodesA.csv");
            
            var catalogList = _csvHelper.ParseToCatalogsToList(catalogPath);
            var supplierProductBarcodeList = _csvHelper.ParseToSupplierProductBarcodeToList(supplierProductBarcodePath);

            var matchedProducts = _superCatalogMapper.GetSuperCatalogFormat(supplierProductBarcodeList, catalogList, "A").ToList();
            matchedProducts.Should().BeOfType<List<SuperCatalog>>();
        }    
    }
}