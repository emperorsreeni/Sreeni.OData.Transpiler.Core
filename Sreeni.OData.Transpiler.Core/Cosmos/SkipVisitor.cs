using ODataTranspiler.Core;
using System.Text;

namespace ODataTranspiler.Cosmos
{
    public class SkipVisitor : IQueryVisitor
    {
        public void Visit(ODataQuery query, StringBuilder sqlBuilder, List<QueryParameter> queryParameters)
        {
            if (query.Skip.HasValue)
            {
                if (sqlBuilder.HasOffsetAlready())
                    return;
                var top = query.Top.HasValue ? query.Top.Value : 1;
                sqlBuilder.Append($" OFFSET {query.Skip.Value} LIMIT {top}");
            }
        }
    }
}
