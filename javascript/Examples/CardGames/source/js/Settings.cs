using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.HTML;


namespace CardGames.source.js
{

    using CardGames.source.js.Controls;

    using Item = Pair<string, EventHandler<IHTMLElement>>;

    
    [Script]
    static class Settings
    {
        /// <summary>
        /// list of all standalone testable controls
        /// </summary>
        public static Item[] Controls = new Item[] 
            {
                new Item(DemoControl.Alias, (e) => new DemoControl(e)),
                new Item(BlackJack.Alias, (e) => new BlackJack(e)),
                new Item(Solitare.Alias, (e) => new Solitare(e)),
                new Item(FreeCell.Alias, (e) => new FreeCell(e)),
                new Item(SoundTest.Alias, (e) => new SoundTest(e)),
                new Item(Spider.Alias, (e) => new Spider(e))
            };

        /// <summary>
        /// will spawn the controls if they exist in the 
        /// document at page load event
        /// </summary>
        static Settings()
        {
            //Console.LogAssambley(
            //    shared.AssemblyInfo.Current,
            //    ScriptCoreLib.Shared.AssemblyInfo.Current,
            //    ScriptCoreLib.Shared.Cards.AssemblyInfo.Current
            //    );


            Native.Spawn(Controls);
        }

    }
}
