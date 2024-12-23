namespace Sreeni.OData.Transpiler.Core
{
    public interface IODataQueryTranslator
    {
        QueryResult Translate(string query, bool includeCount=false);
    }
}
