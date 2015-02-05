using ScriptCoreLib;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLHRElement.idl

    // http://www.w3schools.com/tags/tag_hr.asp

    /// <summary>
    /// However, the tag may still be displayed as a horizontal rule in visual browsers, but is now defined in semantic terms, rather than presentational terms.
    /// </summary>
    [Script(InternalConstructor = true)]
    public class IHTMLHorizontalRule : IHTMLElement
    {
        // X:\jsc.svn\examples\javascript\audio\StandardOscillator\StandardOscillator\Application.cs



        #region Constructor

        public IHTMLHorizontalRule()
        {
            // InternalConstructor
        }

        static IHTMLHorizontalRule InternalConstructor()
        {
            return (IHTMLHorizontalRule)((object)new IHTMLElement(HTMLElementEnum.hr));
        }

        #endregion
    }
}
