using ScriptCoreLib.Shared;
using System;

using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript
{
    // http://www.devguru.com/Technologies/ecmascript/quickref/js_property.html
    using SpawnItem = Pair<string, System.Action<IHTMLElement>>;


    [Script]
    public static partial class Native
    {
        //#region window
        //[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        //[Obsolete("window", true)]
        //[Script(ExternalTarget = "window")]
        //static public IWindow Window;

        //// web worker will not have this. this is the global this object
        //#endregion

        // 60 issues to deal with before we can recompile
        const bool error = false;

        #region document
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Script(ExternalTarget = "document")]
        [Obsolete("document", error)]
        static public IHTMLDocument Document;


        #endregion


        // window.Math ? Math module
        [Script(ExternalTarget = "Math"), System.Obsolete("Use global::System.Math instead!", false)]
        static internal IMath Math;


        #region screen
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("screen", error)]
        [Script(ExternalTarget = "screen")]
        static public IScreen Screen;

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

        // dynamic ?
        static object __self;

        public static object self
        {
            get
            {
                if (__self == null)
                {
                    // x:\jsc.svn\examples\javascript\OmniWebWorkerExperiment\OmniWebWorkerExperiment\Application.cs
                    __self = new IFunction("return this;").apply(null);
                }

                return __self;
            }
        }

        static public IWindow window;

        // alias for window.document. not available for web workers
        static public IHTMLDocument document;

        static public IScreen screen;
        static public DedicatedWorkerGlobalScope worker;
        static public SharedWorkerGlobalScope sharedworker;

        static partial void Uint8ClampedArray();

        static Native()
        {


            Uint8ClampedArray();


            // what is it?
            if (Expando.InternalIsMember(self, "document")
                && Expando.InternalIsMember(self, "screen"))
            {
                // should be a window with a document
                window = (IWindow)self;

                document = window.document;
                screen = window.screen;
            }
            else if (Expando.InternalIsMember(self, "importScripts"))
            {

                if (Expando.InternalIsMember(self, "postMessage"))
                {
                    // now what. are we running as a web worker?
                    // WorkerGlobalScope
                    // DedicatedWorkerGlobalScope
                    // DedicatedWorkerContext

                    worker = (DedicatedWorkerGlobalScope)self;
                }
                else
                {
                    // now what. are we running as a web worker?
                    // WorkerGlobalScope
                    // DedicatedWorkerGlobalScope
                    // DedicatedWorkerContext

                    sharedworker = (SharedWorkerGlobalScope)self;
                }
            }


        }

        [System.Obsolete]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
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
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void Spawn(string id, System.Action<IHTMLElement> Spawn)
        {
            try
            {
                System.Console.WriteLine("spawn on load: " + id);

                //if (Native.Window == null)


                Native.window.onload +=
                    delegate
                    {
                        Native.document.getElementsByClassName(id).ForEach(
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
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void Spawn(string id, System.Action<IHTMLElement, string> s)
        {
            System.Console.WriteLine("spawn on load: " + id);

            Native.window.onload +=
                delegate
                {


                    Native.document.getElementsByClassName(id).ForEach(
                        delegate(IHTMLElement v)
                        {
                            System.Console.WriteLine("spawn: {" + id + "}");


                            s(v, id);
                        }

                    );



                };
        }

        [System.Obsolete]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        internal static void SpawnInline(string classname, System.Action<IHTMLElement> h)
        {
            Native.Document.getElementsByClassName(classname + ":inline").ForEach(h);
        }

        public static int setTimeout(Action yield, int ms)
        {
            if (Native.window != null)
                return Native.window.setTimeout(yield, ms);

            return Native.worker.setTimeout(yield, ms);
        }

        public static int setInterval(Action yield, int ms)
        {
            if (Native.window != null)
                return Native.window.setInterval(yield, ms);

            return Native.worker.setInterval(yield, ms);
        }

        public static void clearTimeout(int i)
        {
            if (Native.window != null)
            {
                Native.window.clearTimeout(i);
                return;
            }

            Native.worker.clearTimeout(i);
        }

        public static void clearInterval(int i)
        {
            if (Native.window != null)
            {
                Native.window.clearInterval(i);
                return;
            }

            Native.worker.clearInterval(i);
        }

        public static string encodeURIComponent(string i)
        {
            if (Native.window != null)
            {
                return Native.window.encodeURIComponent(i);
            }

            return Native.worker.encodeURIComponent(i);
        }

        public static string decodeURIComponent(string i)
        {
            if (Native.window != null)
            {
                return Native.window.decodeURIComponent(i);
            }

            return Native.worker.decodeURIComponent(i);
        }

        public static string escape(string i)
        {
            if (Native.window != null)
            {
                return Native.window.escape(i);
            }

            return Native.worker.escape(i);
        }

        public static string unescape(string i)
        {
            if (Native.window != null)
            {
                return Native.window.unescape(i);
            }

            return Native.worker.unescape(i);
        }
    }




}
