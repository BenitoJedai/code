using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{



    [Script(Implements = typeof(global::System.Windows.Forms.TextBoxBase))]
    public class __TextBoxBase : __Control
    {
        // see also:
        // X:\jsc.svn\core\ScriptCoreLib.Avalon\ScriptCoreLib.Avalon\JavaScript\BCLImplementation\System\Windows\Controls\TextBox.cs
        // X:\jsc.svn\examples\javascript\forms\FormsProjectTemplateExperiment\FormsProjectTemplateExperiment\ApplicationControl.cs

        public IHTMLDiv InternalContainer;

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


        public override void InternalAddGotFocus(EventHandler e)
        {

            this.InternalGetTextField().onfocus +=
                delegate
            {
                e(this, new EventArgs());
            };
        }

        public override void InternalAddLostFocus(EventHandler e)
        {

            this.InternalGetTextField().onblur +=
                delegate
            {
                e(this, new EventArgs());
            };
        }

        public __TextBoxBase()
        {
            #region InternalContainer
            this.InternalContainer = new IHTMLDiv();

            this.InternalContainer.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            this.InternalContainer.name = "__TextBox";

            this.InternalContainer.style.left = "0px";
            this.InternalContainer.style.top = "0px";

            // do we create any new havoc?
            this.InternalContainer.style.zIndex = 0;
            #endregion


            #region InternalTextField_ShadowContainer
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

            }.AttachTo(this.InternalContainer);

            // boxstyle. will it break layout?
            this.InternalTextField.style.padding = "0.4em";

            //Need to test/document
            this.InternalNameChanged += delegate
            {
                this.InternalTextField.name = InternalName;
                if (InternalName.ToUpper().Contains(("email").ToUpper()))
                    this.InternalTextField.type = Shared.HTMLInputTypeEnum.email;
                if (InternalName.ToUpper().Contains(("phone").ToUpper()))
                    this.InternalTextField.type = Shared.HTMLInputTypeEnum.tel;
            };

            this.InternalSetDefaultFont();

            this.InternalTextField.style.position = IStyle.PositionEnum.absolute;
            this.InternalTextField.style.margin = "0";
            this.InternalTextField.style.paddingTop = "0";
            this.InternalTextField.style.paddingBottom = "0";

            // what about inheritance chain?
            // X:\jsc.svn\examples\javascript\forms\Test\CSSLastTextBox\CSSLastTextBox\Application.cs
            IStyleSheet.all[typeof(TextBox)][IHTMLElement.HTMLElementEnum.input].style.border = "1px solid gray";
            #endregion

            // what about padding?
            // X:\jsc.svn\examples\javascript\forms\Test\TestTextBoxPadding\TestTextBoxPadding\Application.cs
            // http://stackoverflow.com/questions/628500/can-i-stop-100-width-text-boxes-from-extending-beyond-their-containers/628912#628912

            //box-sizing: border-box;
            (this.InternalTextField.style as dynamic).boxSizing = "border-box";
            //this.InternalTextField.style.boxSizing = "border-box";
            this.InternalTextField.style.width = "100%";
            this.InternalTextField.style.height = "100%";

            //this.ClientSizeChanged +=
            //    delegate
            //    {
            //        this.InternalTextField.style.width = this.ClientSize.Width + "px";
            //        this.InternalTextField.style.height = this.ClientSize.Height + "px";
            //    };

            #region InternalRaiseTextChanged
            Action InternalAutoSizeUpdate =
                delegate
            {
                if (this.InternalTextField_Shadow == null)
                    return;

                //InternalAutoSizeToText(this.InternalTextField.value);
            };

            this.InternalTextField.onchange +=
                delegate
            {
                InternalAutoSizeUpdate();
                this.InternalRaiseTextChanged();
            };

            this.InternalTextField.onkeyup +=
                delegate
            {
                InternalAutoSizeUpdate();
                this.InternalRaiseTextChanged();
            };
            #endregion




            this.Size = new global::System.Drawing.Size(100, 20);


        }

        protected override void InternalSetForeColor(global::System.Drawing.Color value)
        {
            this.InternalGetTextField().style.color = value.ToString();
        }

        protected override void InternalSetBackgroundColor(global::System.Drawing.Color value)
        {
            this.InternalGetTextField().style.backgroundColor = value.ToString();
        }

        public override void InternalSetFont(global::System.Drawing.Font value)
        {
            this.InternalGetTextField().style.font = this.Font.ToCssString();
        }


        public bool InternalMultiline;
        public bool Multiline
        {
            get
            {
                return InternalMultiline;
            }
            set
            {
                InternalMultiline = value;

                InternalSetAcceptsReturn(value);
            }
        }

        public Action InternalUpdateScrollBars;

        #region InternalSetAcceptsReturn
        public
            //override
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

                        // X:\jsc.svn\examples\javascript\forms\SQLiteConsoleExperiment\SQLiteConsoleExperiment\ApplicationControl.cs

                        //refresh

                        this.InternalTextField_MultiLine.style.margin = "0";
                        this.InternalTextField_MultiLine.style.paddingTop = "0";
                        this.InternalTextField_MultiLine.style.paddingBottom = "0";
                        this.InternalTextField_MultiLine.style.position = IStyle.PositionEnum.absolute;
                        this.InternalTextField_MultiLine.style.SetLocation(0, 0);
                        this.InternalTextField_MultiLine.style.overflow = IStyle.OverflowEnum.hidden;
                        this.InternalTextField_MultiLine.style.resize = "none";
                        this.InternalTextField_MultiLine.style.outline = "none";
                        // http://www.electrictoolbox.com/disable-textarea-resizing-safari-chrome/


                        this.BackColor = this.BackColor;
                        this.ForeColor = this.ForeColor;
                        this.BorderStyle = this.BorderStyle;


                        if (InternalUpdateScrollBars != null)
                            InternalUpdateScrollBars();

                        var p = this.InternalTextField.parentNode;

                        // we should actually just notify our collection about this change
                        // but instead we do the exchange here at the moment 
                        if (p != null)
                        {
                            p.insertBefore(this.InternalTextField_MultiLine, this.InternalTextField);

                            p.removeChild(this.InternalTextField);

                            //Console.WriteLine("InternalSetAcceptsReturn!!");
                        }



                        this.InternalTextField_MultiLine.style.width = "100%";
                        this.InternalTextField_MultiLine.style.height = "100%";

                        // lets apply current font - probably is the default font...
                        //this.FontFamily = this.InternalFontFamily;
                        //this.FontSize = this.InternalFontSize;
                        this.InternalSetFont(this.Font);

                        Action InternalAutoSizeUpdate =
                            delegate
                        {
                            if (this.InternalTextField_Shadow == null)
                                return;

                            //this.InternalAutoSizeToText(this.InternalTextField_MultiLine.value);
                        };

                        this.InternalTextField_MultiLine.onchange +=
                             delegate
                        {
                            InternalAutoSizeUpdate();
                            this.InternalRaiseTextChanged();
                        };

                        this.InternalTextField_MultiLine.onkeyup +=
                             delegate
                        {
                            InternalAutoSizeUpdate();
                            this.InternalRaiseTextChanged();
                        };

                        //InternalUpdateBackground();
                        //InternalUpdateBorderThickness();
                        //InternalUpdateForeground();

                        return;
                    }

            throw new NotImplementedException();
        }
        #endregion


        // public override IHTMLElement InternalGetDisplayObject()
        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return this.InternalContainer;
            }
        }

        public void ScrollToCaret()
        {
            // ? where is our caret?
            this.InternalTextField_MultiLine.ScrollToBottom();
        }

        public int SelectionStart
        {
            set { this.InternalTextField_MultiLine.SelectionStart = value; }
            get { return this.InternalTextField_MultiLine.SelectionStart; }
        }

        public void Select(int start, int length)
        {
            SelectionStart = start;
        }


        #region BorderStyle
        public BorderStyle InternalBorderStyle;
        public BorderStyle BorderStyle
        {
            get
            { return InternalBorderStyle; }

            set
            {
                InternalBorderStyle = value;

                // tested by
                // X:\jsc.svn\examples\javascript\forms\SQLiteConsoleExperiment\SQLiteConsoleExperiment\ApplicationControl.cs

                if (value == global::System.Windows.Forms.BorderStyle.None)
                {
                    this.InternalGetTextField().style.borderWidth = "0px";
                    this.InternalGetTextField().style.padding = "0px";

                }
            }
        }
        #endregion



        public bool ReadOnly
        {
            get
            {
                if (this.InternalMultiline)

                    return this.InternalTextField_MultiLine.readOnly;
                return this.InternalTextField.readOnly;


            }
            set
            {
                // x:\jsc.svn\examples\javascript\forms\test\testreadonly\testreadonly\applicationcontrol.cs

                if (this.InternalMultiline)
                    this.InternalTextField_MultiLine.readOnly = value;
                else
                    this.InternalTextField.readOnly = value;
            }
        }

        public void AppendText(string text)
        {
            this.Text += text;
        }

        public void Clear()
        {
            this.Text = "";
        }

        int InternalTextKnownLength = 0;
        bool InternalTextNewLineMangling = false;


        public override string Text
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
                    //this.InternalAutoSizeToText(value);
                }


                if (this.InternalTextField_MultiLine != null)
                {
                    // we need to detect newline mangling

                    var __value = value;
                    if (__value == null)
                        __value = "";

                    this.InternalTextField_MultiLine.value = __value;

                    #region detect newline changes
                    this.InternalTextKnownLength = this.InternalTextField_MultiLine.value.Length;
                    this.InternalTextNewLineMangling = this.InternalTextKnownLength < __value.Length;
                    #endregion

                    return;
                }

                InternalTextField.value = value;

                OnTextChanged(this, new EventArgs());
            }
        }


        public bool InternalWordWrap;
        public bool WordWrap
        {
            get
            {
                return InternalWordWrap;
            }
            set
            {
                InternalWordWrap = value;
                if (value)
                    this.InternalTextField_MultiLine.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.pre;
                else
                    this.InternalTextField_MultiLine.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;

            }
        }

        #region
        static public implicit operator TextBoxBase(__TextBoxBase e)
        {
            return (TextBoxBase)(object)e;
        }

        static public implicit operator __TextBoxBase(TextBoxBase e)
        {
            return (__TextBoxBase)(object)e;
        }
        #endregion
    }
}
