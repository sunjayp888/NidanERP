﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CandidateFinalPlacementViewModel : BaseViewModel
    {
        public Admission Admission { get; set; }
        public CandidateFinalPlacement CandidateFinalPlacement { get; set; }
        public Batch Batch { get; set; }
        public SelectList Batches { get; set; }
        public int AdmissionId { get; set; }
        public int CandidateFinalPlacementId { get; set; }
        public int BatchId { get; set; }
        public string CandidateName { get; set; }
        public long Mobile { get; set; }
        public string EmailId { get; set; }
        public string Course { get; set; }
        public SelectList Companies { get; set; }
        public SelectList CompanyBranches { get; set; }
        public SelectList PlacementStates { get; set; }
    }
}