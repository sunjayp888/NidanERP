using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(TrainerMetadata))]
    public partial class Trainer : IOrganisationFilterable
    {
        private class TrainerMetadata
        {
            [Display(Name = "Aadhar No")]
            public long AadharNo { get; set; }

            [Display(Name = "Certification No")]
            public string CertificationNo { get; set; }

            [Display(Name = "Sector")]
            public int SectorId { get; set; }

            [Display(Name = "Course")]
            public int CourseId { get; set; }
        }
    }
}
