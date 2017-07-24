using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nidan.Entity;

namespace Nidan.Models
{
    public class EventViewModel : BaseViewModel
    {
        public Event Event { get; set; }
        public EventBrainstorming EventBrainstorming { get; set; }
    }
}