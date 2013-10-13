using ScriptCoreLib.JavaScript;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
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


        public static implicit operator IHTMLSpan(string innerText)
        {
            return new IHTMLSpan { innerText = innerText };
        }



        [System.Obsolete("experimental")]
        public static implicit operator IHTMLSpan(Task<string> innerText)
        {
            // first step for databinding?
            var s = new IHTMLSpan { };

            innerText.ContinueWith(
                task =>
                {
                    s.innerText = task.Result;
                }
            );

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
    }
}
