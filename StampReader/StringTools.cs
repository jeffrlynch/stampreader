using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampReader
{
    public static class StringTools
    {
        public static string ConvertInputToStampQueryFomatMulti(string Input)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(Input))
                return result;
            string[] arrInput = Input.Split(';');
            if (arrInput.Length < 1)
                return result;
            else if (arrInput.Length == 1)
                return ($"('{arrInput[0]}')");
            else if (arrInput.Length>1)
            {
                result = "(";
                for (int loop = 0; loop < arrInput.Length; loop++)
                    result += $"'{arrInput[loop]}',";
                result=result.TrimEnd().Substring(0,result.Length-1);
                result += ")";
                return result;
            }
            return result;
        }
    }
}
