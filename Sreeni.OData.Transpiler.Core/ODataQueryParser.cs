namespace Sreeni.OData.Transpiler.Core
{
    public class ODataQueryParser
    {
        public ODataQuery Parse(string query)
        {
            var result = new ODataQuery();
            var queryParts = query.Split(['&'], StringSplitOptions.RemoveEmptyEntries);
            foreach (var queryPart in queryParts)
            {
                var parts = queryPart.Split(['='], StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                {
                    throw new ArgumentException("Invalid query part: " + queryPart);
                }
                var key = parts[0];
                var value = parts[1];
                switch (key)
                {
                    case "$select":
                        result.Select = value.Split([','], StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
                        break;
                    case "$filter":
                        result.Filter = value;
                        break;
                    case "$orderby":
                        result.OrderBy = value.Split([','], StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
                        break;
                    case "$top":
                        result.Top = int.Parse(value);
                        break;
                    case "$skip":
                        result.Skip = int.Parse(value);
                        break;
                    case "$count":
                        result.Count = bool.Parse(value);
                        break;
                    case "$expand":
                        result.Expand = value.Split([','], StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
                        break;
                }
            }
            return result;
        }
    }
}
