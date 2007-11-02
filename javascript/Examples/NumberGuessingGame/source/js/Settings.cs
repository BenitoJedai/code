using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.HTML;


namespace NumberGuessingGame.source.js
{

    using NumberGuessingGame.source.js.Controls;

    using Item = Pair<string, EventHandler<IHTMLElement>>;


    [Script]
    static class Settings
    {
        /// <summary>
        /// list of all standalone testable controls
        /// </summary>
        public static Item[] Controls = new Item[] 
            {
                new Item(DemoControl.Alias, (e) => new DemoControl(e))
            };

        /// <summary>
        /// will spawn the controls if they exist in the 
        /// document at page load event
        /// </summary>
        static Settings()
        {
            //Console.LogAssambley(
            //    shared.AssemblyInfo.Current,
            //    ScriptCoreLib.Shared.AssemblyInfo.Current
            //    //ScriptCoreLib.GoogleAPI.Shared.AssemblyInfo.Current,
            //    //ScriptCoreLib.Cards.Shared.AssemblyInfo.Current
            //    );


            Native.Spawn(Controls);
        }

    }
}
