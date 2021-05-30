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
    public interface ICsvParser
    {
        List<Catalog> ParseToCatalogsToList(string path);
        List<Supplier> ParseToSupplierToList(string path);
        List<SupplierProductBarcode> ParseToSupplierProductBarcodeToList(string path);
        void WriteSuperCatalogToFile(string path, string fileName, List<SuperCatalog> superCatalogs);
    }

    public class CsvParser : ICsvParser
    {
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
            try
            {
                csvReader.Context.RegisterClassMap<SupplierProductBarcodeMapper>();
                return csvReader.GetRecords<SupplierProductBarcode>().ToList();
            }
            catch (ValidationException ex)
            {
                //ex.Data.Values has more info...
            }

            return null;
        }

        public void WriteSuperCatalogToFile(string path, string fileName, List<SuperCatalog> superCatalogs)
        {
            var writeFilePath = Path.Combine(path, fileName);
            using var streamWriter = new StreamWriter(writeFilePath);
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(superCatalogs);
        }
    }
}