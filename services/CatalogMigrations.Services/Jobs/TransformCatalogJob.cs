using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Helpers.Csv.Mappers;

namespace CatalogMigrations.Services.Jobs
{
    public interface ITransformCatalogJob
    {
        
    }
    public class TransformCatalogJob : ITransformCatalogJob
    {
        public TransformCatalogJob()
        {
          
        }

        public void TransformCatalog(List<Catalog> catalogs)
        {
            
        }
    }
}