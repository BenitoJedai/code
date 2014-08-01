using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls.Primitives;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System.Windows;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
    // http://msdn.microsoft.com/en-us/library/system.windows.controls.textbox(v=vs.110).aspx
    // http://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.textbox
    // http://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/Windows/Controls/TextBox.cs

    [Script(Implements = typeof(global::System.Windows.Controls.TextBox))]
    internal class __TextBox : __TextBoxBase
    {
        // or shall the element/shadow be contextual/ meaning
        // to activate only if inside a special host element?
        // this would allow name sharing?

        //public const string ElementName = "Avalon-TextBox";

        // chrome seems to prefer lowe case in inspector..
        public const string ElementName = "avalon-textbox";


        // shall this be the first type to have shadow dom be applied to?
        // first little used Avalon, then Forms?



        // X:\jsc.svn\examples\javascript\Avalon\Test\TestShadowTextBox\TestShadowTextBox\ApplicationCanvas.cs

        // see also:
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\TextBoxBase.cs


        static __TextBox()
        {
            // test against worker mode
            if (Native.document == null)
                return;

            // X:\jsc.svn\examples\javascript\Test\TestShadowIFrame\TestShadowIFrame\Application.cs
            // http://dart.googlecode.com/svn/branches/bleeding_edge/dart/tests/html/custom/document_register_type_extensions_test.dart
            // dart is already doing it.

            // https://code.google.com/p/chromium/issues/detail?id=320506
            /// http://www.w3.org/TR/custom-elements/#concepts
            


            // this wont actually work for chrome extension running inside tab
            Native.document.registerElement(

                // could we register multiple names?
                // it should be hand writable?

                //name: "global--System-Windows-Controls-TextBox",

                // what about name clashing?
                //name: "XAML-TextBox",

                // forms textbox 
                name: ElementName,

                createdCallback:
                    (IHTMLElement e) =>
                    {
                        // um. this would be the new way do do ctor.
                        // like we do for Application(html) already?

                    }
            );
        }

        // what if we want our own element type too?
        public IHTMLElement InternalContainer;
        public ShadowRoot InternalContainer_shadow;

        public IHTMLSpan InternalTextField_Shadow;
        public IHTMLDiv InternalTextField_ShadowContainer;

        #region InternalGetTextField
        public IHTMLInput InternalTextField;
        public IHTMLTextArea InternalTextField_MultiLine;

        public IHTMLElement InternalGetTextField()
        {
            if (InternalTextField_MultiLine != null)
                return InternalTextField_MultiLine;

            return InternalTextField;
        }
        #endregion


        public __TextBox()
        {
            #region InternalContainer
            //this.InternalContainer = new IHTMLDiv();
            this.InternalContainer = new IHTMLElement(ElementName);

            // are we excluding older browsers? does the app analyzer let the developer know what devices can be used now?
            this.InternalContainer_shadow = this.InternalContainer.createShadowRoot();
            // if we only create shadow, empty, other children dissapear dont they. as there are no insertion points

            this.InternalContainer.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;

            // in Forms we use typeof() because we see Component. WPF objects are more like XElements..
            this.InternalContainer.name = "__TextBox";

            this.InternalContainer.style.left = "0px";
            this.InternalContainer.style.top = "0px";

            // do we create any new havoc?
            this.InternalContainer.style.zIndex = 0;
            #endregion


            #region InternalTextField_ShadowContainer

            // when is it attached?
            this.InternalTextField_ShadowContainer = new IHTMLDiv();


            this.InternalTextField_ShadowContainer.style.position = IStyle.PositionEnum.absolute;
            this.InternalTextField_ShadowContainer.style.overflow = IStyle.OverflowEnum.hidden;
            this.InternalTextField_ShadowContainer.style.SetSize(0, 0);

            this.InternalTextField_Shadow = new IHTMLSpan();
            this.InternalTextField_Shadow.AttachTo(this.InternalTextField_ShadowContainer);
            this.InternalTextField_Shadow.style.whiteSpace = IStyle.WhiteSpaceEnum.pre;
            this.InternalTextField_Shadow.style.display = IStyle.DisplayEnum.inline_block;
            this.InternalTextField_Shadow.style.position = IStyle.PositionEnum.absolute;
            #endregion

            #region InternalTextField
            this.InternalTextField = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.text)
            {

                //}.AttachTo(this.InternalContainer);
                // public static T AttachTo<T>(this T e, IHTMLElement c) where T : INodeConvertible<IHTMLElement>;
            }.AttachTo(this.InternalContainer_shadow);

            this.InternalSetDefaultFont();

            this.InternalTextField.style.position = IStyle.PositionEnum.absolute;
            this.InternalTextField.style.margin = "0";
            this.InternalTextField.style.paddingTop = "0";
            this.InternalTextField.style.paddingBottom = "0";
            this.InternalTextField.style.border = "1px solid gray";
            #endregion


            Action InternalAutoSizeUpdate =
                delegate
                {
                    if (this.InternalTextField_Shadow == null)
                        return;

                    InternalAutoSizeToText(this.InternalTextField.value);
                };

            this.InternalTextField.onchange +=
                delegate
                {
                    InternalAutoSizeUpdate();
                };

            this.InternalTextField.onkeyup +=
                delegate
                {
                    InternalAutoSizeUpdate();
                };
        }

        #region InternalSetAcceptsReturn
        public
            override
            void InternalSetAcceptsReturn(bool value)
        {
            if (value)
                if (InternalTextField != null)
                    if (InternalTextField_MultiLine == null)
                    {
                        // known situation

                        this.InternalTextField_MultiLine = new IHTMLTextArea(this.InternalTextField.value)
                        {
                            readOnly = this.InternalTextField.readOnly,
                            wrap = "off"
                        };
                        this.InternalTextField_MultiLine.style.margin = "0";
                        this.InternalTextField_MultiLine.style.paddingTop = "0";
                        this.InternalTextField_MultiLine.style.paddingBottom = "0";
                        this.InternalTextField_MultiLine.style.position = IStyle.PositionEnum.absolute;
                        this.InternalTextField_MultiLine.style.SetLocation(0, 0);
                        this.InternalTextField_MultiLine.style.overflow = IStyle.OverflowEnum.hidden;
                        this.InternalTextField_MultiLine.style.resize = "none";


                        // is it tested with the shadow dom?
                        var p = this.InternalTextField.parentNode;

                        // we should actually just notify our collection about this change
                        // but instead we do the exchange here at the moment 
                        if (p != null)
                        {
                            p.insertBefore(this.InternalTextField_MultiLine, this.InternalTextField);

                            p.removeChild(this.InternalTextField);

                            //Console.WriteLine("InternalSetAcceptsReturn!!");
                        }

                        // lets apply current font - probably is the default font...
                        this.FontFamily = this.InternalFontFamily;
                        this.FontSize = this.InternalFontSize;

                        Action InternalAutoSizeUpdate =
                            delegate
                            {
                                if (this.InternalTextField_Shadow == null)
                                    return;

                                this.InternalAutoSizeToText(this.InternalTextField_MultiLine.value);
                            };

                        this.InternalTextField_MultiLine.onchange +=
                             delegate
                             {
                                 InternalAutoSizeUpdate();
                             };

                        this.InternalTextField_MultiLine.onkeyup +=
                             delegate
                             {
                                 InternalAutoSizeUpdate();
                             };

                        InternalUpdateBackground();
                        InternalUpdateBorderThickness();
                        InternalUpdateForeground();

                        return;
                    }

            throw new NotImplementedException();
        }
        #endregion


        internal void InternalAutoSizeToText(string value)
        {
            string n = value.Replace("\r", "");

            if (n.EndsWith("\n"))
                n += "\n";

            this.InternalTextField_Shadow.innerText = n;
            this.InternalAutoSizeUpdate();
        }

        public override IHTMLElement InternalGetDisplayObject()
        {
            return this.InternalContainer;
        }


        public override void InternalSetWidth(double value)
        {
            this.InternalContainer.style.width = value + "px";

            if (this.InternalTextField != null)
                this.InternalTextField.style.width = (value + "px");
            if (this.InternalTextField_MultiLine != null)
                this.InternalTextField_MultiLine.style.width = (value + "px");

            InternalDisableAutoSize();
        }

        public override void InternalSetHeight(double value)
        {
            this.InternalContainer.style.height = value + "px";


            if (this.InternalTextField != null)
                this.InternalTextField.style.height = (value + "px");
            if (this.InternalTextField_MultiLine != null)
                this.InternalTextField_MultiLine.style.height = (value + "px");

            InternalDisableAutoSize();

        }

        private void InternalDisableAutoSize()
        {
            this.InternalTextField_ShadowContainer.Orphanize();
            this.InternalTextField_ShadowContainer = null;
            this.InternalTextField_Shadow = null;
        }

        public override double InternalGetWidth()
        {
            var e = this.InternalGetDisplayObject();

            return e.Bounds.Width;
        }

        public override double InternalGetHeight()
        {
            var e = this.InternalGetDisplayObject();

            return e.Bounds.Height;
        }





        #region InternalSetBorderThickness
        internal __Thickness InternalBorderThickness = new __Thickness();
        public override void InternalSetBorderThickness(Thickness value)
        {
            this.InternalBorderThickness = value;

            InternalUpdateBorderThickness();
        }

        private void InternalUpdateBorderThickness()
        {
            if (this.InternalBorderThickness.InternalValue == 0)
            {
                if (this.InternalTextField != null)
                    this.InternalTextField.style.borderWidth = "0";

                if (this.InternalTextField_MultiLine != null)
                    this.InternalTextField_MultiLine.style.borderWidth = "0";


                return;
            }

            if (this.InternalBorderThickness.InternalValue == 1)
            {
                if (this.InternalTextField != null)
                    this.InternalTextField.style.borderWidth = "1px";

                if (this.InternalTextField_MultiLine != null)
                    this.InternalTextField_MultiLine.style.borderWidth = "1px";


                return;
            }

            throw new NotSupportedException();
        }
        #endregion


        #region InternalForeground
        Brush InternalForeground;

        public override Brush InternalGetForeground()
        {
            return InternalForeground;
        }

        public override void InternalSetForeground(Brush value)
        {
            this.InternalForeground = value;

            InternalUpdateForeground();
        }

        private void InternalUpdateForeground()
        {
            var AsSolidColorBrush = this.InternalForeground as SolidColorBrush;

            if (AsSolidColorBrush != null)
            {
                var _SolidColorBrush = (__SolidColorBrush)AsSolidColorBrush;
                __Color _Color = _SolidColorBrush.Color;

                if (this.InternalTextField != null)
                    this.InternalTextField.style.color = _Color;

                if (this.InternalTextField_MultiLine != null)
                    this.InternalTextField_MultiLine.style.color = _Color;

            }
        }
        #endregion

        #region InternalSetBackground
        internal Brush InternalBackground;
        public override void InternalSetBackground(Brush value)
        {
            this.InternalBackground = value;

            InternalUpdateBackground();
        }

        private void InternalUpdateBackground()
        {
            var AsSolidColorBrush = this.InternalBackground as SolidColorBrush;

            if (AsSolidColorBrush != null)
            {
                var _SolidColorBrush = (__SolidColorBrush)AsSolidColorBrush;
                string _Color = (__Color)_SolidColorBrush.Color;

                var IsTransparent = __Color.Equals(__Colors.Transparent, _SolidColorBrush.Color);
                if (IsTransparent)
                {
                    _Color = "transparent";
                }

                if (this.InternalTextField != null)
                    this.InternalTextField.style.backgroundColor = _Color;

                if (this.InternalTextField_MultiLine != null)
                    this.InternalTextField_MultiLine.style.backgroundColor = _Color;


            }
        }
        #endregion


        #region TextChanged
        public override event TextChangedEventHandler TextChanged
        {
            add
            {
                Internal_add_TextChanged(value);
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        private void Internal_add_TextChanged(TextChangedEventHandler value)
        {
            //Console.WriteLine("Internal_add_TextChanged: ");

            var NotifyText = default(string);

            Action Notify =
                delegate
                {
                    //Console.WriteLine("notify: " + this.Text);
                    InternalAutoSizeUpdate();
                    value(this, null);
                };

            var t = new ScriptCoreLib.JavaScript.Runtime.Timer();

            Action Check =
                delegate
                {
                    var Text = this.Text;

                    if (Text == NotifyText)
                        return;

                    NotifyText = Text;
                    Notify();
                };

            t.Tick +=
                delegate
                {
                    Check();
                };

            this.InternalGetTextField().onfocus +=
                delegate
                {
                    //Console.WriteLine("onfocus: ");
                    NotifyText = this.Text;
                    // based on cpu and text length we may choose a larger interval
                    t.StartInterval(1000 / 10);
                };


            this.InternalGetTextField().onblur +=
                delegate
                {
                    //Console.WriteLine("onblur: ");
                    t.Stop();
                    NotifyText = null;
                };

            this.InternalGetTextField().onchange +=
                delegate
                {
                    Check();
                };


            this.InternalGetTextField().onkeyup +=
             delegate
             {
                 Check();
             };

            this.InternalGetTextField().onkeydown +=
             delegate
             {
                 Check();
             };
        }
        #endregion




        int InternalTextKnownLength = 0;
        bool InternalTextNewLineMangling = false;

        #region Text
        public string Text
        {
            get
            {
                if (this.InternalTextField_MultiLine != null)
                {
                    #region detect newline changes
                    var c = this.InternalTextField.value.Length;

                    if (c != this.InternalTextKnownLength)
                    {
                        this.InternalTextKnownLength = c;
                        this.InternalTextNewLineMangling = this.InternalTextField_MultiLine.value.Count("\n") > this.InternalTextField_MultiLine.value.Count(Environment.NewLine);
                    }
                    #endregion


                    if (this.InternalTextNewLineMangling)
                        return this.InternalTextField_MultiLine.value.Replace("\n", Environment.NewLine);

                    return this.InternalTextField_MultiLine.value;
                }

                return InternalTextField.value;
            }
            set
            {
                if (this.InternalTextField_Shadow != null)
                {
                    this.InternalAutoSizeToText(value);
                }


                if (this.InternalTextField_MultiLine != null)
                {
                    // we need to detect newline mangling

                    this.InternalTextField_MultiLine.value = value;

                    #region detect newline changes
                    this.InternalTextKnownLength = this.InternalTextField_MultiLine.value.Length;
                    this.InternalTextNewLineMangling = this.InternalTextKnownLength < value.Length;
                    #endregion

                    return;
                }

                InternalTextField.value = value;

            }
        }

        private void InternalAutoSizeUpdate()
        {
            if (this.InternalTextField_Shadow == null)
                return;

            // attaching to the dom to do just in time measureing
            var f = Native.Document.body.firstChild;
            if (f == null)
                Native.Document.body.appendChild(this.InternalTextField_ShadowContainer);
            else
                Native.Document.body.insertBefore(this.InternalTextField_ShadowContainer, f);

            var w = this.InternalTextField_Shadow.scrollWidth;
            var h = this.InternalTextField_Shadow.scrollHeight;

            this.InternalTextField_ShadowContainer.Orphanize();

            // if we are not attached to the DOM tree we wont get any values.

            if (w < 20)
                w = 20;

            if (h < 20)
                h = 20;

            if (this.InternalTextField != null)
                this.InternalTextField.style.SetSize(
                    w,
                    h
                );

            if (this.InternalTextField_MultiLine != null)
                this.InternalTextField_MultiLine.style.SetSize(
                    w,
                    h
                );
        }
        #endregion

        public override void InternalAppendText(string textData)
        {
            this.Text += textData;
        }

        public static implicit operator __TextBox(TextBox e)
        {
            return (__TextBox)(object)e;
        }

        public override void InternalSetIsReadOnly(bool value)
        {
            if (this.InternalTextField_MultiLine != null)
            {
                this.InternalTextField_MultiLine.readOnly = value;

                return;
            }


            this.InternalTextField.readOnly = value;
        }

        #region TextWrapping
        public TextWrapping TextWrapping
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                if (this.InternalTextField_MultiLine == null)
                    new NotSupportedException();

                // http://www.idocs.com/tags/forms/_TEXTAREA_WRAP.html
                // http://msdn.microsoft.com/en-us/library/ms535152(VS.85).aspx

                if (value == TextWrapping.NoWrap)
                {
                    this.InternalTextField_MultiLine.wrap = "off";
                    //this.InternalTextField_MultiLine.style.whiteSpace = IStyle.WhiteSpaceEnum.nowrap;

                    return;
                }

                if (value == TextWrapping.Wrap)
                {
                    // Default. Text is displayed with wordwrapping and submitted without carriage returns and line feeds.
                    this.InternalTextField_MultiLine.wrap = "soft";
                    //this.InternalTextField_MultiLine.style.whiteSpace = IStyle.WhiteSpaceEnum.pre;

                    return;
                }

                throw new NotSupportedException();
            }
        }
        #endregion


        #region TextAlignment
        public TextAlignment TextAlignment
        {
            get
            {
                var align = this.InternalGetTextField().style.textAlign;

                // jsc will replace local enum assignment with their string values...
                var right = IStyle.TextAlignEnum.right;
                if (align == right)
                    return TextAlignment.Right;

                var center = IStyle.TextAlignEnum.center;
                if (align == center)
                    return TextAlignment.Center;

                var justify = IStyle.TextAlignEnum.justify;
                if (align == justify)
                    return TextAlignment.Justify;

                return TextAlignment.Left;
            }
            set
            {
                // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/text/TextFormatAlign.html


                var s = this.InternalGetTextField().style;


                if (value == TextAlignment.Left)
                    s.textAlign = IStyle.TextAlignEnum.left;
                else if (value == TextAlignment.Right)
                    s.textAlign = IStyle.TextAlignEnum.right;
                else if (value == TextAlignment.Center)
                    s.textAlign = IStyle.TextAlignEnum.center;
                else if (value == TextAlignment.Justify)
                    s.textAlign = IStyle.TextAlignEnum.justify;
            }
        }
        #endregion

        public void Clear()
        {
            this.Text = "";
        }



        #region InternalSetFontFamily
        FontFamily InternalFontFamily;


        public override void InternalSetFontFamily(FontFamily value_)
        {
            if (value_ == null)
                return;

            InternalFontFamily = value_;

            var value = ((__FontFamily)(object)value_).InternalFamilyName;


            if (this.InternalTextField != null)
                this.InternalTextField.style.fontFamily = (IStyle.FontFamilyEnum)(object)(value);

            if (this.InternalTextField_MultiLine != null)
                this.InternalTextField_MultiLine.style.fontFamily = (IStyle.FontFamilyEnum)(object)(value);

            if (this.InternalTextField_Shadow != null)
                this.InternalTextField_Shadow.style.fontFamily = (IStyle.FontFamilyEnum)(object)(value);

            InternalAutoSizeUpdate();

        }
        #endregion

        #region InternalSetFontSize
        double InternalFontSize;
        public override void InternalSetFontSize(double value)
        {
            InternalFontSize = value;

            if (this.InternalTextField != null)
                this.InternalTextField.style.fontSize = (Convert.ToInt32(value) + "px");

            if (this.InternalTextField_MultiLine != null)
                this.InternalTextField_MultiLine.style.fontSize = (Convert.ToInt32(value) + "px");

            if (this.InternalTextField_Shadow != null)
                this.InternalTextField_Shadow.style.fontSize = (Convert.ToInt32(value) + "px");

            InternalAutoSizeUpdate();

        }
        #endregion
    }
}
