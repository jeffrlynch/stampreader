using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampReader
{
    public static class StringTools
    {
        public static string ConvertInputToMultiFormat(string Input)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(Input))
                return result;
            string[] arrInput = Input.Split(';');
            if (arrInput.Length < 1)
                return result;
            else if (arrInput.Length == 1)
                return ($"('{arrInput[0]}')");//Format Single StampID
            else if (arrInput.Length>1)
            {
                result = "(";
                //Loop to create the SQL In('1','54','3') content
                for (int loop = 0; loop < arrInput.Length; loop++)
                    result += $"'{arrInput[loop]}',";
                //Remove trailing comma
                result=result.TrimEnd().Substring(0,result.Length-1);
                result += ")";
                return result;
            }
            return result;
        }
    }
}
