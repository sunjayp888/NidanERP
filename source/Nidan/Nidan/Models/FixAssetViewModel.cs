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
        public int FixAssetId { get; set; }
        public SelectList AssetClasses { get; set; }
        public SelectList Itemes { get; set; }
        public SelectList Centres { get; set; }
    }
}