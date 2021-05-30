using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;

namespace CatalogMigrations.Services.Mapper
{
    public interface IBarcodeMapper
    {
        IEnumerable<string> GetExistingProductLookups(
            List<SupplierProductBarcode> supplierProductBarcodesA,
            List<SupplierProductBarcode> supplierProductBarcodesB);

        IEnumerable<SupplierProductBarcode> GetNewProducts(
            List<SupplierProductBarcode> supplierProductBarcodesA,
            List<SupplierProductBarcode> supplierProductBarcodesB,
            List<string> productLookup);
    }
    
    public class BarcodeMapper : IBarcodeMapper
    {
        public IEnumerable<string> GetExistingProductLookups(List<SupplierProductBarcode> supplierProductBarcodesA, 
            List<SupplierProductBarcode>supplierProductBarcodesB)
        {
            var matchingBarcodes = supplierProductBarcodesA
                .Select(_ => _.Barcode)
                .Intersect(supplierProductBarcodesB.Select(_ => _.Barcode));

            return matchingBarcodes;
        } 
        
        public IEnumerable<SupplierProductBarcode> GetNewProducts(List<SupplierProductBarcode> supplierProductBarcodesA, List<SupplierProductBarcode> supplierProductBarcodesB,
            List<string> matchingBarcodes)
        {
            var newProductList = new List<SupplierProductBarcode>();
            
            var productsList = supplierProductBarcodesB.ToList();
            
            foreach (var product in productsList)
            {
                if (!matchingBarcodes.Contains(product.Barcode))
                {
                    newProductList.Add(product);
                }
            }
            return newProductList;
        }
    }
}