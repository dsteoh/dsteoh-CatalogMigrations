using CatalogMigrations.DataModels.Models;
using CsvHelper.Configuration;

namespace CatalogMigrations.Services.Helpers.Csv.Mappers
{
    public sealed class SupplierMapper : ClassMap<Supplier>
    {
        public SupplierMapper()
        {
            Map(_ => _.Id).Name("ID");
            Map(_ => _.Name).Name("Name");
        }
    }
}