using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PvPGamingWebsite.Statics
{
    public static class Methods
    {
        public static string TrimWithDotting(string input, int length)
        {
            string result = Trim(input, length);
            if (result == input)
            {
                return result;
            }
            else
            {
                return result + "...";
            }
        }

        public static string Trim(string input, int length)
        {
            char[] inputArr = input.ToCharArray();
            if (inputArr.Length > length)
            {
                string result = "";
                inputArr.Take(length).ToList().ForEach((item) => { result += item; });
                return result;
            }
            else
            {
                return input;
            }
        }
    }
}