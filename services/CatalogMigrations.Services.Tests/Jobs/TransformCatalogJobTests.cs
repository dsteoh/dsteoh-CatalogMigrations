using System.Collections.Generic;
using System.Linq;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Jobs;
using CatalogMigrations.Services.Mapper;
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
            Mock<IBarcodeMapper> mockBarcodeMapper = new Mock<IBarcodeMapper>();
            _transformCatalogJob = new TransformCatalogJob(mockBarcodeMapper.Object);
        }

        [Fact]
        public void TransformCatalog_ShouldReturn_SuperCatalog()
        {
            
        }
    }
}