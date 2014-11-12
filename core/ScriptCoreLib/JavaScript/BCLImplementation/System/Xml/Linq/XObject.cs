using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    // http://referencesource.microsoft.com/#System.Xml.Linq/XLinq.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System.Xml.Linq/System.Xml.Linq/XObject.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Xml/Linq/XObject.cs

    [Script(Implements = typeof(XObject))]
    internal class __XObject
    {
        public virtual void InternalAddChanged(EventHandler<XObjectChangeEventArgs> e)
        {

        }
        // X:\jsc.svn\examples\javascript\Test\TestShadowIFrame\TestShadowIFrame\Application.cs
        public event EventHandler<XObjectChangeEventArgs> Changed
        {
            add
            {
                InternalAddChanged(value);
                
            }

            remove
            {
                throw new NotSupportedException();
            }
        }

        internal virtual XElement InternalGetParent()
        {
            throw new NotSupportedException();
        }

        public XElement Parent
        {
            get
            {
                return this.InternalGetParent();
            }
        }
    }
}
