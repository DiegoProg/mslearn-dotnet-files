using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace files_module
{
    class Program
    {
            static void Main(string[] args)
            {
            var files = FindFiles("stores");
            var currentDirectory = Directory.GetCurrentDirectory();
           // var salesFiles = FindFiles("stores");
            var storesDirectory = Path.Combine(currentDirectory, "stores");
            var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
            Directory.CreateDirectory(salesTotalDir);
            
            var salesFiles = FindFiles(storesDirectory);

            var salesTotal = CalculateSalesTotal(salesFiles);

            File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

                       
            foreach (var file in files)
                {
                Console.WriteLine(file);
                string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";

                FileInfo info = new FileInfo(fileName);
                Console.WriteLine($"Full Name: {info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}"); // And many more
                }
             }

            static IEnumerable<string> FindFiles(string folderName)
                {
                     List<string> salesFiles = new List<string>();
    
                    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);
    
                    foreach (var file in foundFiles)
                    {
                        if (file.EndsWith("sales.json"))
                        {
                        salesFiles.Add(file);
                        }
                    }
    
                    return salesFiles;


                }
            static double CalculateSalesTotal(IEnumerable<string> salesFiles)
            {
            double salesTotal = 0;

            // Loop over each file path in salesFiles
            foreach (var file in salesFiles)
             {      
                // Read the contents of the file
                string salesJson = File.ReadAllText(file);

                // Parse the contents as JSON
                SalesData data = JsonConvert.DeserializeObject<SalesData>(salesJson);

                // Add the amount found in the Total field to the salesTotal variable
                salesTotal += data.Total;
            }

            return salesTotal;
            }

            class SalesData
            {
            public double Total{get;set;}
            }
    }

    
}