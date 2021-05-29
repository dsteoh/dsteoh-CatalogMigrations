namespace CatalogMigrations.DataModels.Models
{
    public class SupplierProductBarcode
    {
        public int SupplierId { get; set; }
        public string Sku { get; set; }
        public string Barcode { get; set; }
    }
}