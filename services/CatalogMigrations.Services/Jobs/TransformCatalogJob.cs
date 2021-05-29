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
        
        public TransformCatalogJob(IBarcodeMapper barcodeMapper)
        {
            _barcodeMapper = barcodeMapper;
        }
        
        public void TransformCatalog(List<SupplierProductBarcode> supplierProductBarcodesA, 
            List<SupplierProductBarcode>supplierProductBarcodesB)
        {
            var matchingProducts = _barcodeMapper.GetMatchingProducts(supplierProductBarcodesA, supplierProductBarcodesB).ToList();
        }
    }
}