using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsWebServiceWithDesigner.Library
{

    [Designer(typeof(XComponentDesigner), typeof(IRootDesigner))]
    public class XDesignedControl : UserControl
    {
        // if not built yet>
        //        // at System.ComponentModel.Design.Serialization.CodeDomDesignerLoader.EnsureDocument(IDesignerSerializationManager manager)
        //at System.ComponentModel.Design.Serialization.CodeDomDesignerLoader.PerformLoad(IDesignerSerializationManager manager)
        //at Microsoft.VisualStudio.Design.Serialization.CodeDom.VSCodeDomDesignerLoader.PerformLoad(IDesignerSerializationManager serializationManager)
        //at Microsoft.VisualStudio.Design.Serialization.CodeDom.VSCodeDomDesignerLoader.DeferredLoadHandler.Microsoft.VisualStudio.TextManager.Interop.IVsTextBufferDataEvents.OnLoadCompleted(Int32 fReload) 

    }
}
