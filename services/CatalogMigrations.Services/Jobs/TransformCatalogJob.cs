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
        List<SuperCatalog> TransformCatalog(
            List<SupplierProductBarcode> supplierProductBarcodesA,
            List<Catalog> catalogA,
            List<SupplierProductBarcode> supplierProductBarcodesB,
            List<Catalog> catalogB);
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
        
        public List<SuperCatalog> TransformCatalog(
            List<SupplierProductBarcode> supplierProductBarcodesA, 
            List<Catalog> catalogA,
            List<SupplierProductBarcode>supplierProductBarcodesB, 
            List<Catalog> catalogB)
        {
            var productList = new List<SupplierProductBarcode>();
            
            // Get lookup of matching product
            var matchingBarcodeLookups = _barcodeMapper
                .GetExistingProductLookups(supplierProductBarcodesA, supplierProductBarcodesB);
            
            // New products in company B
            var newCompanyBProducts = _barcodeMapper.GetNewProducts(supplierProductBarcodesA,
                supplierProductBarcodesB, matchingBarcodeLookups).ToList();
            
            var superCatalogA = _superCatalogMapper.GetSuperCatalogFormat(productList, catalogA, "A");
            var superCatalogB = _superCatalogMapper.GetSuperCatalogFormat(newCompanyBProducts, catalogB, "B");

            throw new NotImplementedException();
        }
    }
}