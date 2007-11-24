using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib
{
    // http://www.devguru.com/Technologies/ecmascript/quickref/js_property.html
    using SpawnItem = Pair<string, EventHandler<IHTMLElement>>;


    namespace JavaScript
    {
        [Script]
        public static class Native
        {


            [Script(ExternalTarget = "window")]
            static public IWindow Window;

            [Script(ExternalTarget = "document")]
            static public IHTMLDocument Document;

            
            [Script(ExternalTarget = "Math"), System.Obsolete("Use global::System.Math instead!", false)]
            static internal IMath Math;
            

            [Script(ExternalTarget = "screen")]
            static public IScreen Screen;


            public static EventHandler<IEvent> DisabledEventHandler
            {
                get
                {
                    return delegate(IEvent e)
                    {
                        e.PreventDefault();
                        e.StopPropagation();
                    };
                }
            }

            static Native()
            {


            }

            public static void Spawn(params SpawnItem[] e)
            {
                foreach (var x in e)
                {
                     Native.Spawn(x.A, x.B);
                }
            }

            /// <summary>
            /// Searches all tags by className, and spawns a control in that element at window load
            /// </summary>
            /// <param name="e">className</param>
            /// <param name="Spawn">delegate with owner element</param>
            public static void Spawn(string id, EventHandler<IHTMLElement> Spawn)
            {
                Console.Log("spawn on load: " + id);

                if (Native.Window == null)
                    return;

                Native.Window.onload +=
                    delegate
                    {
                        Native.Document.getElementsByClassName(id).ForEach(
                            delegate(IHTMLElement e)
                            {
                                Console.Log("spawn: {" + id + "}");

                                Spawn(e);
                            });
                    };
            }


            public static void Spawn(string id, EventHandler<IHTMLElement, string> s)
            {
                Console.Log("spawn on load: " + id);

                Native.Window.onload +=
                    delegate
                    {


                        Native.Document.getElementsByClassName(id).ForEach(
                            delegate(IHTMLElement v)
                            {
                                Console.Log("spawn: {" + id + "}");


                                s(v, id);
                            }

                        );



                    };
            }

            internal static void SpawnInline(string classname, EventHandler<IHTMLElement> h)
            {
                Native.Document.getElementsByClassName(classname + ":inline").ForEach(h);
            }

            public static IHTMLEmbed PlaySound(string src)
            {
                var u = new IHTMLEmbed();

                u.autostart = "true";
                u.volume = "100";
                u.src = src;
                u.style.SetLocation(0, 0, 0, 0);

                Native.Document.body.appendChild(u);

                return u;
            }

            public static void Include(string src)
            {
                Console.Log("include " + src);

                var s = new IHTMLScript();
                s.type = "text/javascript";
                s.src = src;

                s.attachToDocument();
            }
        }
    }




}
