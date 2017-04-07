namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Room")]
    public partial class Room
    {
        public int RoomId { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public int Floor { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OccupiedStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OccupiedEndDate { get; set; }

        [StringLength(50)]
        public string OccupiedStartTime { get; set; }

        [StringLength(50)]
        public string OccupiedEndTime { get; set; }

        public int? BatchId { get; set; }

        public int RoomTypeId { get; set; }

        public int Capacity { get; set; }

        public int SquareFeet { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual RoomType RoomType { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

    }
}
