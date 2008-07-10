using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.utils
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/utils/IExternalizable.html
    [Script(IsNative = true)]
    public interface IExternalizable
    {
        /// <summary>
        /// A class implements this method to decode itself from a data stream by calling the methods of the IDataInput interface.
        /// </summary>
        void readExternal(IDataInput input);
        

        /// <summary>
        /// A class implements this method to encode itself for a data stream by calling the methods of the IDataOutput interface.
        /// </summary>
        void writeExternal(IDataOutput output);
        
    }
}
