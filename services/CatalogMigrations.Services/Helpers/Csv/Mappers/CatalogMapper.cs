using CatalogMigrations.DataModels.Models;
using CsvHelper.Configuration;

namespace CatalogMigrations.Services.Helpers.Csv.Mappers
{
    public sealed class CatalogMapper : ClassMap<Catalog>
    {
        public CatalogMapper()
        {
            Map(_ => _.Sku).Name("SKU");
            Map(_ => _.Description).Name("Description");
        }
    }
}