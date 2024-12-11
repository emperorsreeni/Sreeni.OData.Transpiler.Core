namespace Sreeni.OData.Transpiler.Core
{
    public interface IODataClient
    {
        Task<T> GetItemByQueryAsync<T>(string  query) where T : class;
       
    }
}
