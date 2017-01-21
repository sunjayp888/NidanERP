using Newtonsoft.Json;

namespace Nidan.Entity.Dto
{
    public class ScheduleItemType
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("colour")]
        public string Colour { get; set; }
    }
}
