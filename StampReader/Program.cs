using CommandLine;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace StampReader
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Helpers.SetVersioningInfo();
            ExecutionOptions options = new ExecutionOptions();
            //1. Get Executions options set from command prompt
            Parser.Default.ParseArguments(args, options);
            if (string.IsNullOrEmpty(options.Mode))
                Environment.Exit(-1);
            //2. Establish Stamp Database Configuration
            DB StampDB = new DB(Properties.Settings.Default.StampDB);
            if (!StampDB.IsValid)
            {
                Console.WriteLine("Couldn't located the Stamp Database");
                Environment.Exit(1);
            }
            //3.Perform Query based on inputs
            StampQry myStampQry = new StampQry(StampDB, options);
            if (options.Mode == "my" || options.Mode == "missingyear")
            {
                FilterResults filterResults = new FilterResults(myStampQry.MyTableResults);
                PrintResults(options, filterResults.FilteredDT);
                PrintSummary(filterResults.FilteredDT, options);
            }
            else if (options.Mode=="s")
            {
                Console.WriteLine($"No results for selected stamp:{options.StampsToFind}, fetching price details for comparison");
                options.Mode = "sm";
                myStampQry = new StampQry(StampDB, options);
                PrintResults(options, myStampQry.MyTableResults);
            }
            else
                PrintResults(options, myStampQry.MyTableResults);
            Console.WriteLine("\n");
            //Console.ReadKey();
        }
        private static void PrintSummary(DataTable results, ExecutionOptions options)
        {
            if (results.Rows.Count > 1)
            {
                decimal SumVF = results.AsEnumerable()
                    .Where(x => x["Mint-VF"] != DBNull.Value)
                    .Sum(x => x.Field<decimal>("Mint-VF"));
                Console.WriteLine($"{results.Rows.Count} stamps identified for year:{options.NumParam}");
                Console.WriteLine($"Sum Mint-VF:{SumVF}");
                Console.WriteLine($"Average Stamp Price:{Math.Round(SumVF / results.Rows.Count, 2)}");
            }
        }
        private static void PrintResults(ExecutionOptions options, DataTable myStampQry)
        {
            if (myStampQry?.Rows?.Count > 0)
            {
                if (myStampQry.Rows.Count == 1)
                    myStampQry.PrintList();
                else
                {
                    myStampQry.Print();
                    if (options.JSonOutput)
                    {
                        string json = JsonConvert.SerializeObject(myStampQry, Formatting.Indented);
                        File.WriteAllText(Properties.Settings.Default.JSonFileOutput, json);
                    }
                }
            }
            else
                Console.WriteLine($"No results for selected Stamp(s):{options.StampsToFind}");
        }
    }
}
