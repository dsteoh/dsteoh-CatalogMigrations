using System;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CatalogMigrations.DataModels.Models;
using CatalogMigrations.Services.Helpers.Csv.Mappers;

namespace CatalogMigrations.Services.Helpers.Csv
{
    public class CsvParser : ICsvParser
    {
        public CsvParser()
        {
            
        }
        
        public List<Catalog> ParseToCatalogsToList(string path)
        {
            using var streamReader = new StreamReader(path);
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<CatalogMapper>();
            return csvReader.GetRecords<Catalog>().ToList();
        }
        
        public List<Supplier> ParseToSupplierToList(string path)
        {
            using var streamReader = new StreamReader(path);
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<SupplierMapper>();
            return csvReader.GetRecords<Supplier>().ToList();
        }
        
        public List<SupplierProductBarcode> ParseToSupplierProductBarcodeToList(string path)
        {
            using var streamReader = new StreamReader(path);
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<SupplierProductBarcodeMapper>();
            return csvReader.GetRecords<SupplierProductBarcode>().ToList();
        }
    }
}