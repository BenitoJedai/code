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
using System.Collections.Generic;
using System.ComponentModel;


namespace CardGames.source.js
{
    [Script]
    class SoundManager
    {
        public BindingList<IHTMLEmbed> History = new BindingList<IHTMLEmbed>();

        public int Size = 8;

        public bool Enabled = true;

        public SoundManager()
        {
            History.ListChanged +=
                (sender, args) =>
                {
                    if (args.ListChangedType == ListChangedType.ItemDeleted)
                    {
                        History[args.NewIndex].Dispose();

                    }


                    if (args.ListChangedType == ListChangedType.ItemAdded)
                    {
                        if (History.Count > Size)
                        {

                            History.RemoveFirst();
                        }
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
