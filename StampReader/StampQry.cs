using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace StampReader
{
    public class StampQry
    {
        //TBD
        //string qryStampAlbums = "SELECT * FROM StampAlbums";
        public string MyQuery { get; private set; } = string.Empty;
        public DataTable MyTableResults { get; set; } = new DataTable("EmptyTable");
        private bool resultsFinalized = false;
        public StampQry(DB StampDB, ExecutionOptions appOptions)
        {
            switch (appOptions.Mode.ToLower())
            {
                case "single":
                case "s":
                    MyQuery = "select myC.ScottNumber, Year(v.DateIssued) AS YearIssued," +
                "v.DescriptiveName, v.VarietyDenomination,v.Perforation, myC.StampType, myC.Condition, " +
                "c.CountryName, sa.AlbumDescription, myC.AlbumPage " +
                "FROM (((mycollection myC " +
                "INNER JOIN Varieties v ON myC.VarietyID=v.VarietyID) " +
                "INNER JOIN Countries c on v.CountryID=c.CountryID) " +
                "INNER JOIN StampAlbums sa on myC.StampAlbumID=sa.AlbumID)" +
                $"WHERE myC.ScottNumber='{appOptions.StampsToFind}'";
                    break;
                case "sm":
                    MyQuery = "SELECT ScottNum, Year(v.DateIssued) AS YearIssued,DescriptiveName,VarietyDenomination,Perforation," +
                        "sv.[Mint-VF],sv.[Mint-F],sv.[Mint-VG]," +
                        "sv.[Used-VF],sv.[Used-F],sv.[Used-VG]," +
                        "sv.FDC,sv.PlateBlock,sv.Block4,sv.LinePair,Commemorative,Definitive " +
                        "FROM((Varieties v " +
                        "INNER JOIN Countries c ON v.CountryID=c.CountryID) " +
                        "LEFT JOIN[SMVal~2018] sv ON v.VarietyID=sv.VarietyID) " +
                        $"WHERE c.CountryName=\"{Properties.Settings.Default.srchCountry}\" " +
                        $"AND v.ScottNum=\"{appOptions.StampsToFind}\"";
                    break;
                case "multi":
                case "m":
                    string myStamps = StringTools.ConvertInputToMultiFormat(appOptions.StampsToFind);
                    MyQuery = "select myC.ScottNumber, Year(v.DateIssued) AS YearIssued," +
                 "v.DescriptiveName, v.VarietyDenomination,v.Perforation, myC.StampType, myC.Condition, " +
                 "c.CountryName, sa.AlbumDescription, myC.AlbumPage " +
                 "FROM (((mycollection myC " +
                 "INNER JOIN Varieties v ON myC.VarietyID=v.VarietyID) " +
                 "INNER JOIN Countries c on v.CountryID=c.CountryID) " +
                 "INNER JOIN StampAlbums sa on myC.StampAlbumID=sa.AlbumID)" +
                 $"WHERE myC.ScottNumber in {myStamps} ORDER BY v.DateIssued ASC";
                    break;
                case "missingyear":
                case "my":
                    if (appOptions.NumParam < 1800)
                    {
                        Console.WriteLine("Must specify year of stamp, using the -y switch");
                        Environment.Exit(1);
                    }
                    MyTableResults = GetStampsForYear(StampDB,appOptions.NumParam,Properties.Settings.Default.srchCountry);
                    DataTable OwnedStamps = GetOwnedStampsForYear(StampDB, appOptions.NumParam, Properties.Settings.Default.srchCountry);
                    DataTable MissingStamps = DetermineMissingStampsForYear(MyTableResults, OwnedStamps);
                    resultsFinalized = true;
                    break;
            }
            if (!resultsFinalized)
                MyTableResults = StampDB.Query(MyQuery);
        }
        private DataTable GetStampsForYear(DB StampDB, int Year,string Country)
        {
            MyQuery = "SELECT ScottNum, Year(v.DateIssued) AS YearIssued,DescriptiveName,VarietyDenomination,Perforation," +
                        "sv.[Mint-VF],sv.[Mint-F],sv.[Mint-VG]," +
                        "sv.[Used-VF],sv.[Used-F],sv.[Used-VG]," +
                        "sv.FDC,sv.PlateBlock,sv.Block4,sv.LinePair,Commemorative,Definitive " +
                        "FROM((Varieties v " +
                        "INNER JOIN Countries c ON v.CountryID=c.CountryID) " +
                        "LEFT JOIN[SMVal~2018] sv ON v.VarietyID=sv.VarietyID) " +
                        $"WHERE c.CountryName=\"{Country}\" " +
                        $"AND Year(v.DateIssued)={Year} " +
                        "AND ScottNum not like \"R*\" " +
                        "AND ScottNum not like \"U*\"";
            return StampDB.Query(MyQuery);
        }
        private DataTable GetOwnedStampsForYear(DB StampDB, int Year, string Country)
        {
            string StampsIOwnForYear = "SELECT DISTINCT scottnumber " +
                        "FROM ((myCollection myC " +
                        "INNER JOIN Varieties v ON myC.varietyID=v.varietyID) " +
                        "INNER JOIN Countries c ON v.CountryID=c.CountryID) " +
                        $"WHERE c.CountryName=\"{Country}\" " +
                        $"AND Year(v.DateIssued)={Year}";
            return StampDB.Query(StampsIOwnForYear);
        }
        private DataTable DetermineMissingStampsForYear(DataTable StampsForYear, DataTable StampsOwnedForYear)
        {
            //1.Create List of Owned Stamps
            List<string> ownedStampNumbers = (from row in StampsOwnedForYear.AsEnumerable()
                                              select row.Field<string>("scottnumber")).Distinct().ToList();
            //2.Check Stamps Available for specified year and determine ones i own
            List<DataRow> DetermineRowsFromYearAlreadyOwned = (from row in StampsForYear.AsEnumerable()
                                 where ownedStampNumbers.Contains(row.Field<string>("ScottNum"))
                                 select row).ToList();
            //3.Remove stamps i own from the stamps available for specified year
            DetermineRowsFromYearAlreadyOwned.ForEach(r => StampsForYear.Rows.Remove(r));
            StampsForYear.AcceptChanges();
            return StampsForYear;
        }
    }
}
