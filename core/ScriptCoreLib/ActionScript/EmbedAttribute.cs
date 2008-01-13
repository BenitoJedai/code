using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://www.adobe.com/devnet/flex/quickstart/embedding_assets/
    [Script(IsNative = true)]
    public sealed class EmbedAttribute : Attribute
    {
        public string source;
    }
}
