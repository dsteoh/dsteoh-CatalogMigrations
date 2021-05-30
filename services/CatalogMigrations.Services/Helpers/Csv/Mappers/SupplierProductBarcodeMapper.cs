using CatalogMigrations.DataModels.Models;
using CsvHelper.Configuration;

namespace CatalogMigrations.Services.Helpers.Csv.Mappers
{
    public sealed class SupplierProductBarcodeMapper : ClassMap<SupplierProductBarcode>
    {
        public SupplierProductBarcodeMapper()
        {
            Map(_ => _.SupplierId).Name("SupplierID");
            Map(_ => _.Sku).Name("SKU");
            Map(_ => _.Barcode).Name("Barcode");
        }
    }
}