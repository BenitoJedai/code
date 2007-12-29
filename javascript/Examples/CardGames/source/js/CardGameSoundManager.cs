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
            this.Play("assets/CardGames/lose.wav");
        }

        public void PlaySoundDeal()
        {
            this.Play("assets/CardGames/deal.wav");
        }

        public void PlaySoundDrag()
        {
            this.Play("assets/CardGames/drag.wav");
        }

        public void PlaySoundDrop()
        {
            this.Play("assets/CardGames/click.wav");
        }

        public void PlaySoundWin()
        {
            this.Play("assets/CardGames/win.wav");
        }

        #endregion

    }
}
