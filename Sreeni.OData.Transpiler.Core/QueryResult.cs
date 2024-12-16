namespace Sreeni.OData.Transpiler.Core
{
    public class QueryResult
    {
        public string Query { get; set; }
        public List<QueryParameter> Parameters { get; set; }
        public string CountQuery { get; set; }
    }
}
