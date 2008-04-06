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
            var a = new IHTMLTextArea().AttachToDocument();
            var b = new IHTMLTextArea().AttachToDocument();

            a.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            a.style.width = "40em";
            a.style.height = "20em";

            b.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            b.style.width = "40em";
            b.style.height = "20em";
            b.wrap = "off";

            b.readOnly = true;

            a.onchange +=
                delegate
                {
                    try
                    {
                        var w = new StringBuilder();
                        var lines = a.Lines.ToArray();

                        for (int i = 0; i < lines.Length; i += 2)
                        {
                            w.AppendLine("/// <summary>");
                            w.AppendLine("/// " + lines[i + 1]);
                            w.AppendLine("/// <summary>");

                            var x = lines[i].Split(':');

                            var TypeName = x[1].Trim();
                            var FieldName = x[0].Trim();

                            var dict = new Dictionary<string, string>
                            {
                                {"Object", "object"},
                                {"Number", "double"},
                                {"String", "string"}
                            };

                            if (dict.ContainsKey(TypeName))
                                TypeName = dict[TypeName];

                            w.AppendLine("public " + TypeName + " " + FieldName + " { get; set; }");
                            w.AppendLine();
                        }

                        b.value = w.ToString();
                    }
                    catch (Exception ex)
                    {
                        b.value = "error: " + ex.Message;
                    }
                };
        }

        static ConvertASToCS()
        {
            typeof(ConvertASToCS).SpawnTo(i => new ConvertASToCS());
        }

    }

}
