using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace TestLogicArrayNullOrLength
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // ok if  null
            _SpritesFromPOV = new object[1];

            //Sprites = null;
            Sprites = new List<object>();

            //        TypeError: Error #1009: Cannot access a property or method of a null object reference.
            //at TestLogicArrayNullOrLength::ApplicationSprite/UpdatePOV_39d0131d_06000002()[U:\web\TestLogicArrayNullOrLength\ApplicationSprite.as:43]
            //at TestLogicArrayNullOrLength::ApplicationSprite()[U:\web\TestLogicArrayNullOrLength\ApplicationSprite.as:30]


            UpdatePOV();
        }

        protected void UpdatePOV()
        {

            //VerifyError: Error #1030: Stack depth is unbalanced. 1 != 0.

            //    at FlashTreasureHunt.ActionScript::FlashTreasureHunt___c__DisplayClassa4/_AttachGuardLogic_b__a1_8d06839e_060000ba()[U:\web\FlashTreasureHunt\ActionScript\FlashTreasureHunt___c__DisplayClassa4.as:51]
            //    at Function/http://adobe.com/AS3/2006/builtin::apply()
            //    at ScriptCoreLib.Shared.BCLImplementation.System::__Action/Invoke_4ebbe596_06001bb1()[U:\web\ScriptCoreLib\Shared\BCLImplementation\System\__Action.as:30]
            //    at FlashTreasureHunt.ActionScript::Extensions___c__DisplayClassc/_AtDelayDo_b__b_8d06839e_060001b5()[U:\web\FlashTreasureHunt\ActionScript\Extensions___c__DisplayClassc.as:21]
            //    at flash.utils::Timer/_timerDispatch()
            //    at flash.utils::Timer/tick()



            //        VerifyError: Error #1030: Stack depth is unbalanced. 1 != 0.

            //at ScriptCoreLib.ActionScript.RayCaster::ViewEngineBase/UpdatePOV_8d06839e_0600013a()[U:\web\ScriptCoreLib\ActionScript\RayCaster\ViewEngineBase.as:894]
            //at ScriptCoreLib.ActionScript.RayCaster::ViewEngineBase/RenderScene_8d06839e_06000140()[U:\web\ScriptCoreLib\ActionScript\RayCaster\ViewEngineBase.as:214]
            //at FlashTreasureHunt.ActionScript::FlashTreasureHunt/_InitializeMap_b__cb_8d06839e_06000056()[U:\web\FlashTreasureHunt\ActionScript\FlashTreasureHunt.as:2131]


            // flash will blow up here?
            var SpritesHaveChanged = _SpritesFromPOV == null || _SpritesFromPOV.Length != Sprites.Count;
            //  flag0 = (!(this._SpritesFromPOV != null) || (!((int(this._SpritesFromPOV.length)) == this.Sprites.Count)));


            UpdatePOV(SpritesHaveChanged);
        }

        protected object[] _SpritesFromPOV;
        public List<object> Sprites;

        public void UpdatePOV(bool SpritesHaveChanged)
        {
            //var list_11:__List_1;
            //var func_22:__Func_2;
            //var objectArray3:Array;


            //if (SpritesHaveChanged)
            //{
            //    this;
            //    list_11 = this.Sprites;

            //    if ((ApplicationSprite.CS___9__CachedAnonymousMethodDelegate1 == null))
            //    {
            //        ApplicationSprite.CS___9__CachedAnonymousMethodDelegate1 = new ScriptCoreLib.Shared.BCLImplementation.System.__Func_2(null, __IntPtr.op_Explicit_4ebbe596_06001244(ApplicationSprite._UpdatePOV_b__0_39d0131d_06000004));
            //    }

            //    func_22 = ApplicationSprite.CS___9__CachedAnonymousMethodDelegate1;
            //    objectArray3 = __Enumerable.ToArray_4ebbe596_06000398(__Enumerable.Select_4ebbe596_060003b1(list_11, func_22));
            //    this._SpritesFromPOV = objectArray3;
            //}







            if (SpritesHaveChanged)
                _SpritesFromPOV = Sprites.Select(i => new object()).ToArray();


            if (SpritesHaveChanged)
                _SpritesFromPOV = Sprites.Select(i => new object()).ToArray();
        }
    }
}
