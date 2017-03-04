using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(CommercialAdmissionMetadata))]
    public partial class CommercialAdmission : IOrganisationFilterable
    {
        private class CommercialAdmissionMetadata
        {
            [Display(Name = "Sector")]
            public int? SectorId { get; set; }

            [Display(Name = "Course")]
            public int? CourseId { get; set; }

            [Display(Name = "Student Type")]
            public int? StudentTypeId { get; set; }

            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Middle Name")]
            public string MiddleName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Email Id")]
            public string EmailId { get; set; }

            [Display(Name = "Date Of Birth")]
            public DateTime? DateOfBirth { get; set; }

            [Display(Name = "Category Code")]
            public int? CasteCategoryId { get; set; }

            [Display(Name = "Religion")]
            public int? ReligionId { get; set; }

            [Display(Name = "Father Name")]
            public string FatherName { get; set; }

            [Display(Name = "Father Mobile No")]
            public long? FatherMobile { get; set; }

            [Display(Name = "Residential No")]
            public long? ResidentialNo { get; set; }

            [Display(Name = "Permanent Address")]
            public string Address { get; set; }

            [Display(Name = "Taluka/Taluk/Block")]
            public int? TalukaId { get; set; }

            [Display(Name = "District")]
            public int? DistrictId { get; set; }

            [Display(Name = "State")]
            public int? StateId { get; set; }

            [Display(Name = "Pin Code")]
            public int? PinCode { get; set; }

            [Display(Name = "Communication Address")]
            public string CommunicationAddress { get; set; }

            [Display(Name = "Taluka/Taluk/Block")]
            public int? CommunicationTalukaId { get; set; }

            [Display(Name = "District")]
            public int? CommunicationDistrictId { get; set; }

            [Display(Name = "State")]
            public int? CommunicationStateId { get; set; }

            [Display(Name = "Pin Code")]
            public int? CommunicationPinCode { get; set; }

            [Display(Name = "Educational Qualification")]
            public int? QualificationId { get; set; }

            [Display(Name = "Professional Qualification")]
            public string ProfessionalQualification { get; set; }

            [Display(Name = "Technical Qualification")]
            public string TechnicalQualification { get; set; }

            [Display(Name = "Pre-Training Status")]
            public string PreTrainingStatus { get; set; }

            [Display(Name = "Experience in Year(s)")]
            public int YearOfExperience { get; set; }

            [Display(Name = "Employment Status")]
            public string EmploymentStatus { get; set; }

            [Display(Name = "Employer Name")]
            public string EmployerName { get; set; }

            [Display(Name = "Employer Contact No")]
            public long? EmployerContactNo { get; set; }

            [Display(Name = "Employer Address")]
            public string EmployerAddress { get; set; }

            [Display(Name = "Annual Income")]
            public long? AnnualIncome { get; set; }
        }
    }
}
