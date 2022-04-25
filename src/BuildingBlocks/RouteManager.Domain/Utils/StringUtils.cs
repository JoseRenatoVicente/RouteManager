using System.Globalization;

namespace RouteManager.Domain.Utils
{
    public static class StringUtils
    {
        public static bool CompareToIgnore(string stringA, string stringB)
        {
            return string.Compare(stringA, stringB, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0;
        }
    }
}
