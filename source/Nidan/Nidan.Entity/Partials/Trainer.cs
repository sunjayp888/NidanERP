using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(TrainerMetadata))]
    public partial class Trainer : IOrganisationFilterable
    {
        [NotMapped]
        public string Fullname => string.Join(" ", new string[] { Title.Trim(), FirstName.Trim(), MiddleName.Trim(), LastName.Trim() }).Trim();
        private class TrainerMetadata
        {
            [Display(Name = "Aadhar Number")]
            public long AadharNo { get; set; }

            [Display(Name = "Certification Number")]
            public string CertificationNo { get; set; }

            [Display(Name = "Sector")]
            public int SectorId { get; set; }

            //[Display(Name = "Course")]
            //public int CourseId { get; set; }

            [Display(Name = "Address 1")]
            public string Address1 { get; set; }

            [Display(Name = "Address 2")]
            public string Address2 { get; set; }

            [Display(Name = "Address 3")]
            public string Address3 { get; set; }

            [Display(Name = "Address 4")]
            public string Address4 { get; set; }

            [Display(Name = "Taluka")]
            public int TalukaId { get; set; }

            [Display(Name = "State")]
            public int StateId { get; set; }

            [Display(Name = "District")]
            public int DistrictId { get; set; }

            [Display(Name = "Pin Code")]
            public string PinCode { get; set; }

            [Display(Name = "Email")]
            public string EmailId { get; set; }
        }
    }
}
