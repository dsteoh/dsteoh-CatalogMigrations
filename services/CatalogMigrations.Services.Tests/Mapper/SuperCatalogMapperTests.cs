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
        private readonly List<SupplierProductBarcode> _barcode;
        private readonly List<Catalog> _catalog;
        private readonly SuperCatalogMapper _superCatalogMapper;

        public SuperCatalogMapperTests()
        {
            _superCatalogMapper = new SuperCatalogMapper();

            _barcode = new List<SupplierProductBarcode>()
            {
                new()
                {
                    Sku = "2222-1111-1111",
                    Barcode = "z2783613083800",
                    SupplierId = 1
                },
                new()
                {
                    Sku = "2222-1111-1112",
                    Barcode = "z2783613083801",
                    SupplierId = 2
                }
            };

            _catalog = new List<Catalog>()
            {
                new()
                {
                    Sku = "2222-1111-1111",
                    Description = "Frostmourne"
                },
                new()
                {
                    Sku = "2222-1111-1112",
                    Description = "Blue Journeyman Backpack"
                }
            };
        }

        [Fact]
        public void GetSuperCatalogFormat()
        {
            var result = new List<SuperCatalog>()
            {
                new()
                {
                    Sku = "2222-1111-1111",
                    Description = "Frostmourne",
                    Source = "A"
                },
                new()
                {
                    Sku = "2222-1111-1112",
                    Description = "Blue Journeyman Backpack",
                    Source = "A"
                }
            };

            var superCatalog = _superCatalogMapper.GetSuperCatalogFormat(
                _barcode,
                _catalog,
                "A").ToList();

            superCatalog.Should().BeEquivalentTo(result);
        }
    }
}