using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Helpers
{
    public static class CNHelpers
    {
        static string[]  cnNumbers = new string[] { "零","一","二","三","四","五","六","七","八","九","十" };

        public static string ToCNNumber(this int val)
        {
            string strVal = val.ToString();
            int length = strVal.Length;
            string intStr = strVal.Substring(length - 1, 1);

            if (length == 1) return cnNumbers[intStr.ToInt()];

            intStr = intStr.ToInt() == 0 ? "" : cnNumbers[intStr.ToInt()];

            string ten = strVal.Substring(length - 2, 1);
            string tenStr = cnNumbers[ten.ToInt()];
            if (length == 2)
            {
                if (String.IsNullOrEmpty(intStr))
                {
                    return $"{tenStr}十{intStr}";
                }
                else
                {
                    if (ten.ToInt() > 1) return $"{ tenStr}十{ intStr}";
                    else return $"十{ intStr}";
                }

            }

            string hundred = strVal.Substring(length - 3, 1);
            string hundredStr = cnNumbers[hundred.ToInt()];
            if (length == 3)
            {
                if (String.IsNullOrEmpty(intStr))
                {
                    return $"{hundredStr}百{tenStr}十";
                }
                else
                {
                    return $"{hundredStr}百{tenStr}十${intStr}";

                }
            }

            return "";

        }
    }
}
