using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;


namespace CardGames.source.js
{
    [Script]
    class CardGameSoundManager : SoundManager
    {

        #region sounds

        public void PlaySoundNoMoveFound()
        {
            this.Play("fx/sounds/lose.wav");
        }

        public void PlaySoundDeal()
        {
            this.Play("fx/sounds/deal.wav");
        }

        public void PlaySoundDrag()
        {
            this.Play("fx/sounds/drag.wav");
        }

        public void PlaySoundDrop()
        {
            this.Play("fx/sounds/click.wav");
        }

        public void PlaySoundWin()
        {
            this.Play("fx/sounds/win.wav");
        }

        #endregion

    }
}
