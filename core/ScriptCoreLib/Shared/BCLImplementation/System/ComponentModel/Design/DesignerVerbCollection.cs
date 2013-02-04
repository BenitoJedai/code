using ScriptCoreLib.Shared.BCLImplementation.System.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel.Design
{
    [Script(Implements = typeof(global::System.ComponentModel.Design.DesignerVerbCollection))]
    public class __DesignerVerbCollection : __CollectionBase
    {
        public __DesignerVerbCollection() : this(null)
        {

        }

        public __DesignerVerbCollection(DesignerVerb[] value)
        {
        }

    }
}
