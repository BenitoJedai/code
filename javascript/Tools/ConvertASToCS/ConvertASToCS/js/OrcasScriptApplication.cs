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


namespace ConvertASToCS.js
{
    [Script, ScriptApplicationEntryPoint]
    public class ConvertASToCS
    {
        IHTMLInput DeclaringType = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.text);

        public ConvertASToCS()
        {
            Native.Document.title = "ConvertASToCS";

            var cookie = new Cookie("DeclaringType").BindTo(DeclaringType);

            new IHTMLDiv(
                new IHTMLLabel("DeclaringType: ", DeclaringType), DeclaringType
                ).AttachToDocument();

            AddEvents();
            AddConstants();
            AddProperties();
            AddMethods();
        }

        private void AddMethods()
        {
            var h = new IHTMLElement(IHTMLElement.HTMLElementEnum.h3).AttachToDocument();
            var htext = new IHTMLSpan("Methods (click to show/hide)").AttachTo(h);
            var content = new IHTMLDiv().AttachToDocument();
            content.Hide();

            var update = default(Action);

            var a = new IHTMLTextArea().AttachTo(content);
            IHTMLButton.Create(
                "Example code",
                delegate
                {
                    a.value =
@"XML(value:Object)
Creates a new XML object.
	
addNamespace(ns:Object):XML
Adds a namespace to the set of in-scope namespaces for the XML object.
	
appendChild(child:Object):XML
Appends the given child to the end of the XML object's properties.
	
attribute(attributeName:*):XMLList
Returns the XML value of the attribute that has the name matching the attributeName parameter.
	
attributes():XMLList
Returns a list of attribute values for the given XML object.
	
child(propertyName:Object):XMLList
Lists the children of an XML object.
	
childIndex():int
Identifies the zero-indexed position of this XML object within the context of its parent.
	
children():XMLList
Lists the children of the XML object in the sequence in which they appear.
	
comments():XMLList
Lists the properties of the XML object that contain XML comments.
	
contains(value:XML):Boolean
Compares the XML object against the given value parameter.
	
copy():XML
Returns a copy of the given XML object.
	
defaultSettings():Object
[static] Returns an object with the following properties set to the default values: ignoreComments, ignoreProcessingInstructions, ignoreWhitespace, prettyIndent, and prettyPrinting.
	
descendants(name:Object = *):XMLList
Returns all descendants (children, grandchildren, great-grandchildren, and so on) of the XML object that have the given name parameter.
	
elements(name:Object = *):XMLList
Lists the elements of an XML object.
	
hasComplexContent():Boolean
Checks to see whether the XML object contains complex content.
	
hasOwnProperty(p:String):Boolean
Checks to see whether the object has the property specified by the p parameter.
	
hasSimpleContent():Boolean
Checks to see whether the XML object contains simple content.
	
inScopeNamespaces():Array
Lists the namespaces for the XML object, based on the object's parent.
	
insertChildAfter(child1:Object, child2:Object):*
Inserts the given child2 parameter after the child1 parameter in this XML object and returns the resulting object.
	
insertChildBefore(child1:Object, child2:Object):*
Inserts the given child2 parameter before the child1 parameter in this XML object and returns the resulting object.
	
length():int
For XML objects, this method always returns the integer 1.
	
localName():Object
Gives the local name portion of the qualified name of the XML object.
	
name():Object
Gives the qualified name for the XML object.
	
namespace(prefix:String = null):*
If no parameter is provided, gives the namespace associated with the qualified name of this XML object.
	
namespaceDeclarations():Array
Lists namespace declarations associated with the XML object in the context of its parent.
	
nodeKind():String
Specifies the type of node: text, comment, processing-instruction, attribute, or element.
	
normalize():XML
For the XML object and all descendant XML objects, merges adjacent text nodes and eliminates empty text nodes.
	
parent():*
Returns the parent of the XML object.
	
prependChild(value:Object):XML
Inserts a copy of the provided child object into the XML element before any existing XML properties for that element.
	
processingInstructions(name:String = ""*""):XMLList
If a name parameter is provided, lists all the children of the XML object that contain processing instructions with that name.
	
propertyIsEnumerable(p:String):Boolean
Checks whether the property p is in the set of properties that can be iterated in a for..in statement applied to the XML object.
	
removeNamespace(ns:Namespace):XML
Removes the given namespace for this object and all descendants.
	
replace(propertyName:Object, value:XML):XML
Replaces the properties specified by the propertyName parameter with the given value parameter.
	
setChildren(value:Object):XML
Replaces the child properties of the XML object with the specified set of XML properties, provided in the value parameter.
	
setLocalName(name:String):void
Changes the local name of the XML object to the given name parameter.
	
setName(name:String):void
Sets the name of the XML object to the given qualified name or attribute name.
	
setNamespace(ns:Namespace):void
Sets the namespace associated with the XML object.
	
setSettings(... rest):void
[static] Sets values for the following XML properties: ignoreComments, ignoreProcessingInstructions, ignoreWhitespace, prettyIndent, and prettyPrinting.
	
settings():Object
[static] Retrieves the following properties: ignoreComments, ignoreProcessingInstructions, ignoreWhitespace, prettyIndent, and prettyPrinting.
	
text():XMLList
Returns an XMLList object of all XML properties of the XML object that represent XML text nodes.
	
toString():String
Returns a string representation of the XML object.
	
toXMLString():String
Returns a string representation of the XML object.
	
valueOf():XML
Returns the XML object.
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

                                            w.AppendLine("public " + StaticModifier + MethodReturnType + " " + MethodName + "(" + v + ")");
                                            w.AppendLine("{");

                                            if (MethodReturnType != "void")
                                                w.AppendLine("  return default(" + MethodReturnType + ");");

                                            w.AppendLine("}");
                                            w.AppendLine();
                                        }
                                    }
                                }




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

                        update_output(

                        );
                    }
                    catch (Exception ex)
                    {
                        b.value = "error: " + ex.Message;
                    }
                };


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

                var p = new ParamInfo[this.Parameters.Length - 1];

                for (int i = 0; i < p.Length; i++)
                {
                    p[i] = this.Parameters[i];
                }

                return new MethodParametersInfo(p);
            }

            public IEnumerable<MethodParametersInfo> Variations
            {
                get
                {
                    if (Parameters.Length == 0)
                        return new[] { this }.AsEnumerable();

                    var v = new[]
                    {
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

                    if (this.Parameters.Last().HasDefaultValue)
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
        }

        private void AddEvents()
        {
            var h = new IHTMLElement(IHTMLElement.HTMLElementEnum.h3).AttachToDocument();
            var htext = new IHTMLSpan("Events (click to show/hide)").AttachTo(h);
            var content = new IHTMLDiv().AttachToDocument();
            content.Hide();

            var update = default(Action);

            var a = new IHTMLTextArea().AttachTo(content);
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

                    var DeclaringTypeName = DeclaringType.value;

                    for (int i = 0; i < lines.Length; i += 2)
                    {
                        if ((i + 1) < lines.Length)
                        {
                            var Summary = lines[i + 1].Trim();
                            var EventName = lines[i].Trim();

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


                                w.AppendLine("[method: Script(NotImplementedHere = true)]");
                                w.AppendLine("public event Action<" + EventType + "> " + EventName + ";");

                                w.AppendLine();



                                w2.AppendLine("#region " + EventName);
                                w2.AppendLine("public static void add_" + EventName + "(" + DeclaringTypeName + " that, Action<" + EventType + "> value)");
                                w2.AppendLine("{");
                                w2.AppendLine(" CommonExtensions.CombineDelegate(that, value, " + EventType + "." + EventCodeName + ");");
                                w2.AppendLine("}");
                                w2.AppendLine();
                                w2.AppendLine("public static void remove_" + EventName + "(" + DeclaringTypeName + " that, Action<" + EventType + "> value)");
                                w2.AppendLine("{");
                                w2.AppendLine(" CommonExtensions.RemoveDelegate(that, value, " + EventType + "." + EventCodeName + ");");
                                w2.AppendLine("}");
                                w2.AppendLine("#endregion");
                                w2.AppendLine();
                            }

                        }
                    }

                    w.AppendLine("#endregion");

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
                    }
                    catch (Exception ex)
                    {
                        b.value = "error: " + ex.Message;
                    }
                };


            DeclaringType.onchange +=
               delegate
               {
                   update_output(null);
               };

            a.onchange +=
                delegate
                {
                    update();
                };
        }


        private void AddConstants()
        {
            var h = new IHTMLElement(IHTMLElement.HTMLElementEnum.h3).AttachToDocument();
            var htext = new IHTMLSpan("Constants (click to show/hide)").AttachTo(h);
            var content = new IHTMLDiv().AttachToDocument();
            content.Hide();

            var update = default(Action);

            var a = new IHTMLTextArea().AttachTo(content);
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

                    w.AppendLine("#region Constants");

                    for (int i = 0; i < lines.Length; i += 2)
                    {
                        if ((i + 1) < lines.Length)
                        {
                            var Summary = lines[i + 1].Trim();

                            var x = lines[i].Split(':');


                            var ConstantName = x[0].Trim();

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

                                w.AppendLine("public static readonly " + ConstantType + " " + ConstantName + " = " + ConstantValue + ";");

                                w.AppendLine();


                            }


                        }
                    }

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
                    }
                    catch (Exception ex)
                    {
                        b.value = "error: " + ex.Message;
                    }
                };


            a.onchange +=
                delegate
                {
                    update();
                };
        }

        private void AddProperties()
        {

            var h = new IHTMLElement(IHTMLElement.HTMLElementEnum.h3).AttachToDocument();
            var htext = new IHTMLSpan("Properties (click to show/hide)").AttachTo(h);
            var a = new IHTMLTextArea().AttachToDocument();
            var b = new IHTMLTextArea().AttachToDocument();

            htext.onclick +=
                delegate
                {
                    a.ToggleVisible();
                    b.ToggleVisible();
                };

            a.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            a.style.width = "100%";
            a.style.height = "20em";


            b.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
            b.style.width = "100%";
            b.style.height = "20em";

            b.readOnly = true;

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

                                if (!lines[i].Contains("AIR-only"))
                                {
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

        private static string FixVariableName(string Name)
        {
            var list = new List<string>
            {
                "namespace",
                "event",
                "for",
                "as",
                "in",
                "out"
            };

            if (list.Contains(Name))
                return "@" + Name;

            return Name;
        }

        private static string FixTypeName(string TypeName)
        {
            var dict = new Dictionary<string, string>
                                {
                                    {"*", "object"},
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
