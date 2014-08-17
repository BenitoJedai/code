using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    // http://referencesource.microsoft.com/#System.Xml.Linq/XLinq.cs
    // https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/System.Xml.Linq/System.Xml.Linq/XObject.cs


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
