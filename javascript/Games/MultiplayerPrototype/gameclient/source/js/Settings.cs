using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.HTML;


namespace gameclient.source.js
{
    using gameclient.source.js.Controls;

    using Item = Pair<string, EventHandler<IHTMLElement>>;


    [Script]
    public static class Settings
    {
        /// <summary>
        /// list of all standalone testable controls
        /// </summary>
        public static Item[] Controls = new Item[] 
            {
                new Item(DemoControl.Alias, (e) => new DemoControl(e)),
                new Item(Program.Alias, e => new Program(e))
            };

        /// <summary>
        /// will spawn the controls if they exist in the 
        /// document at page load event
        /// </summary>
        static Settings()
        {
            Native.Spawn(Controls);
        }

    }
}
