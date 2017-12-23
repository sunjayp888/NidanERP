using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CandidatePrePlacementActivityViewModel : BaseViewModel
    {
        public Admission Admission { get; set; }
        public CandidatePrePlacementActivity CandidatePrePlacementActivity { get; set; }
        public Batch Batch { get; set; }
        public SelectList Batches { get; set; }
    }
}