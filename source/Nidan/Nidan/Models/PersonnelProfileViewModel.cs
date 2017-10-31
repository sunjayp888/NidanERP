using System.Collections.Generic;
using System.Web.Mvc;
using HR.Entity;
using Nidan.Business.Enum;
using Nidan.Entity;

namespace Nidan.Models
{
    public class PersonnelProfileViewModel : BaseViewModel
    {
        public Personnel Personnel { get; set; }        
        public string PhotoBytes { get; set; }
        public SelectList Centres { get; set; }
        //public Role Role { get; set; }
        public SelectList Roles { get; set; }
        public string Role { get; set; }
    }
}