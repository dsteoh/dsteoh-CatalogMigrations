using System;
using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Jobs;
using CatalogMigrations.Services.Mapper;
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
            
            var _barcodeB = new List<SupplierProductBarcode>()
            {
                new()
                {
                    Sku = "2222-1111-1112",
                    Barcode = "z2783613083800",
                    SupplierId = 2
                },
                new()
                {
                    Sku = "2222-1111-1111",
                    Barcode = "z2783613093803",
                    SupplierId = 3
                },
                new()
                {
                    Sku = "2222-1111-1111",
                    Barcode = "z2783613093804",
                    SupplierId = 3
                }
            };
           
            
            mockBarcodeMapper.Setup(_ => _.GetExistingProductLookups(
                It.IsAny<List<SupplierProductBarcode>>(), 
                It.IsAny<List<SupplierProductBarcode>>())).Returns(lookUps);
            
            mockBarcodeMapper.Setup(_ => _.GetNewProducts(
                It.IsAny<List<SupplierProductBarcode>>(), 
                It.IsAny<List<SupplierProductBarcode>>(), 
                It.IsAny<List<string>>())).Returns(_barcodeB);

        }

        [Fact]
        public void TransformCatalog_ShouldReturn_SuperCatalog()
        {
            throw new NotImplementedException();
        }
    }
}