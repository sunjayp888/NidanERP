using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class FixAssetViewModel :BaseViewModel
    {
        public FixAsset FixAsset { get; set; }
        public SelectList Rooms { get; set; }
        public SelectList Products { get; set; }
        public int FixAssetId { get; set; }
    }
}