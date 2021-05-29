using System.Collections.Generic;
using CatalogMigrations.DataModels.Models;

namespace CatalogMigrations.Services.Helpers.Csv
{
    public interface ICsvParser
    {
        List<Catalog> ParseToCatalogsToList(string path);
        List<Supplier> ParseToSupplierToList(string path);
        List<SupplierProductBarcode> ParseToSupplierProductBarcodeToList(string path);
    }
}