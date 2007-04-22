using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.System;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.Shared;


namespace ScriptCoreLib.JavaScript.Silverlight
{
    [Script]
    public static class SilverlightControlExtensions
    {
        static public void InvokeOnComplete(this SilverlightControl x, Action done)
        {
            Timer.While(() => !x.IsLoaded, done, Timer.DefaultTimeout);
        }

        static public DependencyObject CreateElement(this SilverlightControl x, string name)
        {
            return x.CreateFromXAML("<" + name + " />");
        }

        //[Script(OptimizedCode="{arg0}.onload = {arg1};", UseCompilerConstants=true)]
        //internal static void add_onload(this SilverlightControl e, IFunction f)
        //{

        //}
    }

    // http://msdn2.microsoft.com/en-us/library/bb188406.aspx

    [Script(InternalConstructor = true)]
    public class SilverlightControl : IHTMLElement
    {
        /// <summary>
        /// Loads the content of a XAML file into the WPF/E control.
        /// </summary>
        public string Source;

        /// <summary>
        /// Loads a string containing XAML content into the WPF/E control.
        /// </summary>
        public string SourceElement;

        /// <summary>
        /// Loads a string containing XAML content into the WPF/E control. Unlike the SourceElement property, the SourceString property can only be set at run time using JavaScript.
        /// </summary>
        public string SourceString;

        /// <summary>
        /// Specifies a user-defined JavaScript error handling function that is invoked when an error is generated in the WPF/E runtime components.
        /// </summary>
        public string OnError;

        /// <summary>
        /// An integer value that specifies the maximum number of frames to render per second.
        /// </summary>
        public int MaxFrameRate;

        /// <summary>
        ///  The IsLoaded property is set to true after the XAML content in the WPF/E control has completely loaded, but before the OnLoad event occurs.
        /// </summary>
        public bool IsLoaded;

        /// <summary>
        /// Determines whether to show areas of the plug-in that are being redrawn each frame.
        /// </summary>
        public bool EnableRedrawRegions;

        /// <summary>
        /// Specifies the background color value as a string, which can either be a named color value, or an 8-bit or 16-bit color value, with or without alpha transparency.
        /// </summary>
        public string BackgroundColor;

        /// <summary>
        /// The width of the rendering area of the WPF/E control in pixels, not device-independent units.
        /// </summary>
        public int ActualWidth;

        /// <summary>
        /// The height of the rendering area of the WPF/E control in pixels, not device-independent units.
        /// </summary>
        public int ActualHeight;

        /// <summary>
        /// Determines whether the WPF/E control displays as a windows-less or windowed control.
        /// </summary>
        public bool WindowlessMode;

        /// <summary>
        /// A string that represents the version of the WPF/E control.
        /// </summary>
        public string Version;

        /// <summary>
        /// Determines whether the WPF/E control displays as a full-screen control or embedded control.
        /// </summary>
        public bool FullScreen;

        /// <summary>
        /// Gets any object in the WPF/E object hierarchy by referencing the object's x:Name attribute value.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DependencyObject FindName(string name)
        {
            return default(DependencyObject);
        }

        /// <summary>
        /// Creates a specified object and returns a reference to it.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public object CreateObject(string e)
        {
            return default(object);

        }

        /// <summary>
        /// Creates XAML content dynamically.
        /// </summary>
        /// <param name="xaml"></param>
        /// <returns></returns>
        public DependencyObject CreateFromXAML(string xaml)
        {
            return default(DependencyObject);
        }

        /// <summary>
        /// Creates XAML content dynamically using downloader content.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public DependencyObject CreateFromXamlDownloader(Downloader e)
        {
            return default(DependencyObject);
        }

        /// <summary>
        /// Reloads the WPF/E control. This method must be invoked to force any changed WPF/E control properties to take effect.
        /// </summary>
        public void Reload()
        {
        }

        #region InternalConstructors

        public SilverlightControl(IHTMLDocument doc)
        {

        }

        internal static SilverlightControl InternalConstructor(IHTMLDocument doc)
        {

            return default(SilverlightControl);
        }

        public SilverlightControl()
        {

        }

        internal static SilverlightControl InternalConstructor()
        {
            return new SilverlightControl(Native.Document);
        }

        #endregion



        //public event Action<SilverlightControl> onload
        //{
        //    [Script(DefineAsStatic = true)]
        //    remove 
        //    {
        //        //EventHandlerProxy.SetValue(this, "OnLoad", null);
        //    }
        //    [Script(DefineAsStatic = true)]
        //    add 
        //    {
        //        this.add_onload(IFunction.OfDelegate(value));
        //    }
        //}

        public event Action<SilverlightControl> FullScreenChanged
        {
            [Script(DefineAsStatic = true)]
            remove
            {
                RemoveEventHandler(this, "FullScreenChanged", value);
            }
            [Script(DefineAsStatic = true)]
            add
            {
                AddEventHandler(this, "FullScreenChanged", value);
            }
        }


        static public void RemoveEventHandler(object Target, string Member, global::System.Delegate Handler)
        {

        }

        [Script]
        class IFunction_MulticastDelegate
        {
            public List<IFunction> list = new List<IFunction>();

            public IFunction handler;

            public EventHandlerProxy.Item proxy;
        }

        static public void AddEventHandler(object Target, string Member, global::System.Delegate Handler)
        {
            //var c = EventHandlerProxy.GetValueStringOrDefault(Target, Member);

            //if (c == null)
            //{
            //    Console.WriteLine("AddEventHandler first call");

            //    var data = new IFunction_MulticastDelegate();

            //    var multicast = new IFunction("data", @"return function (a) { window.dump('will invoke ' + data.list.length + ' items with ' + a ' \n'); };");

            //    data.handler = (IFunction)multicast.Invoke(data);

            //    data.handler.Invoke("param 1");

            //    data.list.Add(new IFunction("a", "window.dump('handler 1: ' + a + '\n');"));

            //    data.handler.Invoke("param 2");

            //    EventHandlerProxy.SetValueString(Target, Member, "javascript:xxx");

            //    //data.proxy = EventHandlerProxy.Add(data
            //}
            //else
            //{
            //    Console.WriteLine("AddEventHandler non first call");
            //}

            // Expando.Of(Target).SetMember(Member, "javascript:" + Add(Handler).FunctionName);
        }
    }


}