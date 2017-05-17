using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(CentreMetadata))]
    public partial class Centre : IOrganisationFilterable
    {
        private class CentreMetadata
        {
            [Display(Name = "Address")]
            public string Address1 { get; set; }

            [Display(Name = "Address")]
            public string Address2 { get; set; }

            [Display(Name = "Address")]
            public string Address3 { get; set; }

            [Display(Name = "Address")]
            public string Address4 { get; set; }

            [Display(Name = "Taluka")]
            public int TalukaId { get; set; }

            [Display(Name = "State")]
            public int StateId { get; set; }

            [Display(Name = "District")]
            public int DistrictId { get; set; }

            [Display(Name = "Pin Code")]
            public int PinCode { get; set; }

            [Display(Name = "Email")]
            public string EmailId { get; set; }

            [Display(Name = "Contact Number")]
            public long Telephone { get; set; }
        }
    }
}
