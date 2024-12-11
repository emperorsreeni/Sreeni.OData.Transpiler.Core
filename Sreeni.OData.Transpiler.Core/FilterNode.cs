using Newtonsoft.Json;

namespace Sreeni.OData.Transpiler.Core
{
    public class FilterNode
    {
        [JsonExtensionData]
        public Dictionary<string, object> Conditions { get; set; }
        public List<FilterNode> And { get; set; }
        public List<FilterNode> Or { get; set; }
    }
}
