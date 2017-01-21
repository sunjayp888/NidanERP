using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nidan.Entity;


namespace Nidan.Models
{
    public class AbsenceTypeViewModel : BaseViewModel
    {
        [Editable(true)]
        public string Name { get; set; }
        public AbsenceType AbsenceType { get; set; }
        public List<Colour> ColoursList { get; set; }
    }
}