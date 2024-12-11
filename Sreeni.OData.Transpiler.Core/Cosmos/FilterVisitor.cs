using ODataTranspiler.Core;
using System.Text;
using System.Text.RegularExpressions;

namespace ODataTranspiler.Cosmos
{
    public class FilterVisitor : IQueryVisitor
    {
        public void Visit(ODataQuery query, StringBuilder sqlBuilder, List<QueryParameter> parameters)
        {
            if (!string.IsNullOrEmpty(query.Filter))
            {
                var filterBuilder = new FilterBuilder(query.Filter, parameters);
                sqlBuilder.Append(" WHERE " + filterBuilder.Build());
            }
        }

    }

    public class FilterBuilder
    {
        private string _filter;
        private readonly List<QueryParameter> _parameters;

        public FilterBuilder(string filter, List<QueryParameter> parameters)
        {
            _filter = filter;
            _parameters = parameters;
        }

        public string Build()
        {
            return ReplaceOperators()
                       .HandleContainsFunction()
                       .HandleInFunction()
                       .HandleBetweenFunction()
                       .HandleConditionalOperators()
                       .Filter();
        }

        private FilterBuilder ReplaceOperators()
        {
            _filter = _filter.Replace(" eq ", " = ")
                             .Replace(" ne ", " != ")
                             .Replace(" gt ", " > ")
                             .Replace(" lt ", " < ")
                             .Replace(" ge ", " >= ")
                             .Replace(" le ", " <= ")
                             .Replace(" and ", " AND ")
                             .Replace(" or ", " OR ");
            return this;
        }

        private FilterBuilder HandleContainsFunction()
        {
            _filter = Regex.Replace(_filter, @"contains\(([^,]+),([^\)]+)\)", match =>
            {
                var field = match.Groups[1].Value.CleanAsField();
                var value = match.Groups[2].Value.Trim().Trim('\'');
                var paramName = _parameters.AddParameter(field, value);
                return $"CONTAINS(c.{field}, {paramName})";
            });
            return this;
        }

        private FilterBuilder HandleInFunction()
        {
            _filter = Regex.Replace(_filter, @"([^ ]+) in \(([^)]+)\)", match =>
            {
                var field = match.Groups[1].Value.CleanAsField();
                var values = match.Groups[2].Value.Split(',').Select(v => v.Trim().Trim('\'')).ToArray();
                var paramNames = values.Select(v => _parameters.AddParameter(field, v));
                return $"c.{field} IN ({string.Join(", ", paramNames)})";
            });
            return this;
        }

        private FilterBuilder HandleBetweenFunction()
        {
            _filter = Regex.Replace(_filter, @"([^ ]+) between ([^ ]+) AND ([^ ]+)", match =>
            {
                var field = match.Groups[1].Value.CleanAsField();
                var startValue = match.Groups[2].Value.Trim().Trim('\'');
                var endValue = match.Groups[3].Value.Trim().Trim('\'');
                var paramName1 = _parameters.AddParameter(field, startValue);
                var paramName2 = _parameters.AddParameter(field, endValue);
                return $"c.{field} BETWEEN {paramName1} AND {paramName2}";
            });
            return this;
        }

        private FilterBuilder HandleConditionalOperators()
        {
            _filter = Regex.Replace(_filter, @"(\w+) (=|!=|>|>=|<|<=) ([^ ]+)", match =>
            {
                var field = match.Groups[1].Value.CleanAsField();
                var op = match.Groups[2].Value.Trim();
                var value = match.Groups[3].Value.Trim().Trim('\'');
                var paramName = _parameters.AddParameter(field, value);
                return $"c.{field} {op} {paramName}";
            });
            return this;
        }

        private string Filter() {
            return _filter;
        }
    }
}
