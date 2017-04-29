namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Registration")]
    public partial class Registration
    {
        public int RegistrationId { get; set; }

        public int? StudentCode { get; set; }

        public int EnquiryId { get; set; }

        public int CandidateFeeId { get; set; }

        public string Remarks { get; set; }

        public DateTime? FollowupDate { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual CandidateFee CandidateFee { get; set; }

        public virtual Registration Registration1 { get; set; }

        public virtual Registration Registration2 { get; set; }
    }
}
