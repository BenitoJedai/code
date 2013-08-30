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
            //        VerifyError: Error #1030: Stack depth is unbalanced. 1 != 0.

            //at Function/http://adobe.com/AS3/2006/builtin::apply()
            //at ScriptCoreLib.Shared.BCLImplementation.System::__Action/Invoke_4ebbe596_06001ab0()[U:\web\ScriptCoreLib\Shared\BCLImplementation\System\__Action.as:30]
            //at FlashSpaceInvaders.ActionScript.StarShips::EnemyCloud___c__DisplayClass13/__ctor_b__a_a19717b2_0600014a()[U:\web\FlashSpaceInvaders\ActionScript\StarShips\EnemyCloud___c__DisplayClass13.as:79]
            //at Function/http://adobe.com/AS3/2006/builtin::apply()
            //at ScriptCoreLib.Shared.BCLImplementation.System::__Action_1/Invoke_4ebbe596_06001ab4()[U:\web\ScriptCoreLib\Shared\BCLImplementation\System\__Action_1.as:30]
            //at FlashSpaceInvaders.ActionScript::Property_1/set Value()[U:\web\FlashSpaceInvaders\ActionScript\Property_1.as:77]
            //at FlashSpaceInvaders.ActionScript::Game/__ctor_b__2d_a19717b2_0600001c()[U:\web\FlashSpaceInvaders\ActionScript\Game.as:497]
            //at Function/http://adobe.com/AS3/2006/builtin::apply()
            //at ScriptCoreLib.Shared.BCLImplementation.System::__Action/Invoke_4ebbe596_06001ab0()[U:\web\ScriptCoreLib\Shared\BCLImplementation\System\__Action.as:30]
            //at FlashSpaceInvaders.ActionScript::Game()[U:\web\FlashSpaceInvaders\ActionScript\Game.as:329]
            //at FlashSpaceInvadersApp::ApplicationSprite/PlaySinglePlayer_a19717b2_06000002()[U:\web\FlashSpaceInvadersApp\ApplicationSprite.as:39]
            //at FlashSpaceInvadersApp::ApplicationSprite()[U:\web\FlashSpaceInvadersApp\ApplicationSprite.as:26]




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
