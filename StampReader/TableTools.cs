using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampReader
{
    public class TableTools
    {
        public static DataTable ClearNullColumns(DataTable tableToCleanup)
        {
            if (tableToCleanup.Rows.Count>0 && tableToCleanup.Columns.Count>0)
            {
                foreach (DataColumn column in tableToCleanup.Columns.Cast<DataColumn>().ToArray())
                {
                    if (tableToCleanup.AsEnumerable().All(dr => dr.IsNull(column)))
                        tableToCleanup.Columns.Remove(column);
                }
            }
            tableToCleanup.AcceptChanges();
            tableToCleanup = ClearZeroValColumns(tableToCleanup);
            tableToCleanup.AcceptChanges();
            return tableToCleanup;
        }
        public static DataTable ClearZeroValColumns(DataTable tableToCleanup)
        {
            DataTable ZeroColsRemoved = new DataTable();
            List<string> columnsToDelete = new List<string>();
            foreach (DataColumn col in tableToCleanup.Columns)
                if (isColumnZero(tableToCleanup, col.ColumnName))
                    columnsToDelete.Add(col.ColumnName);
            foreach (string colToDelete in columnsToDelete)
                tableToCleanup.Columns.Remove(colToDelete);
            return tableToCleanup;
        }
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
            {
                typeof(int),
                typeof(uint),
                typeof(double),
                typeof(decimal)
            };
        private static bool isNumericType(Type type)
        {
            return NumericTypes.Contains(type) ||
                NumericTypes.Contains(Nullable.GetUnderlyingType(type));
        }
        protected static bool isColumnZero(DataTable tableToCheck,string colName)
        {
            Type dataType = tableToCheck.Columns[colName].DataType;    
            if (isNumericType(dataType))
            {
                foreach (DataRow row in tableToCheck.Rows)
                {
                    if (row[colName] == DBNull.Value)
                        continue;
                    if (row[colName].GetType() == typeof(decimal))
                    {
                        if ((decimal)row[colName] != 0)
                            return false;
                    }
                    else if ((int)row[colName] != 0)
                            return false;
                }
                return true;//All records==0
            }
            else
                return false;
        }
    }
}
