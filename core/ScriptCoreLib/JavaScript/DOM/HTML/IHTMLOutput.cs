using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Xml.Linq;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLOutput : IHTMLElement
    {


        #region Constructor

        public IHTMLOutput()
        {
            // InternalConstructor
        }

        static IHTMLOutput InternalConstructor()
        {
            return (IHTMLOutput)(object)new IHTMLElement(HTMLElementEnum.output);
        }

        #endregion


        public static implicit operator IHTMLOutput(string innerText)
        {
            return new IHTMLOutput { innerText = innerText };
        }

        [System.Obsolete("experimental")]
        public static implicit operator IHTMLOutput(Task<string> innerText)
        {
            // X:\jsc.svn\examples\javascript\Test\TestUTF8GetStringPerformance\TestUTF8GetStringPerformance\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestMemoryStreamPerformance\TestMemoryStreamPerformance\Application.cs

            // first step for databinding?
            var s = new IHTMLOutput { };

            // inline scope sharing not yet implemented
            innerText.ContinueWith(task => { s.innerText = task.Result; });

            return s;
        }


        [System.Obsolete("experimental")]
        public static implicit operator IHTMLOutput(Task<XElement> x)
        {
            // first step for databinding?
            // X:\jsc.svn\examples\javascript\Test\TestReplaceHTMLWithXElement\TestReplaceHTMLWithXElement\Application.cs

            var s = new IHTMLOutput { };

            // to be used with first child empty rule?
            x.ContinueWith(
                task =>
                {

                    s.AsXElement().ReplaceWith(task.Result);
                }
            );

            return s;
        }
    }
}
