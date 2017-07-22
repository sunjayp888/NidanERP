using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nidan.Entity;

namespace Nidan.Models
{
    public class EventBrainstormingViewModel : BaseViewModel
    {
        public EventBrainstorming EventBrainstorming { get; set; }
        public List<string> DisscussionCompletedYesNo { get; set; }
        public List<string> RefernceDetailDocument { get; set; }
        public Event Event { get; set; }
        public Brainstorming Brainstorming { get; set; }
    }
}