using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using System.Text;
using System;
using System.Linq;
using System.Collections.Specialized;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ConvertASToCS.js.Any;


namespace ConvertASToCS.js
{
    [Script, ScriptApplicationEntryPoint]
    public partial class ConvertASToCS : ConverterBase
    {
        IHTMLInput DeclaringType = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.text);

        public ConvertASToCS()
        {
            Native.Document.title = "ConvertASToCS";

            Native.Document.body.style.padding = "0";
            Native.Document.body.style.margin = "0";


            var cookie = new Cookie("DeclaringType").BindTo(DeclaringType);

            #region Title
            var MyTitleText = new IHTMLDiv("This tool allows you to copy various parts of flash doc html file in and generate C# headers.");
            MyTitleText.style.paddingTop = "12px";
            MyTitleText.style.paddingLeft = "15px";
            MyTitleText.style.fontFamily = IStyle.FontFamilyEnum.Tahoma;
            MyTitleText.style.fontSize = "13px";


            var MyTitle = new IHTMLDiv(MyTitleText);
            MyTitle.style.background = "url(" + Assets.Path + "titleTableTop.jpg) repeat-x";
            MyTitle.style.height = "44px";
            MyTitle.AttachToDocument();

            var MyTitleMiddleTextFloat = new IHTMLDiv();

            MyTitleMiddleTextFloat.style.paddingTop = "3px";
            MyTitleMiddleTextFloat.style.Float = IStyle.FloatEnum.right;

            var MyTitleMiddleText = new IHTMLDiv(new IHTMLLabel("DeclaringType: ", DeclaringType), DeclaringType);
            MyTitleMiddleText.style.paddingTop = "3px";
            MyTitleMiddleText.style.paddingLeft = "15px";
            MyTitleMiddleText.style.fontFamily = IStyle.FontFamilyEnum.Tahoma;
            MyTitleMiddleText.style.fontSize = "20px";



            var MyTitleMiddle = new IHTMLDiv(MyTitleMiddleTextFloat, MyTitleMiddleText);
            MyTitleMiddle.style.background = "url(" + Assets.Path + "titleTableMiddle.jpg) repeat-x";
            MyTitleMiddle.style.height = "31px";
            MyTitleMiddle.AttachToDocument();

            var MyTitleShadow = new IHTMLDiv("");
            MyTitleShadow.style.background = "url(" + Assets.Path + "titleTableBottom.jpg) repeat-x";
            MyTitleShadow.style.height = "5px";
            MyTitleShadow.AttachToDocument();
            #endregion



            DocumentBody = new IHTMLDiv().AttachToDocument();
            DocumentBody.style.padding = "1em";
            DocumentBody.style.backgroundImage = "url(assets/ConvertASToCS/flash_logo.png)";
            DocumentBody.style.backgroundPosition = "right top";
            DocumentBody.style.backgroundRepeat = "no-repeat";


            a = new IHTMLTextArea().AttachTo(DocumentBody);
            a.style.backgroundColor = Color.Transparent;
            a.style.border = "1px solid gray";


            Func<IHTMLImage, string, IHTMLAnchor> CreateButton =
                (img, text) =>
                {
                    img.style.border = "0px";

                    var htext = new IHTMLAnchor("#", img, new IHTMLSpan(text)).AttachTo(MyTitleMiddleTextFloat);

                    htext.onclick += e => e.PreventDefault();
                    htext.style.margin = "1em";

                    return htext;
                };


            AddEvents(CreateButton((Assets.Path + "ak590dyt.pubevent(en-US,VS.80).gif"), "Events"));
            AddConstants(CreateButton((Assets.Path + "ak590dyt.pubproperty(en-US,VS.80).gif"), "Constants"));
            AddProperties(CreateButton((Assets.Path + "ak590dyt.pubproperty(en-US,VS.80).gif"), "Properties"));
            AddMethods(CreateButton((Assets.Path + "deshae98.pubmethod(en-us,VS.90).gif"), "Methods"));
            AddAny(CreateButton((Assets.Path + "deshae98.pubmethod(en-us,VS.90).gif"), "Any"));
            AddProxy(CreateButton((Assets.Path + "deshae98.pubmethod(en-us,VS.90).gif"), "Proxy"));
        }



        readonly IHTMLDiv DocumentBody;

        readonly IHTMLTextArea a;

        private void AddAny(IHTMLAnchor htext)
        {
            var content = new IHTMLDiv().AttachTo(DocumentBody);

            // content.Hide();

            htext.onclick +=
              delegate
              {
                  content.ToggleVisible();
              };

            content.ToggleVisible();

            var LastUpdate = new IHTMLDiv("Not updated yet").AttachTo(content);

            var pre = new IHTMLElement(IHTMLElement.HTMLElementEnum.pre).AttachTo(content);

            Action update =
                delegate
                {
                    LastUpdate.innerText = "Last update: " + DateTime.Now;

                    pre.removeChildren();

                    RenderAnyTo(new Any.ReflectionProvider(a.value), pre);


                };


            a.onchange += delegate { update(); };
        }

        private void AddProxy(IHTMLAnchor htext)
        {
            var content = new IHTMLDiv().AttachTo(DocumentBody);

            // content.Hide();

            htext.onclick +=
              delegate
              {
                  content.ToggleVisible();
              };

            var LastUpdate = new IHTMLDiv("Not updated yet").AttachTo(content);

            var pre = new IHTMLElement(IHTMLElement.HTMLElementEnum.pre).AttachTo(content);

            Action update =
                delegate
                {
                    LastUpdate.innerText = "Last update: " + DateTime.Now;

                    pre.removeChildren();

                    System.Console.WriteLine("Any.ProxyProvider");

                    RenderProxyTo(new Any.ProxyProvider(a.value), pre);


                };


            a.onchange += delegate { update(); };
        }





        private static void RenderAnyTo(ReflectionProvider r, IHTMLElement pre)
        {
            Func<bool> DelegatesParams = () => false;
            Func<bool> IsInterface = () => false;
            Func<bool> IsField = () => false;

            IHTMLElement Target = pre;

            Func<Color, Action<string>> ToColorWrite =
                color =>
                    text =>
                    {
                        var s = new IHTMLSpan { innerText = text };

                        s.style.color = color;
                        s.AttachTo(Target);
                    };

            Func<string, IHTMLSpan> Write = text => new IHTMLSpan(text).AttachTo(Target);

            Action WriteLine = () => Write("\r\n");
            Action WriteSpace = () => Write(" ");

            #region Write<Color>
            Action<string> WriteBlue = ToColorWrite(Color.Blue);
            Action<string> WriteBlack = ToColorWrite(Color.Black);
            Action<string> WriteGray = ToColorWrite(Color.FromRGB(0x80, 0x80, 0x80));
            Action<string> WriteCyan = ToColorWrite(Color.FromRGB(0, 0x80, 0x80));
            Action<string> WriteGreen = ToColorWrite(Color.FromRGB(0, 0x80, 0));
            #endregion

            int Indent = 1;

            Action WriteIdent = () => Write(new string(' ', 4 * Indent));

            Func<string, IDisposable> Region =
                text =>
                {
                    WriteIdent();
                    WriteBlue("#region");
                    WriteSpace();
                    var Collapsible = Write(text);
                    WriteLine();

                    var PreviousTarget = Target;
                    var CurrentTarget = new IHTMLElement(IHTMLElement.HTMLElementEnum.pre).AttachTo(PreviousTarget); ;

                    Collapsible.style.cursor = IStyle.CursorEnum.pointer;
                    Collapsible.onclick +=
                        delegate
                        {
                            CurrentTarget.ToggleVisible();
                        };

                    Target = CurrentTarget;

                    return new Disposable(
                        delegate
                        {


                            WriteIdent();
                            WriteBlue("#endregion");
                            WriteLine();

                            Target = PreviousTarget;
                        }
                    );
                };


            Func<IDisposable> CodeBlock =
                delegate
                {
                    WriteIdent();
                    Write("{");
                    WriteLine();

                    Indent++;

                    return new Disposable(
                        delegate
                        {
                            Indent--;

                            WriteIdent();
                            Write("}");
                            WriteLine();
                        }
                    );
                };

            Action<string> WriteSummary =
                text =>
                {
                    WriteIdent();
                    WriteGray("/// <summary>");
                    WriteLine();

                    WriteIdent();
                    WriteGray("/// ");
                    WriteGreen(text);
                    WriteLine();

                    WriteIdent();
                    WriteGray("/// </summary>");
                    WriteLine();
                };

            Action<string> WriteTypeName =
                text =>
                {
                    var z = FixTypeName(text.Trim());

                    if (CSharpKeywords.Contains(z))
                        WriteBlue(z);
                    else
                        WriteCyan(z);
                };

            Action<string, string> WriteVariableDefinition =
                (TypeName, VariableName) =>
                {
                    WriteTypeName(TypeName);
                    WriteSpace();
                    Write(FixVariableName(VariableName));
                };

            WriteIdent();
            WriteBlue("namespace");
            WriteSpace();
            Write(r.PackageName);
            WriteLine();

            using (CodeBlock())
            {
                WriteIdent();
                Write("[");
                WriteCyan("Script");
                Write("(");
                Write("IsNative = ");
                WriteBlue("true");
                Write(")");
                Write("]");
                WriteLine();

                WriteIdent();

                WriteBlue("public");
                WriteSpace();

                if (r.IsSealed)
                {
                    WriteBlue("sealed");
                    WriteSpace();
                }


                WriteBlue("class");
                WriteSpace();
                WriteCyan(r.TypeName);
                WriteLine();

                using (CodeBlock())
                {
                    #region Constants
                    using (Region("Constants"))
                        foreach (var p in r.Constants)
                        {

                            WriteSummary(p.Summary);

                            WriteIdent();

                            if (string.IsNullOrEmpty(p.Value) && p.IsAirOnly)
                            {
                                WriteGreen("// " + p.Name + " constant was ommited due to no given value");
                                WriteLine();
                                continue;
                            }

                            if (p.Value == "\"?\"")
                            {
                                WriteGreen("// " + p.Name + " constant was ommited due to no string given value");
                                WriteLine();
                                continue;
                            }


                            WriteBlue("public");
                            WriteSpace();

                            if (p.IsAirOnly)
                            {
                                WriteBlue("const");
                                WriteSpace();
                            }
                            else
                            {
                                WriteBlue("static");
                                WriteSpace();
                                WriteBlue("readonly");
                                WriteSpace();
                            }

                            WriteVariableDefinition(p.Type, p.Name);

                            if (!string.IsNullOrEmpty(p.Value))
                            {
                                WriteSpace();
                                Write("=");
                                WriteSpace();
                                Write(p.Value);
                            }

                            Write(";");

                            WriteLine();
                            WriteLine();
                        }
                    #endregion


                    #region Properties
                    using (Region("Properties"))
                        foreach (var p in r.Properties)
                        {
                            if (p.IsInherited)
                                continue;

                            WriteSummary(p.Summary);

                            WriteIdent();

                            WriteBlue("public");
                            WriteSpace();

                            if (p.IsStatic)
                            {
                                WriteBlue("static");
                                WriteSpace();
                            }


                            WriteVariableDefinition(p.PropertyType, p.PropertyName);

                            if (IsField())
                                Write(";");
                            else
                            {
                                WriteSpace();
                                Write("{");
                                WriteSpace();
                                WriteBlue("get");
                                Write(";");
                                WriteSpace();

                                if (p.IsReadOnly)
                                {
                                    WriteBlue("private");
                                    WriteSpace();
                                }

                                WriteBlue("set");
                                Write(";");
                                WriteSpace();
                                Write("}");

                            }


                            WriteLine();
                            WriteLine();
                        }
                    #endregion

                    #region Constructors
                    using (Region("Constructors"))
                        foreach (var p in r.GetInstanceConstructors())
                            foreach (var v in p.ParametersInfo.Variations)
                            {
                                WriteSummary(p.Summary);

                                WriteIdent();

                                WriteBlue("public");
                                WriteSpace();

                                Write(p.Name);
                                Write("(");

                                for (int k = 0; k < v.Parameters.Length; k++)
                                {
                                    if (k > 0)
                                    {
                                        Write(",");
                                        WriteSpace();
                                    }

                                    WriteVariableDefinition(v.Parameters[k].TypeName, v.Parameters[k].Name);
                                }

                                Write(")");

                                if (DelegatesParams())
                                {
                                    WriteSpace();
                                    Write(":");
                                    WriteSpace();
                                    WriteBlue("base");
                                    Write("(" + v.NamesToString() + ")");
                                }


                                WriteLine();


                                using (CodeBlock()) { }

                                WriteLine();
                            }
                    #endregion

                    #region Methods
                    using (Region("Methods"))
                        foreach (var p in r.GetInstanceMethods())
                            foreach (var v in p.ParametersInfo.Variations)
                            {
                                WriteSummary(p.Summary);

                                WriteIdent();

                                if (IsInterface())
                                {

                                }
                                else
                                {
                                    WriteBlue("public");
                                    WriteSpace();

                                    if (p.IsStatic)
                                    {
                                        WriteBlue("static");
                                        WriteSpace();
                                    }
                                }


                                WriteVariableDefinition(p.ReturnType, p.Name);

                                Write("(");

                                for (int k = 0; k < v.Parameters.Length; k++)
                                {
                                    if (k > 0)
                                    {
                                        Write(",");
                                        WriteSpace();
                                    }

                                    WriteVariableDefinition(v.Parameters[k].TypeName, v.Parameters[k].Name);
                                }

                                Write(")");

                                if (IsInterface())
                                {
                                    Write(";");
                                    WriteLine();
                                }
                                else
                                {

                                    WriteLine();

                                    using (CodeBlock())
                                    {
                                        if (!p.ReturnTypeVoid)
                                        {
                                            WriteIdent();
                                            WriteBlue("return");
                                            WriteSpace();
                                            WriteBlue("default");
                                            Write("(");
                                            WriteTypeName(p.ReturnType);
                                            Write(")");
                                            Write(";");
                                            WriteLine();
                                        }
                                    }
                                }

                                WriteLine();
                            }
                    #endregion

                }
            }

        }

        private void AddMethods(IHTMLElement htext)
        {
            var content = new IHTMLDiv().AttachTo(DocumentBody);
            content.Hide();

            var IsInterface = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox);
            new IHTMLDiv(
                new IHTMLLabel("is an interface: ", IsInterface), IsInterface
            ).AttachTo(content);

            var DelegatesParams = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox);
            new IHTMLDiv(
                new IHTMLLabel("delegates parameters to base constructor: ", DelegatesParams), DelegatesParams
            ).AttachTo(content);

            var update = default(Action);

            // var a = new IHTMLTextArea().AttachTo(content);

            IHTMLButton.Create(
                "Example code",
                delegate
                {
                    a.value =
@"addEventListener(type:String, listener:Function, useCapture:Boolean = false, priority:int = 0, useWeakReference:Boolean = false):void
Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
";

                    update();
                }
            ).AttachTo(content);



            var b = new IHTMLTextArea().AttachTo(content);

            htext.onclick +=
                delegate
                {
                    content.ToggleVisible();
                };

            a.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            a.style.width = "100%";
            a.style.height = "20em";


            b.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            b.style.width = "100%";
            b.style.height = "20em";

            b.readOnly = true;



            Action update_output =
                delegate
                {
                    var w = new StringBuilder();
                    var w2 = new StringBuilder();

                    var lines = a.Lines.ToArray();

                    w.AppendLine("#region Methods");
                    w2.AppendLine("#region Constructors");

                    for (int i = 0; i < lines.Length; i += 3)
                    {
                        if ((i + 1) < lines.Length)
                        {
                            var StaticKeyword = "[static]";

                            var Summary = lines[i + 1].Trim();
                            var MethodSig = lines[i].Trim();


                            if (!MethodSig.Contains("AIR-only"))
                            {
                                var q0 = MethodSig.Split(')');
                                var q1 = q0[0].Split('(');

                                var MethodName = FixVariableName(q1[0].Trim());
                                var MethodParameters = new MethodParametersInfo(q1[1].Trim());

                                var MethodReturnType = "";

                                if (q0[1].StartsWith(":"))
                                    MethodReturnType = FixTypeName(q0[1].Substring(1).Trim());



                                var IsConstructor = string.IsNullOrEmpty(MethodReturnType);

                                foreach (var v in MethodParameters.Variations)
                                {
                                    if (IsConstructor)
                                    {
                                        w2.AppendLine("/// <summary>");
                                        w2.AppendLine("/// " + Summary);
                                        w2.AppendLine("/// </summary>");

                                        if (DelegatesParams.@checked)
                                            w2.AppendLine("public " + MethodName + "(" + v + ") : base(" + v.NamesToString() + ")");
                                        else
                                            w2.AppendLine("public " + MethodName + "(" + v + ")");

                                        w2.AppendLine("{");
                                        w2.AppendLine("}");
                                        w2.AppendLine();
                                    }
                                    else
                                    {
                                        if (v.Parameters.Length == 0 && MethodName == "toString")
                                        {

                                        }
                                        else
                                        {
                                            w.AppendLine("/// <summary>");
                                            w.AppendLine("/// " + Summary);
                                            w.AppendLine("/// </summary>");

                                            var StaticModifier = Summary.Contains(StaticKeyword) ? "static " : "";

                                            if (IsInterface.@checked)
                                            {
                                                w.AppendLine(MethodReturnType + " " + MethodName + "(" + v + ");");

                                            }
                                            else
                                            {
                                                w.AppendLine("public " + StaticModifier + MethodReturnType + " " + MethodName + "(" + v + ")");
                                                w.AppendLine("{");

                                                if (MethodReturnType != "void")
                                                    w.AppendLine("  return default(" + MethodReturnType + ");");

                                                w.AppendLine("}");
                                            }
                                            w.AppendLine();

                                        }
                                    }
                                }




                            }


                        }
                    }

                    w.AppendLine("#endregion");
                    w2.AppendLine("#endregion");

                    if (!IsInterface.@checked)
                    {
                        w.AppendLine();
                        w.Append(w2.ToString());
                    }

                    b.value = w.ToString();
                };

            update =
                delegate
                {


                    try
                    {

                        update_output();
                        htext.style.color = Color.Blue;
                    }
                    catch (Exception ex)
                    {
                        htext.style.color = Color.Red;
                        b.value = "error: " + ex.Message;
                    }
                };

            IsInterface.onchange += delegate { update(); };
            DelegatesParams.onchange += delegate { update(); };

            a.onchange +=
                delegate
                {
                    update();
                };
        }

        [Script]
        public class MethodParametersInfo
        {

            [Script]
            public class ParamInfo
            {
                public string Name;
                public string TypeName;
                public string DefaultValue;

                // http://bartdesmet.net/blogs/bart/archive/2006/09/28/4473.aspx
                // http://www.sephiroth.it/weblog/archives/2006/06/actionscript_3_rest_parameter.php
                public bool IsRestParameter;

                public bool HasDefaultValue
                {
                    get { return !string.IsNullOrEmpty(DefaultValue); }
                }
            }

            public readonly ParamInfo[] Parameters;

            public MethodParametersInfo(IEnumerable<ParamInfo> e)
            {
                Parameters = e.ToArray();
            }

            public MethodParametersInfo(string e)
            {

                Parameters = e.Split(',').Select(i => i.Trim()).Where(i => !string.IsNullOrEmpty(i)).Select(
                    text =>
                    {
                        if (text.StartsWith("..."))
                        {
                            return new ParamInfo
                            {
                                IsRestParameter = true,
                                Name = text.Substring(3).Trim(),
                                TypeName = "object"
                            };
                        }

                        var q = text.Split('=');
                        var z = q[0].Split(':');

                        if (q.Length == 1)
                            return new ParamInfo
                            {
                                Name = z[0].Trim(),
                                TypeName = FixTypeName(z[1].Trim())
                            };

                        return new ParamInfo
                        {
                            Name = z[0].Trim(),
                            TypeName = FixTypeName(z[1].Trim()),
                            DefaultValue = q[1].Trim()
                        };
                    }
                ).ToArray();
            }

            public MethodParametersInfo DropLastParameter()
            {
                if (this.Parameters.Length == 0)
                    return null;

                var p = new List<ParamInfo>();

                for (int i = 0; i < this.Parameters.Length - 1; i++)
                {
                    p.Add(this.Parameters[i]);
                }

                return new MethodParametersInfo(p);
            }

            public IEnumerable<MethodParametersInfo> Variations
            {
                get
                {
                    if (Parameters.Length == 0)
                    {
                        return new[] { this }.AsEnumerable();
                    }


                    var v = new[] { 
                    
                        new MethodParametersInfo
                        (
                            from p in Parameters
                            select new ParamInfo
                            {
                                IsRestParameter = p.IsRestParameter,
                                Name = p.Name,
                                TypeName = p.TypeName,
                                DefaultValue = null
                            }
                         )

                     }.AsEnumerable();

                    var last = this.Parameters.Last();

                    if (last.HasDefaultValue || last.IsRestParameter)
                    {
                        // solid this and all below

                        v = v.Concat(DropLastParameter().Variations);
                    }

                    return v;

                }
            }

            public override string ToString()
            {
                if (Parameters.Length == 0)
                    return "";

                var w = new StringBuilder();

                for (int i = 0; i < Parameters.Length; i++)
                {
                    var p = Parameters[i];

                    if (i > 0)
                        w.Append(", ");


                    if (p.IsRestParameter)
                        w.Append("/* params */ " + p.TypeName + " " + p.Name);
                    else if (string.IsNullOrEmpty(p.DefaultValue))
                        w.Append(p.TypeName + " " + FixVariableName(p.Name));
                    else
                        w.Append(p.TypeName + " " + FixVariableName(p.Name) + " = " + p.DefaultValue);
                }



                return w.ToString();
            }

            public string NamesToString()
            {
                return Parameters.Aggregate("",
                    (v, i) =>
                    {
                        if (!string.IsNullOrEmpty(v))
                            v += ", ";

                        v += i.Name;

                        return v;
                    }
                );

            }
        }

        private void AddEvents(IHTMLElement htext)
        {
            var content = new IHTMLDiv().AttachTo(DocumentBody);
            content.Hide();

            var IsCamelCaseNames = "Use CamelCase on event names ".ToCheckBox().AttachToWithLabel(content);

            IHTMLInput NamePrefix = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.text);

            new IHTMLDiv(
                new IHTMLLabel("Event name prefix: ", NamePrefix), NamePrefix
                ).AttachTo(content);

            var update = default(Action);

            // var a = new IHTMLTextArea().AttachTo(content);
            IHTMLButton.Create(
                "Example code",
                delegate
                {
                    a.value =
@"added
	Dispatched when a display object is added to the display list.	
addedToStage
	Dispatched when a display object is added to the on stage display list, either directly or through the addition of a sub tree in which the display object is contained.	
enterFrame
	Dispatched when the playhead is entering a new frame.	
removed
	Dispatched when a display object is about to be removed from the display list.	
removedFromStage
	Dispatched when a display object is about to be removed from the display list, either directly or through the removal of a sub tree in which the display object is contained.	
render
	Dispatched when the display list is about to be updated and rendered.";

                    update();
                }
            ).AttachTo(content);

            // too big
            // var cookie = new Cookie("ExampleEvents").BindTo(a);


            var z = new IHTMLTable().AttachTo(content);
            var zb = z.AddBody();

            var b = new IHTMLTextArea().AttachTo(content);

            htext.onclick +=
                delegate
                {
                    content.ToggleVisible();
                };

            a.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            a.style.width = "100%";
            a.style.height = "20em";


            b.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            b.style.width = "100%";
            b.style.height = "20em";

            b.readOnly = true;

            var dict = new
            {
                EventType = new Dictionary<string, string>(),
                EventCodeName = new Dictionary<string, string>(),
            };

            Action<Action<string>> update_output =
                handler =>
                {
                    var w = new StringBuilder();
                    var w2 = new StringBuilder();

                    var lines = a.Lines.ToArray();

                    w.AppendLine("#region Events");

                    w2.AppendLine("#region Implementation for methods marked with [Script(NotImplementedHere = true)]");

                    var DeclaringTypeName = DeclaringType.value;

                    for (int i = 0; i < lines.Length; i += 2)
                    {
                        if ((i + 1) < lines.Length)
                        {
                            var Summary = lines[i + 1].Trim();
                            var EventName = lines[i].Trim();

                            if (EventName.IndexOf(":") > -1)
                                EventName = EventName.Substring(0, EventName.IndexOf(":")).Trim();

                            if (EventName.ContainsAny("(", "#"))
                                throw new Exception("Invalid Event Name");

                            if (!EventName.Contains("AIR-only"))
                            {
                                //var ReadOnly = "[read-only]";

                                w.AppendLine("/// <summary>");
                                w.AppendLine("/// " + Summary);
                                w.AppendLine("/// </summary>");



                                if (handler != null)
                                    handler(EventName);


                                var EventType = dict.EventType[EventName];
                                var EventCodeName = dict.EventCodeName[EventName];

                                var FriendlyEventName = EventName;

                                if (IsCamelCaseNames.@checked)
                                    FriendlyEventName = FriendlyEventName.ToCamelCase();

                                if (!string.IsNullOrEmpty(NamePrefix.value))
                                    FriendlyEventName = NamePrefix.value + FriendlyEventName;

                                if (FriendlyEventName == "")
                                    throw new Exception("Friendly name is empty.");

                                w.AppendLine("[method: Script(NotImplementedHere = true)]");
                                w.AppendLine("public event Action<" + EventType + "> " + FriendlyEventName + ";");

                                w.AppendLine();



                                w2.AppendLine("#region " + FriendlyEventName);
                                w2.AppendLine("public static void add_" + FriendlyEventName + "(" + DeclaringTypeName + " that, Action<" + EventType + "> value)");
                                w2.AppendLine("{");
                                w2.AppendLine(" CommonExtensions.CombineDelegate(that, value, " + EventType + "." + EventCodeName + ");");
                                w2.AppendLine("}");
                                w2.AppendLine();
                                w2.AppendLine("public static void remove_" + FriendlyEventName + "(" + DeclaringTypeName + " that, Action<" + EventType + "> value)");
                                w2.AppendLine("{");
                                w2.AppendLine(" CommonExtensions.RemoveDelegate(that, value, " + EventType + "." + EventCodeName + ");");
                                w2.AppendLine("}");
                                w2.AppendLine("#endregion");
                                w2.AppendLine();
                            }

                        }
                    }

                    w.AppendLine("#endregion");
                    w2.AppendLine("#endregion");

                    w.AppendLine();

                    w.Append(w2.ToString());

                    b.value = w.ToString();
                };


            update =
                delegate
                {
                    try
                    {
                        zb.removeChildren();

                        // propagate new values to next buttons


                        var PChange = new List<Func<string, string, string, bool>>();

                        update_output(
                            EventName =>
                            {
                                var row = zb.AddRow();

                                row.AddColumn(EventName);

                                var EventType = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.text);

                                if (!dict.EventType.ContainsKey(EventName))
                                    dict.EventType[EventName] = "Event";

                                var EventTypeOld = dict.EventType[EventName];
                                EventType.value = dict.EventType[EventName];

                                var CPChangeOffset = PChange.Count;

                                Action<string, string, string> CPChange =
                                    (_EventName, _Old, _New) =>
                                    {
                                        for (int i = CPChangeOffset + 1; i < PChange.Count; i++)
                                        {
                                            if (!PChange[i](_EventName, _Old, _New))
                                                break;
                                        }
                                    };

                                PChange.Add(
                                    (_Event, _Old, _New) =>
                                    {
                                        // Console.WriteLine(EventName + " detected a change from " + _Event);

                                        if (EventType.value == _Old)
                                        {
                                            EventType.value = _New;
                                            dict.EventType[EventName] = EventType.value;
                                            return true;
                                        }

                                        return false;
                                    }
                                );

                                EventType.onchange +=
                                    delegate
                                    {
                                        if (CPChange != null)
                                            CPChange(EventName, dict.EventType[EventName], EventType.value);

                                        dict.EventType[EventName] = EventType.value;
                                        update_output(null);

                                    };

                                row.AddColumn(EventType);

                                var EventCodeName = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.text);

                                if (!dict.EventCodeName.ContainsKey(EventName))
                                    dict.EventCodeName[EventName] = EventName.ToCamelCaseUpper();

                                EventCodeName.value = dict.EventCodeName[EventName];

                                EventCodeName.onchange +=
                                    delegate
                                    {
                                        dict.EventCodeName[EventName] = EventCodeName.value;
                                        update_output(null);
                                    };

                                row.AddColumn(EventCodeName);
                            }
                        );

                        htext.style.color = Color.Blue;
                    }
                    catch (Exception ex)
                    {
                        htext.style.color = Color.Red;
                        b.value = "error: " + ex.Message;
                    }
                };


            DeclaringType.onchange +=
               delegate
               {
                   update_output(null);
               };

            NamePrefix.onchange += delegate { update(); };
            IsCamelCaseNames.onchange += delegate { update(); };
            a.onchange += delegate { update(); };
        }


        private void AddConstants(IHTMLElement htext)
        {
            var content = new IHTMLDiv().AttachTo(DocumentBody);
            content.Hide();

            var IsEnum = "Declare constants as enums ".ToCheckBox().AttachToWithLabel(content);


            var update = default(Action);


            // var a = new IHTMLTextArea().AttachTo(content);
            IHTMLButton.Create(
                "Example code",
                delegate
                {
                    a.value =
@"ACTIVATE : String = ""activate""
[static] The Event.ACTIVATE constant defines the value of the type property of an activate event object.
	ADDED : String = ""added""
[static] The Event.ADDED constant defines the value of the type property of an added event object.
	ADDED_TO_STAGE : String = ""addedToStage""
[static] The Event.ADDED_TO_STAGE constant defines the value of the type property of an addedToStage event object.
	CANCEL : String = ""cancel""
[static] The Event.CANCEL constant defines the value of the type property of a cancel event object.
	CHANGE : String = ""change""
[static] The Event.CHANGE constant defines the value of the type property of a change event object.
	CLOSE : String = ""close""
[static] The Event.CLOSE constant defines the value of the type property of a close event object.
	AIR-only CLOSING : String = ""closing""
[static] The Event.CLOSING constant defines the value of the type property of a closing event object.
	COMPLETE : String = ""complete""
[static] The Event.COMPLETE constant defines the value of the type property of a complete event object.
	CONNECT : String = ""connect""
[static] The Event.CONNECT constant defines the value of the type property of a connect event object.
	DEACTIVATE : String = ""deactivate""
[static] The Event.DEACTIVATE constant defines the value of the type property of a deactivate event object.
	AIR-only DISPLAYING : String = ""displaying""
[static] Defines the value of the type property of a displaying event object.
	ENTER_FRAME : String = ""enterFrame""
[static] The Event.ENTER_FRAME constant defines the value of the type property of an enterFrame event object.
	AIR-only EXITING : String = ""exiting""
[static] The Event.EXITING constant defines the value of the type property of an exiting event object.
	FULLSCREEN : String = ""fullScreen""
[static] The Event.FULL_SCREEN constant defines the value of the type property of a fullScreen event object.
	AIR-only HTML_BOUNDS_CHANGE : String = ""htmlBoundsChange""
[static] The Event.HTML_BOUNDS_CHANGE constant defines the value of the type property of an htmlBoundsChange event object.
	AIR-only HTML_DOM_INITIALIZE : String = ""htmlDOMInitialize""
[static] The Event.HTML_DOM_INITIALIZE constant defines the value of the type property of an htmlDOMInitialize event object.
	AIR-only HTML_RENDER : String = ""htmlRender""
[static] The Event.HTML_RENDER constant defines the value of the type property of an htmlRender event object.
	ID3 : String = ""id3""
[static] The Event.ID3 constant defines the value of the type property of an id3 event object.
	INIT : String = ""init""
[static] The Event.INIT constant defines the value of the type property of an init event object.
	AIR-only LOCATION_CHANGE : String = ""locationChange""
[static] The Event.LOCATION_CHANGE constant defines the value of the type property of a locationChange event object.
	MOUSE_LEAVE : String = ""mouseLeave""
[static] The Event.MOUSE_LEAVE constant defines the value of the type property of a mouseLeave event object.
	AIR-only NETWORK_CHANGE : String = ""networkChange""
[static] The Event.NETWORK_CHANGE constant defines the value of the type property of a networkChange event object.
	OPEN : String = ""open""
[static] The Event.OPEN constant defines the value of the type property of an open event object.
	REMOVED : String = ""removed""
[static] The Event.REMOVED constant defines the value of the type property of a removed event object.
	REMOVED_FROM_STAGE : String = ""removedFromStage""
[static] The Event.REMOVED_FROM_STAGE constant defines the value of the type property of a removedFromStage event object.
	RENDER : String = ""render""
[static] The Event.RENDER constant defines the value of the type property of a render event object.
	RESIZE : String = ""resize""
[static] The Event.RESIZE constant defines the value of the type property of a resize event object.
	SCROLL : String = ""scroll""
[static] The Event.SCROLL constant defines the value of the type property of a scroll event object.
	SELECT : String = ""select""
[static] The Event.SELECT constant defines the value of the type property of a select event object.
	SOUND_COMPLETE : String = ""soundComplete""
[static] The Event.SOUND_COMPLETE constant defines the value of the type property of a soundComplete event object.
	TAB_CHILDREN_CHANGE : String = ""tabChildrenChange""
[static] The Event.TAB_CHILDREN_CHANGE constant defines the value of the type property of a tabChildrenChange event object.
	TAB_ENABLED_CHANGE : String = ""tabEnabledChange""
[static] The Event.TAB_ENABLED_CHANGE constant defines the value of the type property of a tabEnabledChange event object.
	TAB_INDEX_CHANGE : String = ""tabIndexChange""
[static] The Event.TAB_INDEX_CHANGE constant defines the value of the type property of a tabIndexChange event object.
	UNLOAD : String = ""unload""
[static] The Event.UNLOAD constant defines the value of the type property of an unload event object.
	AIR-only USER_IDLE : String = ""userIdle""
[static] The Event.USER_IDLE constant defines the value of the type property of a userIdle event object.
	AIR-only USER_PRESENT : String = ""userPresent""
[static] The Event.USER_PRESENT constant defines the value of the type property of a userPresent event object.
";

                    update();
                }
            ).AttachTo(content);


            var z = new IHTMLTable().AttachTo(content);
            var zb = z.AddBody();

            var b = new IHTMLTextArea().AttachTo(content);

            htext.onclick +=
                delegate
                {
                    content.ToggleVisible();
                };

            a.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            a.style.width = "100%";
            a.style.height = "20em";


            b.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            b.style.width = "100%";
            b.style.height = "20em";

            b.readOnly = true;



            Action<Action<string>> update_output =
                handler =>
                {
                    var w = new StringBuilder();
                    var w2 = new StringBuilder();

                    var lines = a.Lines.ToArray();

                    if (IsEnum.@checked)
                    {
                        w.AppendLine("public enum " + DeclaringType.value);
                        w.AppendLine("{");
                    }
                    else
                        w.AppendLine("#region Constants");

                    for (int i = 0; i < lines.Length; i += 2)
                    {
                        if ((i + 1) < lines.Length)
                        {
                            var Summary = lines[i + 1].Trim();

                            var x = lines[i].Split(':');


                            var ConstantName = x[0].Trim();

                            if (ConstantName.Contains("("))
                                throw new Exception("Invalid Constant Name");


                            var y = x[1].Trim().Split('=');

                            var ConstantType = FixTypeName(y[0].Trim());
                            var ConstantValue = y[1].Trim();


                            if (!ConstantName.Contains("AIR-only"))
                            {



                                w.AppendLine("/// <summary>");
                                w.AppendLine("/// " + Summary);
                                w.AppendLine("/// </summary>");




                                if (handler != null)
                                    handler(ConstantName);

                                if (IsEnum.@checked)
                                {
                                    var Prefix = DeclaringType.value.ToLower() + "_";

                                    if (ConstantName.ToLower().StartsWith(Prefix))
                                        ConstantName = ConstantName.Substring(Prefix.Length).ToLower().ToCamelCase();

                                    if (string.IsNullOrEmpty(ConstantValue))
                                        w.AppendLine(ConstantName + ",");
                                    else
                                        w.AppendLine(ConstantName + " = " + ConstantValue + ",");
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(ConstantValue))
                                        w.AppendLine("public static readonly " + ConstantType + " " + ConstantName + ";");
                                    else
                                        w.AppendLine("public static readonly " + ConstantType + " " + ConstantName + " = " + ConstantValue + ";");
                                }

                                w.AppendLine();


                            }


                        }
                    }

                    if (IsEnum.@checked)
                    {
                        w.AppendLine("}");
                    }
                    else
                        w.AppendLine("#endregion");



                    b.value = w.ToString();
                };

            update =
                delegate
                {
                    try
                    {
                        zb.removeChildren();

                        update_output(
                            null
                        );
                        htext.style.color = Color.Blue;
                    }
                    catch (Exception ex)
                    {
                        htext.style.color = Color.Red;
                        b.value = "error: " + ex.Message;
                    }
                };

            DeclaringType.onchange += delegate { update(); };
            IsEnum.onchange += delegate { update(); };


            a.onchange += delegate { update(); };
        }

        private void AddProperties(IHTMLElement htext)
        {
            //  todo: interface properties

            var content = new IHTMLDiv().AttachTo(DocumentBody);
            content.Hide();

            var IsField = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox);
            new IHTMLDiv(
                new IHTMLLabel("as fields instead of properties: ", IsField), IsField
            ).AttachTo(content);

            // var a = new IHTMLTextArea().AttachTo(content);
            var b = new IHTMLTextArea().AttachTo(content);


            htext.onclick +=
                delegate
                {
                    content.ToggleVisible();
                };

            a.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            a.style.width = "100%";
            a.style.height = "20em";


            b.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            b.style.width = "100%";
            b.style.height = "20em";

            b.readOnly = true;




            Action update =
                delegate
                {
                    try
                    {
                        //c.removeChildren();

                        var w = new StringBuilder();
                        var lines = a.Lines.ToArray();

                        if (IsField.@checked)
                            w.AppendLine("#region Fields");
                        else
                            w.AppendLine("#region Properties");

                        for (int i = 0; i < lines.Length; i += 2)
                        {
                            if ((i + 1) < lines.Length)
                            {
                                var Summary = lines[i + 1].Trim();

                                if (!lines[i].Contains("AIR-only"))
                                {
                                    var ReadOnly = "[read-only]";

                                    w.AppendLine("/// <summary>");
                                    w.AppendLine("/// " + Summary);
                                    w.AppendLine("/// </summary>");


                                    var x0 = lines[i].Split(':');
                                    var x1 = x0[1].Trim().Split('=');

                                    var DefaultValue = "";

                                    if (x1.Length == 2)
                                        DefaultValue = x1[1].Trim();

                                    var TypeName = FixTypeName(x1[0].Trim());

                                    var FieldName = x0[0].Trim();

                                    /*
                                    var Image = (IHTMLImage)img.cloneNode(false);

                                    Image.style.verticalAlign = "middle";

                                    new IHTMLDiv(
                                        Image,
                                        new IHTMLCode(TypeName + " " + FieldName)
                                        ).AttachTo(c);
                                    */

                                    var StaticModifier = Summary.Contains("[static]") ? "static " : "";

                                    if (IsField.@checked)
                                    {
                                        var ReadonlyModifier = Summary.Contains(ReadOnly) ? "readonly " : "";

                                        var DefaultValueExpression = string.IsNullOrEmpty(DefaultValue) ? "" : " = " + DefaultValue;

                                        w.AppendLine("public " + StaticModifier + ReadonlyModifier + TypeName + " " + FieldName + DefaultValueExpression + ";");
                                    }
                                    else
                                    {
                                        if (Summary.Contains(ReadOnly))
                                            w.AppendLine("public " + StaticModifier + TypeName + " " + FieldName + " { get; private set; }");
                                        else
                                            w.AppendLine("public " + StaticModifier + TypeName + " " + FieldName + " { get; set; }");
                                    }

                                    w.AppendLine();
                                }
                            }
                        }

                        w.AppendLine("#endregion");

                        b.value = w.ToString();
                        htext.style.color = Color.Blue;
                    }
                    catch (Exception ex)
                    {
                        htext.style.color = Color.Red;
                        b.value = "error: " + ex.Message;
                    }
                };

            a.onchange += delegate { update(); };
            IsField.onchange += delegate { update(); };
        }



        static ConvertASToCS()
        {
            typeof(ConvertASToCS).SpawnTo(i => new ConvertASToCS());
        }

    }




}
