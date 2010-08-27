using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace ScriptCoreLib.JavaScript.Controls
{
    /// <summary>
    /// wrapper for a spawn control, defined by input element
    /// </summary>
	[System.Obsolete("To be moved out of CoreLib")]
    [Script]
    internal class SpawnControlBase
    {
        protected readonly IHTMLInput SpawnControl;

        /// <summary>
        /// returns the decoded value string of the input element 
        /// </summary>
        public string SpawnString
        {
            get
            {
                return Convert.FromBase64String( SpawnControl.value );
            }
        }

        public SpawnControlBase(IHTMLElement spawn)
        {
            SpawnControl = (IHTMLInput)spawn;
        }
    }

  
}
