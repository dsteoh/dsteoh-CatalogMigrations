using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Helpers.Csv.Mappers;
using CatalogMigrations.Services.Mapper;

namespace CatalogMigrations.Services.Jobs
{
    public interface ITransformCatalogJob
    {
        IEnumerable<SuperCatalog> TransformCatalog(
            IEnumerable<SupplierProductBarcode> supplierProductBarcodesA,
            IEnumerable<Catalog> catalogA,
            IEnumerable<SupplierProductBarcode> supplierProductBarcodesB,
            IEnumerable<Catalog> catalogB);
    }

    public class TransformCatalogJob : ITransformCatalogJob
    {
        private IBarcodeMapper _barcodeMapper;
        private ISuperCatalogMapper _superCatalogMapper;

        public TransformCatalogJob(IBarcodeMapper barcodeMapper, ISuperCatalogMapper superCatalogMapper)
        {
            _barcodeMapper = barcodeMapper;
            _superCatalogMapper = superCatalogMapper;
        }

        public IEnumerable<SuperCatalog> TransformCatalog(
            IEnumerable<SupplierProductBarcode> supplierProductBarcodesA,
            IEnumerable<Catalog> catalogA,
            IEnumerable<SupplierProductBarcode>supplierProductBarcodesB,
            IEnumerable<Catalog> catalogB)
        {
            // Get lookup of matching product
            var supplierProductBarcodes = supplierProductBarcodesA.ToList();
            var productBarcodesB = supplierProductBarcodesB.ToList();

            var matchingBarcodeLookups = _barcodeMapper
                .GetExistingProductLookups(supplierProductBarcodes, productBarcodesB);

            // New products in company B
            var newCompanyBProducts = _barcodeMapper.GetNewProducts(supplierProductBarcodes,
                productBarcodesB, matchingBarcodeLookups).ToList();

            var distinctA = _barcodeMapper.RemoveDuplicatedProducts(supplierProductBarcodes);
            var distinctB = _barcodeMapper.RemoveDuplicatedProducts(newCompanyBProducts);

            var superCatalogA = _superCatalogMapper.GetSuperCatalogFormat(distinctA, catalogA, "A");
            var superCatalogB = _superCatalogMapper.GetSuperCatalogFormat(distinctB, catalogB, "B");

            return superCatalogA.Concat(superCatalogB);
        }
    }
}