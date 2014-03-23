using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FormsDataBindingForEnabled
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        // http://stackoverflow.com/questions/5666103/binding-button-enabled-to-a-property-in-c-sharp-dll   
        // http://stackoverflow.com/questions/12961533/databinding-enabled-if-false
        // http://stackoverflow.com/questions/3989965/why-cant-i-bind-to-a-field-on-a-class-in-wpf-instead-of-binding-to-a-property
        // http://programmers.stackexchange.com/questions/161303/is-it-bad-practice-to-use-public-fields
        // http://stackoverflow.com/questions/3989965/why-cant-i-bind-to-a-field-on-a-class-in-wpf-instead-of-binding-to-a-property
        // http://social.msdn.microsoft.com/Forums/vstudio/en-US/8eefbfcb-2992-4c0f-b31c-cf2f86067555/xna-and-wpf-databinding-to-a-structs-field-such-as-vector3-x-y-z?forum=wpf


        public Stopwatch elapsed = Stopwatch.StartNew();

        // would jsc be smart to make multiple calls in the same
        // call session?
        // what time window would we need?
        // this might break our 204 and 304 logic?
        public async Task<string> CheckText()
        {
            if (elapsed == null)
            {
                //Debugger.Break();

                Thread.Sleep(60000);
                return "error";
            }


            var z = new { elapsed.Elapsed.TotalSeconds }.ToString();

            //Console.WriteLine(z);

            return z;
        }

        public async Task<bool> CheckEnabled()
        {
            if (elapsed == null)
            {
                //Debugger.Break();
                Thread.Sleep(60000);
                return false;
            }

            //var now = DateTime.Now;

            // 1 sec enabled
            // then 2 sec disabled?
            var Checked = ((int)elapsed.Elapsed.TotalSeconds) % 3 == 0;

            //Console.WriteLine(new { Checked });

            // http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx

            return Checked;
        }


    }


}
