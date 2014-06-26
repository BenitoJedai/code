using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSpecialFieldInfo;
using TestSpecialFieldInfo.Design;
using TestSpecialFieldInfo.HTML.Pages;
using System.Reflection;
using System.Globalization;

namespace TestSpecialFieldInfo
{
    class xSpecial
    {
    }
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\test\TestMemberInitExpression\TestMemberInitExpression\Application.cs

            // {{ Name = foo, FieldType = [native] String }}
            var x = new { foo = new xSpecial() };

            var f = x.GetType().GetFields()[0];

            new IHTMLPre { new { f.Name, f.FieldType } }.AttachToDocument();

            var xf = new sFieldInfo { InternalField = f };

            new IHTMLPre { new { xf.Name, xf.FieldType } }.AttachToDocument();
        }

    }

    class sFieldInfo : FieldInfo
    {
        public FieldInfo InternalField;

        //02000030 TestSpecialFieldInfo.sFieldInfo
        //script: error JSC1000: override is in effect, base class mehtod should be overridden
        //script: error JSC1000: unable to map override to[TestSpecialFieldInfo.sFieldInfo.get_Attributes]
        //*** Compiler cannot continue... press enter to quit.

        public override FieldAttributes Attributes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Type DeclaringType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override RuntimeFieldHandle FieldHandle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Type FieldType
        {
            get
            {
                return typeof(xSpecial);
            }
        }

        public override string Name
        {
            get
            {
                // {{ Name = null, FieldType = <Namespace>.xSpecial }}
                // jsc needs to support multilevel virtual

                return "field2";
            }
        }

        public override Type ReflectedType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object GetValue(object obj)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
