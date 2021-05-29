using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Jobs;
using CatalogMigrations.Services.Mapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace CatalogMigrations.Services.Tests.Mapper
{
    public class BarcodeMapperTests
    {
        private BarcodeMapper _barcodeMapper;
        private List<SupplierProductBarcode> _barcodeA;
        private List<SupplierProductBarcode> _barcodeB;
        private List<SupplierProductBarcode> _productLookup;
        public BarcodeMapperTests()
        {
            _barcodeMapper = new BarcodeMapper();
            
            _barcodeA = new List<SupplierProductBarcode>()
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
            
            _barcodeB = new List<SupplierProductBarcode>()
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
                    Barcode = "z2783613093801",
                    SupplierId = 1
                }
            };
        }
        
        [Fact]
        public void GetMatchingBarcode_ShouldReturn_MatchingBarcodes()
        {
            var matchedProducts = _barcodeMapper.GetMatchingProducts(_barcodeA, _barcodeB);
            matchedProducts.Single().Barcode.Should().Be("z2783613083800");
        }

        [Fact]
        public void GetMatchingSku_ShouldReturn_UniqueProducts()
        {
            var uniqueProducts = _barcodeMapper.GetUniqueProductFromSku(_productLookup,_barcodeA, _barcodeB);
            uniqueProducts.Single().Barcode.Should().Be("z2783613083800");
        }
    }
}