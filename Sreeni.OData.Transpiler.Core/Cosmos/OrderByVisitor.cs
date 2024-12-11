using ODataTranspiler.Core;
using System.Text;

namespace ODataTranspiler.Cosmos
{
    public class OrderByVisitor : IQueryVisitor
    {
        public void Visit(ODataQuery query, StringBuilder sqlBuilder, List<QueryParameter> queryParameters)
        {
            if (query.OrderBy != null && query.OrderBy.Count > 0)
            {
                sqlBuilder.Append(" ORDER BY " + string.Join(", ", query.OrderBy.Select(o => $"c.{o.Split(' ')[0]} {o.Split(' ')[1].ToUpperInvariant()}")));
            }
        }
    }
}
