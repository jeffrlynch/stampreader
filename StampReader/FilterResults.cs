using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace StampReader
{
    public class FilterResults
    {
        public bool hasResults { get; private set; }
        public bool hasMultiResults { get; private set; }
        public DataTable FilteredDT { get; private set; } = new DataTable();
        public FilterResults(DataTable RawResults)
        {
            if (RawResults?.Rows?.Count > 0)
                hasResults = true;
            if (RawResults?.Rows?.Count > 1)
                hasMultiResults = true;
            if (!Properties.Settings.Default.includeAllVarieties && hasMultiResults)
                FilterOtherVarieties(RawResults);
            else
                FilteredDT = RawResults;
            if (!Properties.Settings.Default.includeDefinitive)
                FilterDefinitives(FilteredDT);
            if (Properties.Settings.Default.includeCommemorativeOnly)
                OnlyCommemoratives(FilteredDT);
            if (!Properties.Settings.Default.includeSPecials)
                RemoveSpecials(FilteredDT);
            
        }
        public void FilterOtherVarieties(DataTable WorkTable)
        {
            //1.Identify Rows where last character is not intgeer
            List<DataRow> DetermineAllOtherVarieties = (from row in WorkTable.AsEnumerable()
                                                               where !Regex.IsMatch(row.Field<string>("ScottNum"),@"\d$")
                                                               ||row.Field<string>("ScottNum").Contains('-')
                                                               select row).ToList();
            //2.Remove the Filtered Rows
            DetermineAllOtherVarieties.ForEach(r => WorkTable.Rows.Remove(r));
            WorkTable.AcceptChanges();
            FilteredDT = WorkTable;
        }
        public void FilterDefinitives(DataTable WorkTable)
        {
            //1.Identify Rows where last character is not intgeer
            List<DataRow> DetermineAllOtherVarieties = (from row in WorkTable.AsEnumerable()
                                                        where !Regex.IsMatch(row.Field<string>("ScottNum"), @"\d$")
                                                        || row.Field<bool>("Definitive")==true
                                                        select row).ToList();
            //2.Remove the Filtered Rows
            DetermineAllOtherVarieties.ForEach(r => WorkTable.Rows.Remove(r));
            WorkTable.AcceptChanges();
            FilteredDT = WorkTable;
        }
        public void OnlyCommemoratives(DataTable WorkTable)
        {
            //1.Identify Rows where last character is not intgeer
            List<DataRow> DetermineAllOtherVarieties = (from row in WorkTable.AsEnumerable()
                                                        where row.Field<bool>("Commemorative") == false
                                                        select row).ToList();
            //2.Remove the Filtered Rows
            DetermineAllOtherVarieties.ForEach(r => WorkTable.Rows.Remove(r));
            WorkTable.AcceptChanges();
            FilteredDT = WorkTable;
        }
        public void RemoveSpecials(DataTable WorkTable)
        {
            //1.Identify Rows where last character is not intgeer
            List<DataRow> DetermineSpecials = (from row in WorkTable.AsEnumerable()
                                               where row.Field<string>("ScottNum").StartsWith("SP")
                                               select row).ToList();
            //2.Remove the Filtered Rows
            DetermineSpecials.ForEach(r => WorkTable.Rows.Remove(r));
            WorkTable.AcceptChanges();
            FilteredDT = WorkTable;
        }
    }
}
