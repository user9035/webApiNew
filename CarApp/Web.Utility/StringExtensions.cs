using System;
using System.Net;

namespace Mikz.Feed.Guard.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string str, int length)
        {

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("Length must be >= 0");
            }

            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }

            if (str.Length <= length)
            {
                return str;
            }

            return str.Substring(0, length);
        }

        public static string Decode(this string input)
        {
            return WebUtility.HtmlDecode(input);
        }
    }
}