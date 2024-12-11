namespace Sreeni.OData.Transpiler.Core
{
    public interface IODataListClient : IODataClient
    {
        Task<IEnumerable<T>> GetListByQueryAsync<T>(string query) where T : class;
    }
}
