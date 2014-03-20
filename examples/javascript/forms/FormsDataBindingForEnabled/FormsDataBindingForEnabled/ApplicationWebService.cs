using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

        public async Task<bool> CheckEnabled()
        {
            var now = DateTime.Now;

            // 1 sec enabled
            // then 2 sec disabled?
            var Checked = now.Second % 3 == 0;

            //Console.WriteLine(new { Checked });

            // http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx

            return Checked;
        }


    }


}
