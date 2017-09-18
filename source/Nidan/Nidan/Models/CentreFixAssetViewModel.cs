using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CentreFixAssetViewModel : BaseViewModel
    {
        public CentreFixAsset CentreFixAsset { get; set; }
        public SelectList Rooms { get; set; }
    }
}