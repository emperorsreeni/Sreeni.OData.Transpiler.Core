using System.Text;

namespace Sreeni.OData.Transpiler.Core
{
    public interface IODataQueryVisitor
    {
        void Visit(ODataQuery query, StringBuilder sqlBuilder, List<QueryParameter> parameters);
    }
}
