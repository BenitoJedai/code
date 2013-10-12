using com.google.apphosting.api;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RemainingMillisExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public string title = "before0";
        public int counter = 0;

        //INFO: Dev App Server is now running
        //IsConstructor WriteInternalFields
        //time to serialize fields into cookie { Length = 2 }
        //AddField { FieldName = field_title, FieldValue = YmVmb3JlMA== }
        //AddField { FieldName = field_counter, FieldValue = 0 }
        //time to serialize fields into cookie { Length = 2 }
        //AddField { FieldName = field_title, FieldValue = YmVmb3JlMC4= }
        //AddField { FieldName = field_counter, FieldValue = 1 }
        //RemainingMillis enter { title = before0., counter = 1 }

        // jsc, can you turn AppEngine SDK into async yet?

        public Task<long> RemainingMillis
        {
            get
            {
                //                INFO: Dev App Server is now running
                //IsConstructor WriteInternalFields
                //Invoke arg1 : InternalWebMethodInfo = { IsConstructor = true, MetadataToken = , Name = , TypeFullName =  }
                //time to serialize fields into cookie { Length = 2 }
                //InternalWebMethodInfo.AddField { FieldName = field_title, FieldValue = YmVmb3JlMA== }
                //InternalWebMethodInfo.AddField { FieldName = field_counter, FieldValue = 0 }
                //Invoke arg1 : InternalWebMethodInfo = { IsConstructor = false, MetadataToken = 06000001, Name = get_RemainingMillis, TypeFullName = RemainingMillisExperiment.ApplicationWebService }
                //time to serialize fields into cookie { Length = 2 }
                //InternalWebMethodInfo.AddField { FieldName = field_title, FieldValue = YmVmb3JlMC4= }
                //InternalWebMethodInfo.AddField { FieldName = field_counter, FieldValue = 1 }
                //RemainingMillis enter { title = before0., counter = 1 }


                Console.WriteLine("RemainingMillis enter " + this);

                // Set-Cookie:InternalFields=field_title=YmVmb3Jl&;  path=/
                // Set-Cookie:InternalFields=field_title=YmVmb3Jl&;  path=/
                this.title = "after";
                this.counter++;

                // http://stackoverflow.com/questions/13351563/how-to-impose-google-app-engines-deadlineexceededexception

                // The method or operation is not implemented.
                var en = ApiProxy.getCurrentEnvironment();

                var RemainingMillis = en.getRemainingMillis();

                // Send it back to the caller.
                return RemainingMillis.ToTaskResult();
            }
        }

        //public override string ToString()
        //{
        //    return new { this.counter, this.title }.ToString();

        //}

    }
}
