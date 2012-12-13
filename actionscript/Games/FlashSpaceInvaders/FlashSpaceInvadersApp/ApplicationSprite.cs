using FlashSpaceInvaders.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FlashSpaceInvadersApp
{
    public sealed class ApplicationSprite : Sprite
    {
        public const int DefaultWidth = Game.DefaultWidth;
        public const int DefaultHeight = Game.DefaultHeight;

        //public const int DefaultWidth = Game.DefaultWidth + NonobaClient.NonobaChatWidth;

        // todo: add http://gimme.badsectoracula.com/flashmodplayer/modplayer.html

        // http://zproxy.wordpress.com/2007/03/03/jsc-space-invaders/

        // http://cdexos.sourceforge.net/?q=download

        // http://en.wikipedia.org/wiki/Space_Invaders


        public const string MochiAdKey = "5ea4cb6ec61420b1";

        const string Description = "A remake of the classic Space Invaders.";

        const string Instructions = "Use arrow keys or mouse to move around. Space to shoot.";

        public ApplicationSprite()
        {
            //PlayMultiPlayerWithMochi();
            //PlaySplitScreen();
            PlaySinglePlayer();

        }

        //public override DisplayObject CreateInstance()
        //{
        //    throw new NotImplementedException();
        //}

        //private void PlayMultiPlayerWithMochi()
        //{
        //    this.InvokeWhenStageIsReady(
        //        delegate
        //        {
        //            _mochiads_game_id = MochiAdKey;

        //            showPreGameAd(
        //                delegate
        //                {
        //                    PlayMultiPlayer();
        //                }
        //            );
        //        }
        //    );
        //}

        //private void PlaySplitScreen()
        //{
        //    var s = new SplitScreen();

        //    s.Righty.Element.x = Game.DefaultWidth;

        //    s.Lefty.Element.AttachTo(this);
        //    s.Lefty.Map.PlayerInput.MovementArrows.Enabled = false;

        //    s.Righty.Element.AttachTo(this);
        //    s.Righty.Map.PlayerInput.MovementWASD.Enabled = false;


        //}

        //private void PlayMultiPlayer()
        //{
        //    var g = new MultiPlayer.NonobaClient();

        //    //g.Element.x = (DefaultWidth - MultiPlayer.NonobaClient.DefaultWidth) / 2;

        //    g.Element.AttachTo(this);
        //}

        void PlaySinglePlayer()
        {
            var g = new Game
            {
                //x = DefaultWidth / 4
            };

            g.AttachTo(this);
        }
    }
}
