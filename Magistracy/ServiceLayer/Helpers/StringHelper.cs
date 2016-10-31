using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceLayer.Helpers
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

        public static string StripHtml(this string source)
        {
            string output;

            //get rid of HTML tags
            output = Regex.Replace(source, "<[^>]*>", string.Empty);

            //get rid of multiple blank lines
            output = Regex.Replace(output, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

            return output;
        }
    }
}