using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nidan.Entity.Dto
{
    public class OrderBy
    {
        public string Property { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ListSortDirection Direction { get; set; }
    }
}
