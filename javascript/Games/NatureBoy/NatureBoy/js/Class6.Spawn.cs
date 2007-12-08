using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using NatureBoy.js.Zak;
using ScriptCoreLib.Shared.Drawing;

namespace NatureBoy.js
{
    [Script, ScriptApplicationEntryPoint(Format=SerializedDataFormat.xml)]
    public partial class Class6
    {
        public readonly IHTMLDiv Control = new IHTMLDiv();

        public readonly Zak.WorldInfo Data = DefaultData;

        public readonly IHTMLElement AnchorElement;

        public Class6(Zak.WorldInfo _Data, IHTMLElement _AnchorElement)
        {
            this.Data = _Data;

            this.AnchorElement = _AnchorElement;

            this.Initialize();
        }


        // Spawn Support
        static Class6()
        {
            typeof(Class6).SpawnTo<Zak.WorldInfo>(Zak.Settings.KnownTypes,
                (i, e) =>
                {
                    try
                    {
                        new Class6(i, e);
                    }
                    catch (Exception exc)
                    {
                        "pre".AttachToDocument().innerText = "error: " + exc.Message;
                    }
                }
            );
        }
    }

}
