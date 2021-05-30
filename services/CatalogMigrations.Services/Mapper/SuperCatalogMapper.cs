using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;

namespace CatalogMigrations.Services.Mapper
{
    public interface ISuperCatalogMapper
    {
        IEnumerable<SuperCatalog> GetSuperCatalogFormat(
            IEnumerable<SupplierProductBarcode> supplierProductBarcode,
            IEnumerable<Catalog> catalog,
            string company);
    }

    public class SuperCatalogMapper: ISuperCatalogMapper
    {
        public IEnumerable<SuperCatalog> GetSuperCatalogFormat(
            IEnumerable<SupplierProductBarcode> supplierProductBarcode,
            IEnumerable<Catalog> catalog,
            string company)
        {
            var superCatalog =
                from spb in supplierProductBarcode
                join cat in catalog
                on spb.Sku equals cat.Sku
                select new SuperCatalog()
                {
                    Sku = spb.Sku,
                    Description = cat.Description,
                    Source = company
                };
            return superCatalog;
        }
    }
}