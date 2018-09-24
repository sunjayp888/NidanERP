using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(GovernmentMobilizationMetadata))]
    public partial class GovernmentMobilization : IOrganisationFilterable
    {
        private class GovernmentMobilizationMetadata
        {
            [Display(Name = "Father Name")]
            public string FatherName { get; set; }

           [Display(Name = "Parent Mobile")]
            public string ParentMobile { get; set; }

            [Display(Name = "District")]
            public int DistrictId { get; set; }

            [Display(Name = "Block")]
            public int DistrictBlockId { get; set; }

            [Display(Name = "Panchayat")]
            public int BlockPanchayatId { get; set; }

            [Display(Name = "Qualification")]
            public int QualificationId { get; set; }

            [Display(Name = "Other Qualification")]
            public string OtherQualification { get; set; }

            [Display(Name = "Religion")]
            public int ReligionId { get; set; }

            [Display(Name = "Caste Category")]
            public int CasteCategoryId { get; set; }

            [Display(Name = "Sub Caste")]
            public string SubCaste { get; set; }

            [Display(Name = "Date of Birth")]
            public DateTime DateofBirth { get; set; }

            [Display(Name = "Marital Status")]
            public string MaritalStatus { get; set; }

            [Display(Name = "Remarks")]
            public string Remark { get; set; }
        }
    }
}
