using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.TextBox))]
    public class __TextBox : __TextBoxBase
    {
        // should we listen for enter key?
        public bool AcceptsReturn { get; set; }

        #region AutoCompleteMode
        public AutoCompleteMode InternalAutoCompleteMode;
        public AutoCompleteMode AutoCompleteMode { get { return InternalAutoCompleteMode; } set { InternalAutoCompleteMode = value; if (InternalAutoCompleteModeChanged != null)InternalAutoCompleteModeChanged(); } }
        public event Action InternalAutoCompleteModeChanged;
        #endregion

        #region AutoCompleteSource
        public AutoCompleteSource InternalAutoCompleteSource;
        public AutoCompleteSource AutoCompleteSource { get { return InternalAutoCompleteSource; } set { InternalAutoCompleteSource = value; if (InternalAutoCompleteSourceChanged != null)InternalAutoCompleteSourceChanged(); } }
        public event Action InternalAutoCompleteSourceChanged;
        #endregion

        public __TextBox()
        {
            this.ScrollBars = global::System.Windows.Forms.ScrollBars.None;
            this.InternalAutoCompleteModeChanged += delegate
            {
                if (this.InternalAutoCompleteMode == global::System.Windows.Forms.AutoCompleteMode.None)
                    this.InternalTextField.autocomplete = "off";
                else
                    this.InternalTextField.autocomplete = "on";
            };
        }

        #region CharacterCasing
        CharacterCasing InternalCharacterCasing;
        public CharacterCasing CharacterCasing
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestTextBoxPadding\TestTextBoxPadding\Application.cs

            set
            {
                InternalCharacterCasing = value;

                if (InternalCharacterCasing == global::System.Windows.Forms.CharacterCasing.Upper)
                    this.InternalGetTextField().style.textTransform = DOM.IStyle.TextTransformEnum.uppercase;
                else if (InternalCharacterCasing == global::System.Windows.Forms.CharacterCasing.Lower)
                    this.InternalGetTextField().style.textTransform = DOM.IStyle.TextTransformEnum.lowercase;
                else if (InternalCharacterCasing == global::System.Windows.Forms.CharacterCasing.Normal)
                    this.InternalGetTextField().style.textTransform = DOM.IStyle.TextTransformEnum.none;
            }
            get
            {
                return InternalCharacterCasing;
            }
        }
        #endregion

        private HorizontalAlignment _TextAlign;

        public HorizontalAlignment TextAlign
        {
            get { return _TextAlign; }
            set { _TextAlign = value; }
        }

        public override bool Enabled
        {
            get
            {
                return !InternalTextField.disabled;
            }
            set
            {
                if (InternalTextField_MultiLine != null)
                    InternalTextField_MultiLine.disabled = !value;

                InternalTextField.disabled = !value;
            }
        }


        #region ScrollBars
        ScrollBars InternalScrollBars;

        public ScrollBars ScrollBars
        {
            get
            {
                return InternalScrollBars;
            }
            set
            {
                InternalScrollBars = value;

                InternalUpdateScrollBars = delegate
                {
                    if (value == ScrollBars.Both)
                    {
                        this.InternalGetTextField().style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.scroll;

                    }

                    if (value == ScrollBars.None)
                    {
                        this.InternalGetTextField().style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;
                    }
                    else
                    {
                        this.InternalGetTextField().style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.auto;
                    }
                };


                InternalUpdateScrollBars();


            }
        }
        #endregion
    }
}
