using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLSpanElement.idl
    // span is special having only operators?

    [Script(InternalConstructor = true)]
    public class IHTMLSpan : IHTMLElement
    {


        #region ctor
        public IHTMLSpan()
        {
        }

        public IHTMLSpan(string html)
        {
        }

        public IHTMLSpan(params INode[] e)
        {
        }

        static IHTMLSpan InternalConstructor()
        {
            return (IHTMLSpan)(object)new IHTMLElement(HTMLElementEnum.span);
        }

        static IHTMLSpan InternalConstructor(string e)
        {
            IHTMLSpan n = new IHTMLSpan();

            n.innerHTML = e;

            return n;
        }

        static IHTMLSpan InternalConstructor(params INode[] e)
        {
            IHTMLSpan n = new IHTMLSpan();

            n.appendChild(e);

            return n;
        }


        #endregion




        public static implicit operator IHTMLSpan(XElement x)
        {
            // X:\jsc.svn\examples\javascript\XElementFieldModifiedByWebService\XElementFieldModifiedByWebService\Application.cs
            // what if its not a button?
            // ScriptCoreLib.JavaScript.Extensions
            return (IHTMLSpan)x.AsHTMLElement();
        }

        public static implicit operator IHTMLSpan(string innerText)
        {
            return new IHTMLSpan { innerText = innerText };
        }

        public static implicit operator IHTMLSpan(int innerText)
        {
            return new IHTMLSpan { innerText = "" + innerText };
        }

        public static implicit operator IHTMLSpan(char innerText)
        {
            return new IHTMLSpan { innerText = new string(innerText, 1) };
        }

        public static implicit operator int(IHTMLSpan x)
        {
            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\Application.cs

            var z = default(int);

            int.TryParse(x.innerText, out z);

            return z;
        }



        //public static IHTMLSpan operator +(IHTMLSpan x, int i)
        //{
        //    return new IHTMLSpan { innerText = innerText };
        //}


        [System.Obsolete("experimental")]
        public static implicit operator IHTMLSpan(Task<string> innerText)
        {
            // X:\jsc.svn\examples\javascript\Test\TestUTF8GetStringPerformance\TestUTF8GetStringPerformance\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestMemoryStreamPerformance\TestMemoryStreamPerformance\Application.cs

            // first step for databinding?
            var s = new IHTMLSpan { };

            innerText.ContinueWith(task => { s.innerText = task.Result; });

            return s;
        }

        [System.Obsolete("experimental")]
        //public static implicit operator IHTMLSpan(Task<object> innerText)
        public static implicit operator IHTMLSpan(Task innerText)
        {
            // first step for databinding?
            var s = new IHTMLSpan { };


            var x = innerText as Task<object>;
            if (x != null)
            {
                x.ContinueWith(
                    task =>
                    {
                        s.innerText = task.Result.ToString();
                    }
                );
            }

            return s;
        }



        public static implicit operator IHTMLSpan(System.Type x)
        {
            return new IHTMLSpan { className = x.Name };
        }
    }
}
