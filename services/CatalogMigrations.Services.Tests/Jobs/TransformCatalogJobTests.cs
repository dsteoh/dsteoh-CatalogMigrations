using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Jobs;
using FluentAssertions;
using Moq;
using Xunit;

namespace CatalogMigrations.Services.Tests.Jobs
{
    public class TransformCatalogJobTests
    {
        private TransformCatalogJob _transformCatalogJob;
        
        public TransformCatalogJobTests()
        {
            _transformCatalogJob = new TransformCatalogJob();
        }

        [Fact]
        public void TransformCatalog_ShouldReturn_SuperCatalog()
        {
            
        }
        
        
    }
}