using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;


namespace CardGames.source.js
{
    [Script]
    class SoundManager
    {
        public List<IHTMLEmbed> History = new List<IHTMLEmbed>();

        public int Size = 8;

        public bool Enabled = true;

        public SoundManager()
        {
            History.ItemRemoved +=
                delegate(IHTMLEmbed e)
                {
                    e.Dispose();
                };

            History.ItemAdded +=
                delegate
                {
                    if (History.Count > Size)
                    {

                        History.RemoveFirst();
                    }
                };
        }

        public void Play(string e)
        {
            if (Enabled)
                History.Add(Native.PlaySound(e));
        }
    }

}
