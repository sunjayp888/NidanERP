namespace Nidan.Entity
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [MetadataType(typeof(PersonnelMetadata))]
    public partial class Personnel : IOrganisationFilterable
    {
        [NotMapped]
        public string Fullname => string.Join(" ", new string[] { Title.Trim(), Forenames.Trim(), Surname.Trim() }).Trim();     

        [NotMapped]
        public string AddressOverview
        {
            get
            {
                var address = new List<string>();
                if (!string.IsNullOrWhiteSpace(Address1))
                    address.Add(Address1);
                if (!string.IsNullOrWhiteSpace(Postcode))
                    address.Add(Postcode);
                return string.Join(", ", address);
            }
        }

        [NotMapped]
        public DateTime StartDate { get; set; }

        private class PersonnelMetadata
        {
            [Display(Name = "Organisation")]
            public int? OrganisationId { get; set; }

            [Display(Name = "Date of birth")]            
            public DateTime DOB { get; set; }

            [Display(Name = "Country")]
            public int? CountryId { get; set; }

            [Display(Name = "Address")]
            public string Address1 { get; set; }

            public string Address2 { get; set; }

            public string Address3 { get; set; }

            public string Address4 { get; set; }

            [Display(Name = "Home Phone Number")]
            [Phone]
            public string Telephone { get; set; }

            [Display(Name = "Mobile Phone Number")]
            [Phone(ErrorMessage = "Not a valid mobile phone number")]            
            public string Mobile { get; set; }

            [EmailAddress]
            [Display(Name = "Email Address")]
            public string Email { get; set; }

            [Display(Name = "National Insurance Number")]
            public string NINumber { get; set; }

            [Display(Name = "Bank Account Number")]
            public string BankAccountNumber { get; set; }

            [Display(Name = "Bank Sort Code")]
            public string BankSortCode { get; set; }

            [Display(Name = "Bank Account Name")]
            public string BankAccountName { get; set; }

            [Display(Name = "Bank Address")]
            public string BankAddress1 { get; set; }

            public string BankAddress2 { get; set; }

            public string BankAddress3 { get; set; }

            public string BankAddress4 { get; set; }

            [Display(Name = "Bank Postcode")]
            public string BankPostcode { get; set; }

            [Display(Name = "Bank Phone Number")]
            [Phone]
            public string BankTelephone { get; set; }
           
        }
    }
}