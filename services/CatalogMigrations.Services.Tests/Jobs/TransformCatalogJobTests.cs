using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Jobs;
using CatalogMigrations.Services.Mapper;
using CsvHelper.TypeConversion;
using FluentAssertions;
using Moq;
using Xunit;

namespace CatalogMigrations.Services.Tests.Jobs
{
    public class TransformCatalogJobTests
    {
        private TransformCatalogJob _transformCatalogJob;

        public TransformCatalogJobTests()
        {
            Mock<IBarcodeMapper> mockBarcodeMapper = new Mock<IBarcodeMapper>();
            Mock<ISuperCatalogMapper> mockSuperCatalogMapper = new Mock<ISuperCatalogMapper>();

            _transformCatalogJob = new TransformCatalogJob(mockBarcodeMapper.Object, mockSuperCatalogMapper.Object);

            var lookUps = new List<string>()
            {
                "z2783613083800",
            };

            var newProductCompanyB = new List<SupplierProductBarcode>()
            {
                new()
                {
                    //Same SKU different product
                    Sku = "2222-1111-1112",
                    Barcode = "z2783613083801",
                    SupplierId = 4
                },
                new()
                {
                    //Different SKU new product
                    Sku = "2222-1111-0000",
                    Barcode = "z2783613093805",
                    SupplierId = 5
                }
            };

            var superCatalogA = new List<SuperCatalog>()
            {
                new()
                {
                    Sku = "2222-1111-1111",
                    Description = "Green Journeyman Backpack",
                    Source = "A"
                },
                new()
                {
                    Sku = "2222-1111-1111",
                    Description = "Journeyman Backpack",
                    Source = "A"
                },
                new()
                {
                    Sku = "2222-1111-1112",
                    Description = "Frostmourne",
                    Source = "A"
                },
            };
            
            var superCatalogB = new List<SuperCatalog>()
            {
                new()
                {
                    Sku = "2222-1111-1112",
                    Description = "Blue Journeyman Backpack",
                    Source = "B"
                },
                new()
                {
                    Sku = "2222-1111-0000",
                    Description = "Blue Journeyman Backpack",
                    Source = "B"
                }
            };

            mockBarcodeMapper.Setup(_ => _.GetExistingProductLookups(
                It.IsAny<List<SupplierProductBarcode>>(), 
                It.IsAny<List<SupplierProductBarcode>>())).Returns(lookUps);
            
            mockBarcodeMapper.Setup(_ => _.GetNewProducts(
                It.IsAny<List<SupplierProductBarcode>>(), 
                It.IsAny<List<SupplierProductBarcode>>(), 
                It.IsAny<List<string>>())).Returns(newProductCompanyB);

            mockSuperCatalogMapper.Setup(_ => _.GetSuperCatalogFormat(
                It.IsAny<List<SupplierProductBarcode>>(), 
                It.IsAny<List<Catalog>>(), 
                It.Is<string>(s => s == "A"))).Returns(superCatalogA);
            
            mockSuperCatalogMapper.Setup(_ => _.GetSuperCatalogFormat(
                It.IsAny<List<SupplierProductBarcode>>(), 
                It.IsAny<List<Catalog>>(), 
                It.Is<string>(s => s == "B"))).Returns(superCatalogB);
            
        }

        [Fact]
        public void TransformCatalog_ShouldReturn_SuperCatalog()
        {
            var result = new List<SuperCatalog>()
            {
                new()
                {
                    Sku = "2222-1111-1111",
                    Description = "Green Journeyman Backpack",
                    Source = "A"
                },
                new()
                {
                    Sku = "2222-1111-1111",
                    Description = "Journeyman Backpack",
                    Source = "A"
                },
                new()
                {
                    Sku = "2222-1111-1112",
                    Description = "Frostmourne",
                    Source = "A"
                },
                new()
                {
                    Sku = "2222-1111-1112",
                    Description = "Blue Journeyman Backpack",
                    Source = "B"
                },
                new()
                {
                    Sku = "2222-1111-0000",
                    Description = "Blue Journeyman Backpack",
                    Source = "B"
                }
            };
            
            var superCatalog = _transformCatalogJob.TransformCatalog(
                new List<SupplierProductBarcode>(),
                new List<Catalog>(), 
                new List<SupplierProductBarcode>(),
                new List<Catalog>());
            
            superCatalog.Should().Equals(result);

        }
    }
}