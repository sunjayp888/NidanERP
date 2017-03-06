﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using HR.Entity;
using Nidan.Entity;
using static System.Net.Mime.MediaTypeNames;

namespace Nidan.Models
{
    public class EnquiryViewModel : BaseViewModel
    {
        public Enquiry Enquiry { get; set; }
        public SelectList EducationalQualifications { get; set; }
        public SelectList Occupations { get; set; }
        public SelectList Religions { get; set; }
        public SelectList CasteCategories { get; set; }
        //public SelectList AreaOfInterests { get; set; }
        public SelectList HowDidYouKnowAbouts { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Schemes { get; set; }
        public SelectList Sectors { get; set; }
        public SelectList BatchTimePrefers { get; set; }
        public SelectList StudentTypes { get; set; }
        public SelectList EnquiryTypes { get; set; }
        public int CreateEnquiryFromMobilizationFollowUpId { get; set; }
        public int MobilizationId { get; set; }
        public double ConversionProspect { get; set; }
        public IEnumerable<SelectListItem> ConversionProspectList { get; set; }
        //  public Counselling Counselling { get; set; }

        public List<ConversionProspectType> ConversionProspectType => new List<ConversionProspectType>()
        {
            new ConversionProspectType() {Id=90,Name="90 - 100" },
            new ConversionProspectType() {Id=80,Name="80 - 90" },
            new ConversionProspectType() {Id=70,Name="70 - 80" },
            new ConversionProspectType() {Id=60,Name="60 - 70" },
            new ConversionProspectType() {Id=50,Name="50 - 60" },
            new ConversionProspectType() {Id=40,Name="40 - 50" },
            new ConversionProspectType() {Id=30,Name="Below 40" }
        };
    }

    public class ConversionProspectType
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}