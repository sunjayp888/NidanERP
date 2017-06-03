using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Interfaces
{
    public interface ISMSService
    {
        void SendSMS(SmsData smsData);
    }

    public class SmsData
    {
        public string From { get; set; }
        public string To { get; set; }
        public string MessageBody { get; set; }
    }
}
