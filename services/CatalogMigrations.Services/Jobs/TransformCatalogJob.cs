using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Helpers.Csv.Mappers;

namespace CatalogMigrations.Services.Jobs
{
    public interface ITransformCatalogJob
    {
        
    }
    public class TransformCatalogJob : ITransformCatalogJob
    {
        public TransformCatalogJob()
        {
          
        }

        public void TransformCatalog(List<Catalog> catalogs)
        {
            
        }

        public IEnumerable<SupplierProductBarcode> GetMatchingBarcode(List<SupplierProductBarcode> supplierProductBarcodesA, List<SupplierProductBarcode>supplierProductBarcodesB)
        {
            var productList = new List<SupplierProductBarcode>();
            
            var resultSet = supplierProductBarcodesA
                .Select(_ => _.Barcode)
                .Intersect(supplierProductBarcodesB.Select(_ => _.Barcode)).ToList();

            foreach (var barcode in resultSet)
            {
                var product = supplierProductBarcodesA.SingleOrDefault(_ => _.Barcode == barcode);
                productList.Add(product);
            }
            return productList;
        }
    }
}