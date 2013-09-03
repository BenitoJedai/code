using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using System.Threading.Tasks;


namespace ScriptCoreLib.JavaScript.DOM.HTML
{

    /// <summary>
    /// 
    /// </summary>
    [Script(InternalConstructor = true /*, ExternalTarget = "Image"*/)]
    public class IHTMLImage : IHTMLElement
    {
        // http://www.w3schools.com/tags/tag_IMG.asp

        public string alt;
        public string src;
        public int border;

        public bool complete;

        #region constructors

        public IHTMLImage() { }

        static internal IHTMLImage InternalConstructor()
        {
            return (IHTMLImage)new IHTMLElement(HTMLElementEnum.img);
        }

        public IHTMLImage(string src) { }


        static internal IHTMLImage InternalConstructor(string src)
        {
            return new IHTMLImage { src = src };
        }

        public IHTMLImage(int width, int height) { }

        static internal IHTMLImage InternalConstructor(int width, int height)
        {
            var i = new IHTMLImage { };

            i.style.SetSize(width, height);

            return i;
        }


        #endregion



        #region event onerror
        public event System.Action<IEvent> onerror
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
            return new IHTMLImage { src = src };
        }

        // fixme: rewrtie to extension methods

        [System.Obsolete("use await instead")]
        [Script(DefineAsStatic = true)]
        public void InvokeOnComplete(global::System.Action<IHTMLImage> e)
        {
            InvokeOnComplete(e, 100);
        }

        [System.Obsolete("use await instead")]
        [Script(DefineAsStatic = true)]
        public void InvokeOnComplete(global::System.Action<IHTMLImage> e, int interval)
        {
            if (this.complete)
            {
                e(this);
                return;
            }

            Timer t2 = new Timer();

            t2.Tick +=
                 delegate
                 {
                     if (this.complete)
                     {
                         t2.Stop();
                         e(this);
                         return;
                     }
                 };

            t2.StartInterval(interval);


        }

        [System.Obsolete]
        [Script(DefineAsStatic = true)]
        public void Reload()
        {
            string x = this.src;

            this.src = x;
        }



        [Script(DefineAsStatic = true)]
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


        public Task<byte[]> bytes
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var y = new TaskCompletionSource<byte[]>();

                this.InvokeOnComplete(
                    delegate
                    {
                        var c = new CanvasRenderingContext2D(this.width, this.height);

                        c.drawImage(
                            this, 0, 0, c.canvas.width, c.canvas.height
                        );
                        ;

                        y.SetResult(c.bytes);
                    }
                );
                return y.Task;
            }

        }



        //public static implicit operator byte[](IHTMLImage e)
        //{
        //    return e.bytes;
        //}
    }
}
