using ODataTranspiler.Core;
using System.Text;

namespace ODataTranspiler.Cosmos
{
    public class SelectVisitor : IQueryVisitor
    {
        public void Visit(ODataQuery query, StringBuilder sqlBuilder,List<QueryParameter> queryParameters)
        {
            if (query.Select != null && query.Select.Count > 0)
            {
                sqlBuilder.Append("SELECT ");
                sqlBuilder.Append(string.Join(", ", query.Select.Select(s => $"c.{s.Replace('/','.')}")));
                sqlBuilder.Append(" FROM c");
            }
            else
            {
                sqlBuilder.Append("SELECT * FROM c");
            }
        }
    }
}
