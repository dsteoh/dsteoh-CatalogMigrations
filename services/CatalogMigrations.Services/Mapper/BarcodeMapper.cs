using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;

namespace CatalogMigrations.Services.Mapper
{
    public interface IBarcodeMapper
    {
        IEnumerable<string> GetExistingProductLookups(
            IEnumerable<SupplierProductBarcode> supplierProductBarcodesA,
            IEnumerable<SupplierProductBarcode> supplierProductBarcodesB);

        IEnumerable<SupplierProductBarcode> GetNewProducts(
            IEnumerable<SupplierProductBarcode> supplierProductBarcodesB,
            IEnumerable<string> productLookup);

        IEnumerable<SupplierProductBarcode> RemoveDuplicatedProducts(
            IEnumerable<SupplierProductBarcode> supplierProductBarcodes);
    }

    public class BarcodeMapper : IBarcodeMapper
    {
        public IEnumerable<string> GetExistingProductLookups(
            IEnumerable<SupplierProductBarcode> supplierProductBarcodesA,
            IEnumerable<SupplierProductBarcode>supplierProductBarcodesB)
        {
            var matchingBarcodes = supplierProductBarcodesA
                .Select(_ => _.Barcode)
                .Intersect(supplierProductBarcodesB.Select(_ => _.Barcode));

            return matchingBarcodes;
        }

        public IEnumerable<SupplierProductBarcode> GetNewProducts(
            IEnumerable<SupplierProductBarcode> supplierProductBarcodesB,
            IEnumerable<string> matchingBarcodes)
        {
            var newProductList = new List<SupplierProductBarcode>();

            foreach (var product in supplierProductBarcodesB)
            {
                if (!matchingBarcodes.Contains(product.Barcode))
                {
                    newProductList.Add(product);
                }
            }
            return newProductList;
        }

        public IEnumerable<SupplierProductBarcode> RemoveDuplicatedProducts(
            IEnumerable<SupplierProductBarcode> supplierProductBarcodes)
        {
            var distinctProducts = supplierProductBarcodes.GroupBy(_ => _.Sku)
                .Select(p => p.First());

            return distinctProducts;
        }
    }
}