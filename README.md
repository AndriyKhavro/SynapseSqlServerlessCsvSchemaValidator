# SynapseSqlServerlessCsvSchemaValidator

Console app to verify schema (for example, WITH clause in OPENROWSET query) for CSV files. Currently verifies only integer types (int, bigint, smallint, and tinyint) and uniqueidentifier. I tested it only on one sample input, so it may have bugs, however, should give a clue what could cause the data mismatch.

Could be helpful to debug errors in Synapse SQL Serverless like this:

Msg 15813, Level 16, State 1, Line 5
Error handling external file: 'Max errors count reached.'. File/External table name: 'https://akhavrotest.blob.core.windows.net/testdata/2016-07 - Copy.csv'.

Here is how to run this:

```
dotnet run "Full path to folder with CSV files"  "Comma-separated schema"
```

For example:
```
dotnet run "C:\Users\username\Downloads\My CSV Files"  "column1 uniqueidentifier, column2 tinyInt", column3 bigInt"
```
