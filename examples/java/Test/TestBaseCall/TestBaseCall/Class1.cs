using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
[assembly: Obfuscation(Feature = "script")]
namespace TestBaseCall
{

    [Script(Implements = typeof(global::System.ComponentModel.Component))]
    public class __Component :
    __MarshalByRefObject,
     __IComponent
    {
        public virtual void Dispose(bool disposing)
        {

        }
    }
    [Script(Implements = typeof(MarshalByRefObject))]
    public class __MarshalByRefObject { }
    [Script(Implements = typeof(IComponent))]
    interface __IComponent { }

    [Script(Implements = typeof(Control))]
    class __Control { }

    [Script(Implements = typeof(global::System.Windows.Forms.ScrollableControl))]
    class __ScrollableControl : __Component { }

    [Script(Implements = typeof(global::System.Windows.Forms.IContainerControl))]
    interface __IContainerControl { }
    [Script(Implements = typeof(global::System.Windows.Forms.ContainerControl))]
    internal class __ContainerControl : __ScrollableControl, __IContainerControl
    {
        // protected override void Dispose(bool disposing);

        public override void Dispose(bool disposing)
        //public void Dispose(bool disposing)
        {

        }

    }

    public class Class1 : ContainerControl
    {
        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}
