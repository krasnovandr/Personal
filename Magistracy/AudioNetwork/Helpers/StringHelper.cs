using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AudioNetwork.Helpers
{
    public static class StringHelper
    {
        public static string ToUtf8(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            return new string(str.ToCharArray().
                Select(x => ((x + 848) >= 'А' && (x + 848) <= 'ё') ? (char)(x + 848) : x).
                ToArray());
        }


        public static string ConvertStringArrayToString(this string[] array)
        {
            if (array == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(' ');
            }
            return builder.ToString();
        }
    }
}