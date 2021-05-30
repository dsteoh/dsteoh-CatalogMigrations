# Catalog Migrations

Catalog migrations is a console app used to merge two catalogs together. The business problem is stated in the following repo https://github.com/tosumitagrawal/codingskills

## Running the application

Project requires .NET 5.0 to run 
https://dotnet.microsoft.com/download

Using a command line tool navigate to the path
```
\CatalogMigrations\console\CatalogMigrations 
```
Run `dotnet run`

## The solution
![alt text](https://drive.google.com/uc?id=1EIg0OEIx1jz1414gqlHZ0yrxFuLtqRsO)

The problem suggested that company A inherited company B which means we only need to find possible new products in company B whilst keeping the products in company A.

We can also use the barcode field as a source of truth as stated by the problem statement.

* If any supplier barcode matches for one product of company A with Company B then we can consider that those products as the same.


1. First a table called ```barcodeLookup``` will be created, this table will contain barcodes that exist in both A and B.
2. Using ```barcodeLookup``` we can loop over barcodeB.csv to find which item is new/ not exist barcodeA.csv. Named ```newItemsList```. 
3. We then have to remove the duplicated products from both list (```barcodeA.csv``` and ```newItemsList```) where the same product can have differnt barcodes(row).
4. Finally we then construct the merged catalog using ```barcodeA.csv``` and ```newItemsList```.
