using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using AVLCarSystemApp.DataContexts;
using AVLCarSystemApp.ModelsDTO;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    class Program
    {
        // shound also be loaded from some config file.
        // mappings of column names whic are different from database and csv file 
        private static Dictionary<string, Tuple<string, Dictionary<string, string>>> FileColumnMappings = new Dictionary<string, Tuple<string, Dictionary<string, string>>>()
        {
            // file name and table name with column name mappings
            { "City.csv",
                new Tuple<string, Dictionary<string, string>> (
                    "dbo.Cities", new Dictionary<string, string>()
                    {
                        // source column (file column name) to DB table column name
                        {"IdCity", "Id"}
                    })
            },
            { "Country.csv",
                new Tuple<string, Dictionary<string, string>> (

                        "dbo.Countries", new Dictionary<string, string>() {{"IdCountry", "Id"}}
                )
            },
            {"Salon.csv",
                new Tuple<string, Dictionary<string, string>>(
                        "dbo.Salons", new Dictionary<string, string>()
                        {
                            {"IdSalon", "Id"},
                            {"IdCity", "CityId"},
                            {"IdCountry", "CountryId"}
                        }
               )
            },
            {"Manufacturer.csv",
                new Tuple<string, Dictionary<string, string>>(

                    "dbo.Manufacturers", new Dictionary<string, string>()
                    {
                        {"IdManufacturer", "Id"},
                        {"IdCity", "CityId"},
                        {"IdCountry", "CountryId"}
                    }
               )
            },
            {"EngineType.csv",
                new Tuple<string, Dictionary<string, string>>(
                        "dbo.EngineTypes", new Dictionary<string, string>() {{"IdEngineType", "Id"}}
                )
            },
            {"Engine.csv",
                new Tuple<string, Dictionary<string, string>>(
                    "dbo.Engines", new Dictionary<string, string>()
                    {
                        {"IdEngine", "Id"},
                        {"IdEngineType", "EngineTypeId"}
                    }
                )
            },
            {"Equipment.csv",
                new Tuple<string, Dictionary<string, string>>(
                        "dbo.Equipments", new Dictionary<string, string>()
                        {
                            {"IdEquipment", "Id"},
                            {"AutomaticTransition", "AutomaticTransmission"}
                        }
                )
            },
            { "Model.csv",
                new Tuple<string, Dictionary<string, string>>(
                    "dbo.Models", new Dictionary<string, string>()
                    {
                        {"IdModel", "Id"},
                        {"IdManufacturer", "ManufacturerId"},
                        {"IdEquipment", "EquipmentId"},
                        {"IdEngine", "EngineId"}
                    }
               )
            },
            {"Inventory.csv",
                new Tuple<string, Dictionary<string, string>>(
                        "dbo.Inventories", new Dictionary<string, string>()
                        {
                            {"IdInventory", "Id"},
                            {"IdModel", "ModelId"},
                            {"IdSalon", "SalonId"},
                            {"NumberOfUnits", "NumberOfUnitsOnStock"}
                        }
                )
            }
        };


        static void Main(string[] args)
        {

            Console.Write("Enter the path of dataset folder> ");
            var pathString = Console.ReadLine();
            if (Directory.Exists(pathString))
            {
                Console.Write("Enter connection string> ");
                var connectionString = "Data Source=HRZAGWN111960\\SQLEXPRESS;Initial Catalog=CarSystemDB;Integrated Security=False;User ID=adminavl;Password=adminavl0000;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//Console.ReadLine();

                try
                {
                    ProcessDirectory(pathString, connectionString);
                    Console.WriteLine("Done!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nError!");
                    Console.WriteLine(e);
                }
            }
            else
            {
                Console.WriteLine($"Directory {pathString} does not exist!");
            }

            Console.ReadLine();
        }

        private static void ProcessDirectory(string pathString, string connectionString)
        {
            var files = Directory.EnumerateFiles(pathString, "*.csv");
            Console.WriteLine($"Found {files.Count()} csv files in directory.");

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (!FileColumnMappings.ContainsKey(fileName))
                {
                    Console.WriteLine($"File {file} is not needed to fill database - skipping");
                    continue;
                }

                Console.WriteLine($"Reading file {file}");
                Console.WriteLine("Press enter to load or any key to skip this file.");
                if (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    continue;
                }

                var tableSettings = FileColumnMappings[fileName];
                var dataTable = GetDataTableFromCSVFile(file, tableSettings);

                Console.Write("Loaded! Writing data to database ...");

                InsertDataIntoSQLServerUsingSQLBulkCopy(dataTable, connectionString);

                Console.WriteLine("\b\b\b--- DONE!");
            }
        }

        private static DataTable GetDataTableFromCSVFile(string csvFilePath, Tuple<string, Dictionary<string, string>> tableSettings)
        {
            DataTable csvData = new DataTable(tableSettings.Item1);
            

            using (TextReader reader = File.OpenText(csvFilePath))
            {
                var csv = new CsvReader(reader);
                var headerReaded = csv.Read() && csv.ReadHeader();
                if (!headerReaded)
                {
                    throw new FormatException("Header can't be readed");
                }

                var columns = csv.Context.HeaderRecord;
                foreach (var column in columns)
                {
                    var collName = column;
                    if (tableSettings.Item2.ContainsKey(column))
                    {
                        collName = tableSettings.Item2[column];
                    }
                    csvData.Columns.Add(new DataColumn(collName));
                    
                }

                while (csv.Read())
                {
                    csvData.Rows.Add(csv.Context.Record);
                }
            }

            return csvData;
        }
        

        private static void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData, string connectionString)
        {
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = csvFileData.TableName;
                    foreach (DataColumn dataColumn in csvFileData.Columns)
                    {
                        s.ColumnMappings.Add(dataColumn.ColumnName, dataColumn.ColumnName);
                    }

                    s.WriteToServer(csvFileData);
                }

                dbConnection.Close();
            }
        }
    }
}


