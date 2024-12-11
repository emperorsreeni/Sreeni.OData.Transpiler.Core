using ODataTranspiler.Core;
using System.Text;

namespace ODataTranspiler.Cosmos
{
    public class TopVisitor : IQueryVisitor
    {
        public void Visit(ODataQuery query, StringBuilder sqlBuilder, List<QueryParameter> queryParameters)
        {
            if (query.Top.HasValue)
            {
                if (sqlBuilder.HasOffsetAlready())
                    return;
                var offset = query.Skip.HasValue ? query.Skip.Value : 0;
                sqlBuilder.Append($" OFFSET {offset} LIMIT {query.Top.Value}");
            }
        }
    }
}
