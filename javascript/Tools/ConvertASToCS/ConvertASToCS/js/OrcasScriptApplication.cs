using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using System.Text;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;


namespace ConvertASToCS.js
{
    [Script, ScriptApplicationEntryPoint]
    public class ConvertASToCS
    {
        public ConvertASToCS()
        {
            AddProperties();
        }

      
        private static void AddProperties()
        {
            
            var h = new IHTMLElement(IHTMLElement.HTMLElementEnum.h3).AttachToDocument();

            //var img = new IHTMLImage("http://msdn2.microsoft.com/en-us/library/ks1yssds.pubproperty(en-us,VS.85).gif");

            var htext = new IHTMLSpan("Properties").AttachTo(h);


            var a = new IHTMLTextArea().AttachToDocument();
            var b = new IHTMLTextArea().AttachToDocument();
            //var c = new IHTMLDiv().AttachToDocument();

            htext.onclick +=
                delegate
                {
                    a.ToggleVisible();
                    b.ToggleVisible();
                };

            a.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            a.style.width = "100%";
            a.style.height = "20em";

            /*
            a.wrap = "off";

            a.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.scroll;
            a.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;

            */
            b.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            b.style.width = "100%";
            b.style.height = "20em";

            b.readOnly = true;
            /*
            b.wrap = "off";
            b.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.scroll;
            b.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;
            */

            a.Hide();
            b.Hide();


            a.onchange +=
                delegate
                {
                    try
                    {
                        //c.removeChildren();

                        var w = new StringBuilder();
                        var lines = a.Lines.ToArray();

                        w.AppendLine("#region Properties");

                        for (int i = 0; i < lines.Length; i += 2)
                        {
                            if ((i + 1) < lines.Length)
                            {
                                var Summary = lines[i + 1].Trim();

                                var ReadOnly = "[read-only]";

                                w.AppendLine("/// <summary>");
                                w.AppendLine("/// " + Summary);
                                w.AppendLine("/// </summary>");

                                var x = lines[i].Split(':');

                                var TypeName = FixTypeName(x[1].Trim());

                                var FieldName = x[0].Trim();

                                /*
                                var Image = (IHTMLImage)img.cloneNode(false);

                                Image.style.verticalAlign = "middle";

                                new IHTMLDiv(
                                    Image,
                                    new IHTMLCode(TypeName + " " + FieldName)
                                    ).AttachTo(c);
                                */

                                if (Summary.StartsWith(ReadOnly))
                                    w.AppendLine("public " + TypeName + " " + FieldName + " { get; private set; }");
                                else
                                    w.AppendLine("public " + TypeName + " " + FieldName + " { get; set; }");

                                w.AppendLine();
                            }
                        }

                        w.AppendLine("#endregion");

                        b.value = w.ToString();
                    }
                    catch (Exception ex)
                    {
                        b.value = "error: " + ex.Message;
                    }
                };
        }

        private static string FixTypeName(string TypeName)
        {
            var dict = new Dictionary<string, string>
                                {
                                    {"Object", "object"},
                                    {"Number", "double"},
                                    {"String", "string"},
                                    {"Boolean", "bool"},

                                };

            if (dict.ContainsKey(TypeName))
                TypeName = dict[TypeName];

            return TypeName;
        }

        static ConvertASToCS()
        {
            typeof(ConvertASToCS).SpawnTo(i => new ConvertASToCS());
        }

    }

}
