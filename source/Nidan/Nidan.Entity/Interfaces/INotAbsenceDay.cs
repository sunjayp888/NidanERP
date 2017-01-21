using System;

namespace HR.Entity.Interfaces
{
    public interface INotAbsenceDay
    {        
        DateTime Date { get; set; }
        bool AM { get; set; }
        bool PM { get; set; }
        string ValidationReason { get; }
    }
}
