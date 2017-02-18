using System.Collections.Generic;
using System.Web.Mvc;
using HR.Entity;
using Nidan.Entity;

namespace Nidan.Models
{
    public class PersonnelProfileViewModel : BaseViewModel
    {
        public Personnel Personnel { get; set; }        
        public string PhotoBytes { get; set; }
        public SelectList Centres { get; set; }
    }
}