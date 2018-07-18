using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class GovernmentMobilizationViewModel : BaseViewModel
    {
        public GovernmentMobilization GovernmentMobilization { get; set; }
        public SelectList Districts { get; set; }
        public SelectList DistrictBlocks { get; set; }
        public SelectList BlockPanchayats { get; set; }
        public SelectList Qualifications { get; set; }
        public SelectList Religions { get; set; }
        public SelectList CasteCategories { get; set; }
        public int DistrictId { get; set; }
        public int DistrictBlockId { get; set; }
        public int BlockPanchayatId { get; set; }
        public int QualificationId { get; set; }
        public int ReligionId { get; set; }
        public int CasteCategoryId { get; set; }
        public GovernmentMobilizationGrid GovernmentMobilizationGrid { get; set; }
    }
}