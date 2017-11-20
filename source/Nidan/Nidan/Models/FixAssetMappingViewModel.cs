using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class FixAssetMappingViewModel : BaseViewModel
    {
        public FixAsset FixAsset { get; set; }
        public FixAssetMapping FixAssetMapping { get; set; }
        public List<FixAssetDataGrid> FixAssetMappingList { get; set; }
        public SelectList AssignTypes { get; set; }
        public SelectList AssetOutStatus { get; set; }
        public int FixAssetId { get; set; }
        public int AssetClassId { get; set; }
    }
}