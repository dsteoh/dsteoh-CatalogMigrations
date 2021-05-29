using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;

namespace CatalogMigrations.Services.Mapper
{
    public interface ISuperCatalogMapper
    {
        List<SuperCatalog> GetSuperCatalogFormat(
            List<SupplierProductBarcode> supplierProductBarcode, 
            List<Catalog> catalog,
            string company);
    }
    
    public class SuperCatalogMapper
    {
        public List<SuperCatalog> GetSuperCatalogFormat(
            List<SupplierProductBarcode> supplierProductBarcode, 
            List<Catalog> catalog,
            string company)
        {
            var query =
                from spb in supplierProductBarcode
                join cat in catalog
                on spb.Sku equals cat.Sku
                select new SuperCatalog()
                {
                    Sku = spb.Sku,
                    Description = cat.Description,
                    Source = company
                };
            return query.ToList();
        }
    }
}