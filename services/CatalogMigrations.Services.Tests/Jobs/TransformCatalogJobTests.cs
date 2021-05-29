using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Jobs;
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
            _transformCatalogJob = new TransformCatalogJob();
        }

        [Fact]
        public void TransformCatalog_ShouldReturn_SuperCatalog()
        {
            
        }
        
        [Fact]
        public void GetMatchingBarcode_ShouldReturn_MatchingBarcodes()
        {
            var barcodeA = new List<SupplierProductBarcode>()
            {
                new()
                {
                    Sku = "1111-1111-1111",
                    Barcode = "z2783613083817",
                    SupplierId = 1
                }
            };
            
            var barcodeB = new List<SupplierProductBarcode>()
            {
                new()
                {
                    Sku = "2222-1111-1111",
                    Barcode = "z2783613083818",
                    SupplierId = 2
                },
                new()
                {
                    Sku = "2222-1111-1111",
                    Barcode = "z2783613083800",
                    SupplierId = 1
                }
            };

            var matchedProducts = _transformCatalogJob.GetMatchingBarcode(barcodeA, barcodeB);
            matchedProducts.Single().Barcode.Should().Equals("z2783613083817");
         
        }
    }
}