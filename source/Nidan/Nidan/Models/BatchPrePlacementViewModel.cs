using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Nidan.Entity;

namespace Nidan.Models
{
    public class BatchPrePlacementViewModel : BaseViewModel
    {
        public BatchPrePlacement BatchPrePlacement { get; set; }
        public SelectList Batches { get; set; }
        public SelectList Centres { get; set; }
        public SelectList PrePlacementActivities { get; set; }
        public int CentreId { get; set; }
        public int BatchId { get; set; }
        public int BatchPrePlacementId { get; set; }
        public CandidatePrePlacement CandidatePrePlacement { get; set; }
    }
}