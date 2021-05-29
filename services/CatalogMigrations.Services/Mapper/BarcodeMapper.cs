using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;

namespace CatalogMigrations.Services.Mapper
{
    public interface IBarcodeMapper
    {
        List<string> GetExistingProductLookups(
            List<SupplierProductBarcode> supplierProductBarcodesA,
            List<SupplierProductBarcode> supplierProductBarcodesB);

        IEnumerable<SupplierProductBarcode> GetNewProductsFromSku(
            List<SupplierProductBarcode> supplierProductBarcodesA,
            List<SupplierProductBarcode> supplierProductBarcodesB,
            List<string> productLookup);
    }
    
    public class BarcodeMapper : IBarcodeMapper
    {
        public List<string> GetExistingProductLookups(List<SupplierProductBarcode> supplierProductBarcodesA, 
            List<SupplierProductBarcode>supplierProductBarcodesB)
        {
            var matchingBarcodes = supplierProductBarcodesA
                .Select(_ => _.Barcode)
                .Intersect(supplierProductBarcodesB.Select(_ => _.Barcode)).ToList();

            return matchingBarcodes;
        } 
        
        public IEnumerable<SupplierProductBarcode> GetNewProductsFromSku(List<SupplierProductBarcode> supplierProductBarcodesA, List<SupplierProductBarcode> supplierProductBarcodesB,
            List<string> productLookup)
        {
            var newProductList = new List<SupplierProductBarcode>();
            var combinedProducts = supplierProductBarcodesA.Concat(supplierProductBarcodesB).ToList();
            
            foreach (var product in combinedProducts)
            {
                if (!productLookup.Contains(product.Barcode))
                {
                    newProductList.Add(product);
                }
            }
            return newProductList;
        }
    }
}