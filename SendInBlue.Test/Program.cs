using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace SendInBlue.Test
{
    class Program
    {
        private const string mailServiceKey = "Your send in bule V3 key";
        static void Main(string[] args)
        {
            #region send mail

            var toMail = "";
            var toMailDisplay = "";
            var mailBody = "";
            SendMail(toMail, toMailDisplay, mailBody);

            #endregion send Mail

            #region send message
            string mobileNumbe = string.Empty;
            string messageBody = string.Empty;
            sendMessage(mobileNumbe, messageBody);
            #endregion Send message


            Console.Read();
        }


        private static void SendMail(string toMail, string todisplayName, string body)
#pragma warning restore S3776 // Cognitive Complexity of methods should not be too high
#pragma warning restore S1541 // Methods and properties should not be too complex
        {
            try
            {
                var sendinBlue = new SendInBlue.APIV3(mailServiceKey);
                Dictionary<string, object> data = new Dictionary<string, object>();
                Dictionary<string, string> to = new Dictionary<string, string>();

                to.Add(toMail, todisplayName);

                Dictionary<string, string> cc = new Dictionary<string, string>();
                Dictionary<string, string> bcc = new Dictionary<string, string>();
                List<string> from = new List<string>();
                from.Add("from Mail id");
                from.Add("from mail display name");
                data.Add("to", to);
                data.Add("cc", cc);
                data.Add("bcc", bcc);
                data.Add("from", from);
                data.Add("subject", "Test V3 Send In Blue");
                data.Add("html", body);
                object sendEmail = sendinBlue.send_email(data);
                var details = JObject.Parse(sendEmail.ToString());
                if (details["code"].ToString() == "success")
                {
                    Console.WriteLine($"Send mail success");
                }
                Console.WriteLine($"Send mail failed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send mail Error: {ex.Message}");
            }
        }


        private static void sendMessage(string toMobileNumber, string messageBody)
        {
            try
            {
                var sendinBlue = new SendInBlue.APIV3(mailServiceKey);
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("to", toMobileNumber);
                data.Add("from", "From");
                data.Add("text", messageBody);
                object sendSms = sendinBlue.send_sms(data);
                var details = JObject.Parse(sendSms.ToString());
                if (details["code"].ToString() == "success")
                {
                    Console.WriteLine("success");
                }
                Console.WriteLine("Failed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
