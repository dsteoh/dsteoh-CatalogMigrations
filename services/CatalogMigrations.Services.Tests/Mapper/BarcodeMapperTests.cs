using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Jobs;
using FluentAssertions;
using Moq;
using Xunit;

namespace CatalogMigrations.Services.Tests.Mapper
{
    public class BarcodeMapperTests
    {
        private TransformCatalogJob _barcodeMapper;

        public BarcodeMapperTests()
        {
            _barcodeMapper = new BarcodeMapper();
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
                },
                new()
                {
                    Sku = "2222-1111-1111",
                    Barcode = "z2783613083800",
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
                },
                new()
                {
                    Sku = "2222-1111-1111",
                    Barcode = "z2783613083801",
                    SupplierId = 1
                },
                new()
                {
                    Sku = "2222-1111-1111",
                    Barcode = "z2783613093800",
                    SupplierId = 1
                }
            };

            var matchedProducts = _barcodeMapper.GetMatchingBarcode(barcodeA, barcodeB);
            matchedProducts.Single().Barcode.Should().Be("z2783613083800");
        }
    }
}