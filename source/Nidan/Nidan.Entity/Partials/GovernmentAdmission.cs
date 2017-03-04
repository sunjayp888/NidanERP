using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(GovernmentAdmissionMetadata))]
    public partial class GovernmentAdmission : IOrganisationFilterable
    {
        private class GovernmentAdmissionMetadata
        {
            [Display(Name = "Scheme")]
            public int SchemeId { get; set; }

            [Display(Name = "Training Type")]
            public string TrainingType { get; set; }

            [Display(Name = "Sector")]
            public int SectorId { get; set; }

            [Display(Name = "Sub-Sector")]
            public int SubSectorId { get; set; }

            [Display(Name = "Job Role")]
            public int CourseId { get; set; }

            [Display(Name = "Where did you hear about the scheme ?")]
            public int HowDidYouKnowAboutId { get; set; }

            [Display(Name = "Conveyance And Boarding Preference ?")]
            public string ConveyanceAndBoardingPreference { get; set; }

            [Display(Name = "Salutation")]
            public string Salutation { get; set; }

            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Middle Name")]
            public string MiddleName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "D/O")]
            public string FatherName { get; set; }

            [Display(Name = "Date Of Birth")]
            public DateTime DateOfBirth { get; set; }

            [Display(Name = "Year Of Birth")]
            public int? YearOfBirth { get; set; }

            [Display(Name = "Aadhaar No")]
            public long AadhaarNo { get; set; }

            [Display(Name = "Aadhaar Verification Status")]
            public string AadhaarVerificationStatus { get; set; }

            [Display(Name = "Type of Disability ?")]
            public int DisabilityId { get; set; }

            [Display(Name = "Education Attained")]
            public int QualificationId { get; set; }

            [Display(Name = "Caste Category")]
            public int CasteCategoryId { get; set; }

            [Display(Name = "Religion")]
            public int ReligionId { get; set; }

            [Display(Name = "Alternate ID Type")]
            public int? AlternateIdTypeId { get; set; }

            [Display(Name = "Alternate ID Number")]
            public long? AlternateIdNumber { get; set; }

            [Display(Name = "Landline No")]
            public long? LandlineNo { get; set; }

            [Display(Name = "Email ID")]
            public string EmailId { get; set; }

            [Display(Name = "Name As In Bank")]
            public string NameAsInBank { get; set; }

            [Display(Name = "Bank Account No")]
            public long? BankAccountNo { get; set; }

            [Display(Name = "IFSC Code")]
            public string IfscCode { get; set; }

            [Display(Name = "Bank Name")]
            public string BankName { get; set; }

            [Display(Name = "TC Name")]
            public string TcName { get; set; }

            [Display(Name = "TC ID")]
            public string TcId { get; set; }

            [Display(Name = "Partner Name")]
            public string PartnerName { get; set; }

            [Display(Name = "Address")]
            public string TcAddress { get; set; }

            [Display(Name = "SDMS Candidate ID")]
            public string SdmsCandidateId { get; set; }
        }
    }
}
