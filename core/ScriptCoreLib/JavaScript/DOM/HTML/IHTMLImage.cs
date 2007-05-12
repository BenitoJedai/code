using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;


namespace ScriptCoreLib.JavaScript.DOM.HTML
{

    /// <summary>
    /// 
    /// </summary>
    [Script(InternalConstructor=true, ExternalTarget = "Image")]
    public class IHTMLImage : IHTMLElement
    {
        public string alt;
        public string src;
        public int border;

        public bool complete;

        #region constructors
        public IHTMLImage() { }
        public IHTMLImage(int width, int height) { }
        public IHTMLImage(string src) { }


        static internal IHTMLImage InternalConstructor(string src)
        {
            try
            {
                IHTMLImage n = new IHTMLImage();

                n.src = src;

                return n;
            }
            catch
            {
                string u = "image failed to load: [" + src + "]";

                Console.LogError(u);

                throw new global::System.Exception(u);
            }
            
        }

        #endregion



        #region event onerror
        public event EventHandler<IEvent> onerror
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "error");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "error");
            }
        }
        #endregion

        public static implicit operator IHTMLImage(string src)
        {
            return new IHTMLImage(src);
        }


        [Script(DefineAsStatic = true)]
        public void InvokeOnComplete(EventHandler<IHTMLImage> e)
        {
            InvokeOnComplete(e, 100);
        }

        [Script(DefineAsStatic=true)]
        public void InvokeOnComplete(EventHandler<IHTMLImage> e, int interval)
        {
            Timer t = new Timer();
            
            t.Tick += 
                 delegate
                 {
                     if (this.complete)
                     {
                         t.Stop();
                         e(this);
                     }
                 };

            t.StartInterval(interval);

        }

        /// <summary>
        /// reloads gif animation
        /// </summary>
        [Script(DefineAsStatic = true)]
        public void Reload()
        {
            string x = this.src;

            this.src = x;
        }



        [Script(DefineAsStatic=true)]
        public void ToDocumentBackground()
        {
            ToBackground(Native.Document.body.style);
        }

        [Script(DefineAsStatic = true)]
        public void ToBackground(IStyle s)
        {
            ToBackground(s, true);
        }

        [Script(DefineAsStatic = true)]
        public void ToBackground(IStyle s, bool repeat)
        {
            s.SetBackground(src, repeat);
        }
    }
}
