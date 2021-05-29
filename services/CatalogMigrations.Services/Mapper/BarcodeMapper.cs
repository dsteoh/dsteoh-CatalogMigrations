using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;

namespace CatalogMigrations.Services.Mapper
{
    public interface IBarcodeMapper
    {
        IEnumerable<SupplierProductBarcode> GetMatchingProducts(
            List<SupplierProductBarcode> supplierProductBarcodesA,
            List<SupplierProductBarcode> supplierProductBarcodesB);
        IEnumerable<SupplierProductBarcode> GetUniqueProductFromSku(
            List<SupplierProductBarcode> supplierProductBarcodesA,
            List<SupplierProductBarcode> supplierProductBarcodesB, 
            IEnumerable<SupplierProductBarcode> productLookup);

    }
    
    public class BarcodeMapper : IBarcodeMapper
    {
     
        public IEnumerable<SupplierProductBarcode> GetMatchingProducts(List<SupplierProductBarcode> supplierProductBarcodesA, 
            List<SupplierProductBarcode>supplierProductBarcodesB)
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

        public IEnumerable<SupplierProductBarcode> GetUniqueProductFromSku(List<SupplierProductBarcode> supplierProductBarcodesA, List<SupplierProductBarcode> supplierProductBarcodesB,
            IEnumerable<SupplierProductBarcode> productLookup)
        {
            
        }
    }
}