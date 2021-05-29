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
        
        public void TransformCatalog(List<SupplierProductBarcode> supplierProductBarcodesA, 
            List<Catalog> catalogA, List<Catalog> catalogB,
            List<SupplierProductBarcode>supplierProductBarcodesB)
        {
            var productList = new List<SupplierProductBarcode>();
            var matchingBarcodeLookups = _barcodeMapper
                .GetExistingProductLookups(supplierProductBarcodesA, supplierProductBarcodesB);
            
            var companyBProducts = _barcodeMapper.GetNewProducts(supplierProductBarcodesA,
                supplierProductBarcodesB, matchingBarcodeLookups).ToList();
            
            foreach (var product in supplierProductBarcodesA)
            {
                if (!matchingBarcodeLookups.Contains(product.Barcode))
                {
                    productList.Add(product);
                }
            }

            var superCatalogA = _superCatalogMapper.GetSuperCatalogFormat(productList, catalogA, "A");
            var superCatalogB = _superCatalogMapper.GetSuperCatalogFormat(companyBProducts, catalogB, "B");

            var joinedCatalog = superCatalogA.Concat(superCatalogB);
        }
    }
}