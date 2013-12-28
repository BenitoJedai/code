using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace SVGNavigationTiming
{

    static class CSSDataGridView
    {
        static public CSSStyleRuleMonkier ContentTable
        {

            get
            {
                return IStyleSheet.all["." + __DataGridView.__ContentTable_className];
            }
        }

        static public CSSStyleRuleMonkier __ContentTable_css_tr
        {

            get
            {
                return ContentTable
                        [IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr];

            }
        }

        static public CSSStyleRuleMonkier __ContentTable_css_td
        {

            get
            {
                return ContentTable
                        [IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr][IHTMLElement.HTMLElementEnum.td];

            }
        }
    }
}
