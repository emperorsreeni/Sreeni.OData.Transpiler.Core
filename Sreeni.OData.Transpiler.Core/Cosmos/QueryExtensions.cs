using ODataTranspiler.Core;
using System.Text;

namespace ODataTranspiler.Cosmos
{
    public static class QueryExtensions
    {
        public static string AddParameter(this List<QueryParameter> parameters, string key, object value)
        {
            string paramName = $"@param{parameters.Count}";
            parameters.Add(new QueryParameter { Name = paramName, Value = value });
            return paramName;
        }

        public static bool IsEmpty(this ODataQuery query)
        {
            return string.IsNullOrEmpty(query.Filter) 
                && query.OrderBy == null 
                && query.Select == null 
                && query.Top == null;
        }

        public static bool HasOffsetAlready(this StringBuilder query)
        {
            var queryStr = query.ToString();
            return queryStr.Contains("OFFSET");
        }

        public static string CleanAsField(this string field)
        {
            return field.Trim().Replace('/', '.');
        }
    }
}
