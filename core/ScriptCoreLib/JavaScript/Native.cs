using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib
{
    // http://www.devguru.com/Technologies/ecmascript/quickref/js_property.html
    using SpawnItem = Pair<string, System.Action<IHTMLElement>>;
    using System;


    namespace JavaScript
    {
        [Script]
        public static class Native
        {
            #region window
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            [Obsolete("window")]
            [Script(ExternalTarget = "window")]
            static public IWindow Window;

            // web worker will not have this. this is the global this object
            [Script(ExternalTarget = "window")]
            static public IWindow window;
            #endregion


            #region document
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            [Script(ExternalTarget = "document")]
            [Obsolete("document")]
            static public IHTMLDocument Document;

            // alias for window.document. not available for web workers
            [Script(ExternalTarget = "document")]
            static public IHTMLDocument document;
            #endregion


            // window.Math ? Math module
            [Script(ExternalTarget = "Math"), System.Obsolete("Use global::System.Math instead!", false)]
            static internal IMath Math;


            #region screen
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            [Obsolete("screen")]
            [Script(ExternalTarget = "screen")]
            static public IScreen Screen;

            [Script(ExternalTarget = "screen")]
            static public IScreen screen;
            #endregion



            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            [System.Obsolete("To be moved out of CoreLib or removed")]
            public static System.Action<IEvent> DisabledEventHandler
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

            [System.Obsolete]
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
            [System.Obsolete]
            public static void Spawn(string id, System.Action<IHTMLElement> Spawn)
            {
                try
                {
                    System.Console.WriteLine("spawn on load: " + id);

                    //if (Native.Window == null)


                    Native.Window.onload +=
                        delegate
                        {
                            Native.Document.getElementsByClassName(id).ForEach(
                                delegate(IHTMLElement e)
                                {
                                    System.Console.WriteLine("spawn: {" + id + "}");

                                    Spawn(e);
                                });
                        };
                }
                catch
                {
                    // wont work within web worker
                }
            }


            [System.Obsolete]
            public static void Spawn(string id, System.Action<IHTMLElement, string> s)
            {
                System.Console.WriteLine("spawn on load: " + id);

                Native.Window.onload +=
                    delegate
                    {


                        Native.Document.getElementsByClassName(id).ForEach(
                            delegate(IHTMLElement v)
                            {
                                System.Console.WriteLine("spawn: {" + id + "}");


                                s(v, id);
                            }

                        );



                    };
            }

            [System.Obsolete]
            internal static void SpawnInline(string classname, System.Action<IHTMLElement> h)
            {
                Native.Document.getElementsByClassName(classname + ":inline").ForEach(h);
            }

            //[System.Obsolete("To be moved out of CoreLib or removed")]
            //public static IHTMLEmbed PlaySound(string src)
            //{
            //    var u = new IHTMLEmbed();

            //    u.autostart = "true";
            //    u.volume = "100";
            //    u.src = src;
            //    u.style.SetLocation(0, 0, 0, 0);

            //    Native.Document.body.appendChild(u);

            //    return u;
            //}

            //[System.Obsolete("To be moved out of CoreLib or removed")]
            //public static void Include(string src)
            //{
            //    System.Console.WriteLine("include " + src);

            //    var s = new IHTMLScript();
            //    s.type = "text/javascript";
            //    s.src = src;

            //    s.AttachToDocument();
            //}
        }
    }




}
