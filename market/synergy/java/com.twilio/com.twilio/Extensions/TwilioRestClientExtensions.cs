using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace com.twilio.sdk
{
    [Description("Adding .NET like convenience for Google App Engine.")]
    public static class TwilioRestClientExtensions
    {
        public static com.twilio.sdk.resource.instance.Sms SendSmsMessage(this TwilioRestClient client, string from, string to, string body)
        {
            var sms = default(com.twilio.sdk.resource.instance.Sms);
            try
            {
                var account = client.getAccount();


                var smsFactory = account.getSmsFactory();
                // by using this type we are binding against desktop JVM. what about android?
                // should we move HashMap definition to ScriptCoreLib ?
                var smsParams = new java.util.HashMap();
                smsParams.put("To", to);
                smsParams.put("From", from); // Replace with a valid phone
                // number in your account
                smsParams.put("Body", body);

                // this will cost 0.01 USD
                //                COST
                //-0.012
                sms = smsFactory.create(smsParams);
            }
            catch
            {
                throw;
            }

            return sms;
        }
    }
}
