using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Entity.Dto
{
    public class DivisionBuilding
    {
        public int DivisionId { get; set; }
        public int BuildingId { get; set; }
        public int CountryId { get; set; }
        public string DivisionBuildingId => DivisionId + "#" + BuildingId;
        public string Name { get; set; }
        public string DivisionColour { get; set; }
        public string BuildingName { get; set; }
        public string DivisionName { get; set; }
    }
}
