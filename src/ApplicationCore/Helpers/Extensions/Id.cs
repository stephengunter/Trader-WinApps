using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Helpers
{
    public class TickId
    {
        public static string Create(int length = 20)
        {
            string val = $"{DateTime.Today.ToDateNumber()}{ConvertToBase(DateTime.Now.Ticks)}";
            return val.Length > length ? val.Substring(0, 20) : val;
        }
        

        static String ConvertToBase(long num)
        {
            int nbase = 36;
            String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            long r;
            String newNumber = "";

            // in r we have the offset of the char that was converted to the new base
            while (num >= nbase)
            {
                r = num % nbase;
                newNumber = chars[(int)r] + newNumber;
                num = num / nbase;
            }
            // the last number to convert
            newNumber = chars[(int)num] + newNumber;

            return newNumber;
        }
    }
}
