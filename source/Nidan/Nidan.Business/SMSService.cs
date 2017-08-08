using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Nidan.Business.Interfaces;

namespace Nidan.Business
{
    public class SMSService : ISMSService
    {
        public void SendSMS(SmsData smsData)
        {
            var url = CreateSmsUrl(smsData);
            GetResponse(url);
        }

        public static string GetResponse(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var receiveStream = response.GetResponseStream();
                var readStream = new StreamReader(receiveStream, Encoding.UTF8);
                var sResponse = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                return sResponse;
            }
            catch
            {
                return string.Empty;
            }
        }

        public string CreateSmsUrl(SmsData smsData)
        {
            var smsBaseUrl = ConfigurationManager.AppSettings["SMSBaseUrl"];
            var smsUserId = ConfigurationManager.AppSettings["SMSUserId"];
            var smsPassword = ConfigurationManager.AppSettings["SMSPassword"];
            var smsSenderId = ConfigurationManager.AppSettings["SMSSenderId"];
            string url = smsBaseUrl + smsUserId + "&password=" + smsPassword + "&msisdn=91" + smsData.To + "&sid=" + smsSenderId + "&msg=" + smsData.MessageBody + "&fl=0&gwid=2";
            return url;
        }
    }
}
