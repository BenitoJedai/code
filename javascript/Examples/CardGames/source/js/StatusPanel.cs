using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

using ScriptCoreLib.JavaScript.Cards;

namespace CardGames.source.js
{
    [Script]
    public class StatusPanel
    {
        public readonly IHTMLDiv Control = new IHTMLDiv();

        protected IHTMLDiv SoundSettingDiv;
        protected IHTMLInput SoundSettingDivCheckbox = new IHTMLInput(HTMLInputTypeEnum.checkbox);


        public event EventHandler SoundSettingChanged;

        public bool UseSounds
        {
            get
            {
                return SoundSettingDivCheckbox.@checked;
            }
            set
            {
                SoundSettingDivCheckbox.@checked = value;

                Helper.Invoke(SoundSettingChanged);
            }
        }

        public StatusPanel()
        {
            Control.style.color = Color.White;

            SoundSettingDiv = new IHTMLDiv(SoundSettingDivCheckbox, new IHTMLLabel("use sounds", SoundSettingDivCheckbox));
            SoundSettingDiv.style.textAlign = IStyle.TextAlignEnum.center;
            SoundSettingDiv.onclick +=
                delegate
                {
                    Helper.Invoke(SoundSettingChanged);
                };


            Native.Document.body.appendChild(Control);

            Update();
        }

        public virtual void Update()
        {

        }

        public Point Position;

        public void MoveTo(Point p)
        {
            Position = p;

            Control.SetCenteredLocation(p);
        }

        public bool Ready;


        private bool _Visible;

        public bool Visible
        {
            get { return _Visible; }
            set
            {
                _Visible = value;

                Control.Show(_Visible);
            }
        }
    }
}
