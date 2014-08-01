using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls.Primitives;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
    // http://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/Windows/Controls/CheckBox.cs

    [Script(Implements = typeof(global::System.Windows.Controls.CheckBox))]
    internal class __CheckBox : __ToggleButton
    {
        public readonly IHTMLDiv InternalSprite = new IHTMLDiv();
        public readonly IHTMLInput InternalCheckBox;

        public override IHTMLElement InternalGetDisplayObject()
        {
            return InternalSprite;
        }


        public __CheckBox()
        {
            //InternalSprite.style.width = "600px";
            //InternalSprite.style.height = "400px";

            InternalSprite.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;


            InternalSprite.style.left = "0px";
            InternalSprite.style.top = "0px";


            this.InternalCheckBox = new IHTMLInput(Shared.HTMLInputTypeEnum.checkbox);
            InternalSprite.appendChild(this.InternalCheckBox);
        }

        #region InternalContent
        object InternalContent;

        protected override object InternalGetContent()
        {
            return InternalContent;
        }


        protected override void InternalSetContent(object e)
        {
            // removing "this." will fault MS csc
            this.InternalContent = e;

            var InternalContentUIElement = e as __UIElement;
            var InternalContent = InternalContentUIElement.InternalGetDisplayObjectDirect();

            InternalContent.style.display = DOM.IStyle.DisplayEnum.inline_block;

            var label = InternalContent.firstChild;

            if (label != null)
                if (label.nodeName.ToLower() == "label")
                {
                    InternalCheckBox.EnsureID();

                    ((IHTMLLabel)label).htmlFor = InternalCheckBox.id;
                }

            InternalSprite.appendChild(InternalContent);

            InternalContent.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;

        }
        #endregion

        protected override bool? InternalGetIsChecked()
        {
            return this.InternalCheckBox.@checked;
        }

        protected override void InternalSetIsChecked(bool? e)
        {
            this.InternalCheckBox.@checked = (bool)e;
        }
    }
}
