using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(CompanyBranchMetadata))]
    public partial class CompanyBranch : IOrganisationFilterable
    {
        private class CompanyBranchMetadata
        {

            [Display(Name = "Company Branch")]
            public string Name { get; set; }

            [Display(Name = "Address 1")]
            public string Address1 { get; set; }

            [Display(Name = "Address 2")]
            public string Address2 { get; set; }

            [Display(Name = "Address 3")]
            public string Address3 { get; set; }

            [Display(Name = "Address 4")]
            public string Address4 { get; set; }

            [Display(Name = "HR Name 1")]
            public string HRName1 { get; set; }

            [Display(Name = "HR Email 1")]
            public string HREmail1 { get; set; }

            [Display(Name = "HR Contact 1")]
            public long HRContact1 { get; set; }

            [Display(Name = "HR Name 2")]
            public string HRName2 { get; set; }

            [Display(Name = "HR Email 2")]
            public string HREmail2 { get; set; }

            [Display(Name = "HR Contact 2")]
            public long? HRContact2 { get; set; }

            [Display(Name = "Sector")]
            public int SectorId { get; set; }

        }
    }
}
