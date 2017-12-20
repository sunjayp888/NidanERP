﻿using Nidan.Entity.Dto;

namespace Nidan.Models
{
    public class BaseViewModel
    {
        public string OrganisationName { get; set; }
        public string CentreName { get; set; }
        public string CentreType { get; set; }
        public int PersonnelId { get; set; }
        public int CentreId { get; set; }
        public int AssetClassId { get; set; }
        public Permissions Permissions { get; set; }
    }
}