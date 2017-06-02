using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Enum
{
    public enum FeeType
    {
        Registration = 1,
        Admission,
        Installment
    }

    public enum FeePaymentMethod
    {
        MonthlyInstallment,
        LumpsumAmount
    }

    public enum PaymentMode
    {
        Cash = 1,
        Cheque
    }
}
