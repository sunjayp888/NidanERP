using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nidan.Entity.Dto
{
    public class Schedule
    {
        [JsonProperty("beginDate")]
        public DateTime BeginDate { get; set; }

        [JsonProperty("items")]
        public IEnumerable<ScheduleItem> Items { get; set; }

        [JsonProperty("types")]
        public IEnumerable<ScheduleItemType> Types { get; set; }
    }
}
