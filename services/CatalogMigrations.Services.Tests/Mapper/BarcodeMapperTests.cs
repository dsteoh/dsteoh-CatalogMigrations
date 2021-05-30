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
                    Sku = "2222-1111-1111",
                    Barcode = "z2783613083800",
                    SupplierId = 1
                },
                new()
                {
                    Sku = "2222-1111-1111",
                    Barcode = "z2783613083801",
                    SupplierId = 2
                },
                new()
                {
                    Sku = "2222-1111-3333",
                    Barcode = "z2783613089999",
                    SupplierId = 2
                },
                new()
                {
                    Sku = "2222-2222-3333",
                    Barcode = "z278361308888",
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
            var matchedProducts = _barcodeMapper.GetExistingProductLookups(_barcodeA, _barcodeB).ToList();
            matchedProducts.Single().Should().Be("z2783613083800");
        }

        [Fact]
        public void GetNewProducts_ShouldReturn_NewProducts()
        {
            var result = new List<SupplierProductBarcode>()
            {
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
                },
            };
            
            var newProducts = _barcodeMapper.GetNewProducts(_barcodeA, _barcodeB, _productLookup);
            newProducts.Should().BeEquivalentTo(result);

        }

        [Fact]
        public void RemoveDuplicates_ShouldReturn_ProductsWithNoDuplicates()
        {
            var result = new List<SupplierProductBarcode>()
            {
                new()
                {
                    Sku = "2222-1111-1111",
                    Barcode = "z2783613083800",
                    SupplierId = 1
                },
                new()
                {
                    Sku = "2222-1111-3333",
                    Barcode = "z2783613089999",
                    SupplierId = 2
                },
                new()
                {
                    Sku = "2222-2222-3333",
                    Barcode = "z278361308888",
                    SupplierId = 2
                }
            };
            
            var noDupProducts = _barcodeMapper.RemoveDuplicatedProducts(_barcodeA).ToList();
            noDupProducts.Should().BeEquivalentTo(result);
        }
    }
}