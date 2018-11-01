using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampReader
{
    public class DB
    {
        public bool IsValid { get; private set; } = false;
        private string DBConnectionString { get; set; }
        private string myConnectionString { get; set; } = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
        public DB(string PathToFile)
        {
            if (File.Exists(PathToFile))
            {
                DBConnectionString = myConnectionString + PathToFile;
                IsValid = true;
            }
        }
        public DataTable Query(string MyQuery)
        {
            DataTable myResults = new DataTable();
            using (OleDbConnection mdbConnection = new OleDbConnection(DBConnectionString))
            {
                try
                {
                    OleDbCommand myCommand = new OleDbCommand(MyQuery, mdbConnection);
                    mdbConnection.Open();
                    using (OleDbDataAdapter myReadAdapter = new OleDbDataAdapter(myCommand))
                        myReadAdapter.Fill(myResults);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            myResults = TableTools.ClearNullColumns(myResults);
            return myResults;
        }
    }
}
