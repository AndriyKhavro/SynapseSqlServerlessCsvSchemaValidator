using System;
using System.IO;
using System.Linq;

namespace SynapseSqlServerlessCsvSchemaValidator
{
    class Program
    {
        private const char RowTerminator = '\n';
        private const char FieldTerminator = ',';

        static void Main(string[] args)
        {
            string folder = args[0];
            string schema = args[1];

            var columns = schema.Split(',');

            var functions = columns.Select(CanBeParsed).ToArray();

            var files = Directory.EnumerateFiles(folder);

            foreach (var file in files)
            {
                string text = File.ReadAllText(file);

                var firstLine = text.Split(RowTerminator)[0];

                var rowColumns = firstLine.Split(FieldTerminator);

                Console.WriteLine($"Processing file {file}...");

                for (int i = 0; i < Math.Min(columns.Length, rowColumns.Length); i++)
                {
                    if (!functions[i](rowColumns[i]))
                    {
                        Console.WriteLine($"Column is incorrect: '{columns[i]}'. Value: {rowColumns[i]}. Index: {i+1}");
                    }
                }
            }

            Func<string, bool> CanBeParsed(string column)
            {
                return (data) =>
                {
                    string trimmedData = data.Trim('"', ' ');

                    if (string.IsNullOrEmpty(trimmedData))
                    {
                        return true;
                    }

                    if (column.Contains("uniqueidentifier", StringComparison.OrdinalIgnoreCase))
                    {
                        return Guid.TryParse(trimmedData, out _);
                    }

                    if (column.Contains("int", StringComparison.OrdinalIgnoreCase))
                    {
                        return long.TryParse(trimmedData, out _);
                    }
                    return true;
                };
            }
            
        }
    }
}
