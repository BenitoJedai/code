using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.DOM.SVG;
using System.Xml.Linq;
using System.Linq;
using System.Text;


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

        [Script(DefineAsStatic = true)]
        public virtual void Add(string e)
        {
            // X:\jsc.svn\examples\javascript\WebCamAvatarsExperiment\WebCamAvatarsExperiment\Application.cs

            // what about multiple sources being added this way?
            // make them a gif? :)
            // animate them?

            this.src = e;
        }

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

        public static implicit operator IHTMLImage(IHTMLDiv div)
        {
            System.Console.WriteLine("IHTMLImage <- IHTMLDiv");
            // Error	2	Cannot implicitly convert type 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDiv' to 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage'	X:\jsc.svn\examples\javascript\svg\SVGHTMLElement\SVGHTMLElement\Application.cs	34	48	SVGHTMLElement

            // X:\jsc.svn\examples\javascript\svg\SVGHTMLElement\SVGHTMLElement\Application.cs

            return (Task<ISVGSVGElement>)div;
        }


        public static implicit operator IHTMLImage(ISVGSVGElement s)
        {
            var img = new IHTMLImage();


            {

                // https://bugzilla.mozilla.org/show_bug.cgi?id=368437
                var xml = new XElement("xml", s.AsXElement()).Elements().First();
                var xmlstring = xml.ToString().Replace("<IMG", "<img");

                //Console.WriteLine(new { xmlstring });

                var url = "data:image/svg+xml;base64," + global::System.Convert.ToBase64String(Encoding.UTF8.GetBytes(xmlstring));
                img.src = url;
            }

            return img;
        }

        public static implicit operator IHTMLImage(Task<ISVGSVGElement> ss)
        {
            System.Console.WriteLine("IHTMLImage <- Task<ISVGSVGElement>");
            var img = new IHTMLImage();

            ss.ContinueWith(
                r =>
                {
                    var s = r.Result;

                    // https://bugzilla.mozilla.org/show_bug.cgi?id=368437
                    var xml = new XElement("xml", s.AsXElement()).Elements().First();
                    var xmlstring = xml.ToString().Replace("<IMG", "<img");

                    //Console.WriteLine(new { xmlstring });

                    var url = "data:image/svg+xml;base64," + global::System.Convert.ToBase64String(Encoding.UTF8.GetBytes(xmlstring));
                    img.src = url;
                }
            );

            return img;
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
            if (!string.IsNullOrEmpty(this.src))
                if (this.complete)
                {
                    e(this);
                    return;
                }

            Timer t2 = new Timer();

            t2.Tick +=
                 delegate
                 {
                     if (!string.IsNullOrEmpty(this.src))
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

        [System.Obsolete]
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
