using System.Text;

namespace Sreeni.OData.Transpiler.Core
{
    public class ODataQuery
    {
        public List<string> Select { get; set; }
        public string Filter { get; set; }
        public List<string> OrderBy { get; set; }
        public int? Top { get; set; }
        public bool? Count { get; set; }
        public int? Skip { get; set; }
        public List<string> Expand { get; set; }

        public void Accept(IODataQueryVisitor visitor, StringBuilder sqlBuilder, List<QueryParameter> parameters)
        {
            visitor.Visit(this, sqlBuilder, parameters);
        }
    }
}
