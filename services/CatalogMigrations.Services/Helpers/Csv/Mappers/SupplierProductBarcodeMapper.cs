using CatalogMigrations.DataModels.Models;
using CsvHelper.Configuration;

namespace CatalogMigrations.Services.Helpers.Csv.Mappers
{
    public sealed class SupplierProductBarcodeMapper : ClassMap<SupplierProductBarcode>
    {
        public SupplierProductBarcodeMapper()
        {
            Map(_ => _.SupplierId).Name("SupplierID").Index(0);
            Map(_ => _.Sku).Name("SKU").Index(1);
            Map(_ => _.Barcode).Name("Barcode").Index(2);
        }
    }
}