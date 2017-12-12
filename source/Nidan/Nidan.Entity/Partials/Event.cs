


using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nidan.Entity
{
    using System.ComponentModel.DataAnnotations;
    using Nidan.Entity.Interfaces;

    [MetadataType(typeof(EventTypeMetadata))]
    public partial class Event : IOrganisationFilterable
    {
        private class EventTypeMetadata
        {
            [DataType(DataType.MultilineText)]
            public string Remark { get; set; }

            [Display(Name = "Created By")]
            public int? CreatedBy { get; set; }

            [Display(Name = "Created Date")]
            public DateTime? CreatedDateTime { get; set; }

            [Display(Name = "Approved By")]
            public int? ApprovedBy { get; set; }

            [Display(Name = "Event Approve State")]
            public int EventApproveStateId { get; set; }

            [Display(Name = "Centre")]
            public int CentreId { get; set; }

            [Display(Name = "Start Date")]
            [Column(TypeName = "date")]
            public DateTime? EventStartDate { get; set; }

            [Display(Name = "End Date")]
            [Column(TypeName = "date")]
            public DateTime? EventEndDate { get; set; }
        }
    }
}
