﻿

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;
using System;

namespace Nidan.Entity
{
    [MetadataType(typeof(EnquiryMetadata))]
    public partial class Enquiry : IOrganisationFilterable
    {
        private class EnquiryMetadata
        {
            [Display(Name = "Candidate Name")]
            [StringLength(500)]
            public string CandidateName { get; set; }

            [Display(Name = "Contact No")]
            public long ContactNo { get; set; }

            [Display(Name = "Email Id")]
            [StringLength(500)]
            public string EmailId { get; set; }

            [Display(Name = "Father Name")]
            [StringLength(500)]
            public string GuardianName { get; set; }

            [Display(Name = "Father Contact No")]
            public long? GuardianContactNo { get; set; }

            [Display(Name = "Occupation")]
            public int OccupationId { get; set; }

            [Display(Name = "Religion")]
            public int ReligionId { get; set; }

            [Display(Name = "Category Code")]
            public int CasteCategoryId { get; set; }

            [Display(Name = "Highest Qualification Completed")]
            public int EducationalQualificationId { get; set; }

            [Display(Name = "Year OF Pass Out")]
            [StringLength(100)]
            public string YearOfPassOut { get; set; }

            [Display(Name = "Intrested Course")]
            public int IntrestedCourseId { get; set; }

            [Display(Name = "How Did You Know About Us")]
            public int HowDidYouKnowAboutId { get; set; }

            [Display(Name = "Pre-Training Status")]
            [StringLength(100)]
            public string PreTrainingStatus { get; set; } 

            public string Gender { get; set; }

            [Display(Name = "Employment Status")]
            [StringLength(100)]
            public string EmploymentStatus { get; set; }

            [Display(Name = "Counselled By")]
            [StringLength(500)]
            public string CounselledBy { get; set; }

            [Display(Name = "Course Offered")]
            public int CourseOfferedId { get; set; }

            [Display(Name = "Prefer Timing")]
            public DateTime? PreferTiming { get; set; }

            [Display(Name = "Follow-Up Date")]
            [Column(TypeName = "date")]
            public DateTime? FollowUpDate { get; set; }

            [Display(Name = "Employer Name")]
            [StringLength(500)]
            public string EmployerName { get; set; }

            [Display(Name = "Employer Contact No")]
            [StringLength(50)]
            public string EmployerContactNo { get; set; }

            [Display(Name = "Employer Address")]
            public string EmployerAddress { get; set; }

            [Display(Name = "Annual Income")]
            public int AnnualIncome { get; set; }

            [Display(Name = "Enquiry Type")]
            public int EnquiryTypeId { get; set; }

            [Display(Name = "Student Type")]
            public int StudentTypeId { get; set; }

            [Display(Name = "Sector")]
            public int SectorId { get; set; }

            [Display(Name = "Batch Time Prefer")]
            public int BatchTimePreferId { get; set; }

            [Display(Name = "Appearing Qualification")]
            [StringLength(500)]
            public string AppearingQualification { get; set; }

            [Display(Name = "Experience in Year(s)")]
            public int YearOfExperience { get; set; }

            [Display(Name = "Enquiry Follow-Up Date")]
            [Column(TypeName = "date")]
            public DateTime? EnquiryFollowUpDate { get; set; }

            [Display(Name = "Placement Needed")]
            [StringLength(100)]
            public string PlacementNeeded { get; set; }

            [Display(Name = "Why Enquiry Closed?")]
            public string WhyEnquiryClosed { get; set; }

            [Display(Name = "Remark By Branch Manager")]
            public string RemarkByBm { get; set; }

            [Display(Name = "Scheme")]
            public int SchemeId { get; set; }
        }
    }
}
