using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Mapper;
using FluentAssertions;
using Xunit;

namespace CatalogMigrations.Services.Tests.Mapper
{
    public class BarcodeMapperTests
    {
        private BarcodeMapper _barcodeMapper;
        private List<SupplierProductBarcode> _barcodeA;
        private List<SupplierProductBarcode> _barcodeB;
        private List<string> _productLookup;
        public BarcodeMapperTests()
        {
            _barcodeMapper = new BarcodeMapper();

            _barcodeA = new List<SupplierProductBarcode>()
            {
                new()
                {
                    Sku = "1111-1111-1111",
                    Barcode = "z2783613083800",
                    SupplierId = 1
                },
                new()
                {
                    Sku = "2222-1111-1111",
                    Barcode = "z2783613083801",
                    SupplierId = 2
                }
            };

            _barcodeB = new List<SupplierProductBarcode>()
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

            _productLookup = new List<string>()
            {
                "z2783613083800",
            };
        }
        
        [Fact]
        public void GetExistingProductLookups_ShouldReturn_Lookups()
        {
            var matchedProducts = _barcodeMapper.GetExistingProductLookups(_barcodeA, _barcodeB);
            matchedProducts.Single().Should().Be("z2783613083800");
        }

        [Fact]
        public void GetNewProducts_ShouldReturn_UniqueProducts()
        {
            var result = new List<SupplierProductBarcode>()
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
            };
            
            var uniqueProducts = _barcodeMapper.GetNewProducts(_barcodeA, _barcodeB, _productLookup);
            uniqueProducts.Should().Equals(result);

        }
    }
}