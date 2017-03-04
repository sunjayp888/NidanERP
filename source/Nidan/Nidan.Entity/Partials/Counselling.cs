﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;
using System;

namespace Nidan.Entity
{
    [MetadataType(typeof(CounsellingMetadata))]
    public partial class Counselling : IOrganisationFilterable
    {
        private class CounsellingMetadata
        {
            [Display(Name = "Sector")]
            public int SectorId { get; set; }

            [Display(Name = "Course Offered")]
            public int CourseOfferedId { get; set; }

            [Display(Name = "Prefer Timing")]
            public string PreferTiming { get; set; }

            [Display(Name = "Conversion Prospect")]
            public string ConversionProspect { get; set; }

            [Display(Name = "Follow-Up Date")]
            public DateTime? FollowUpDate { get; set; }
        }
    }
}