using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using com.abstractatech.gamification.synergy.cashflow;
using com.abstractatech.gamification.synergy.cashflow.Design;
using com.abstractatech.gamification.synergy.cashflow.HTML.Pages;
using System.ComponentModel;
using System.Threading.Tasks;
using com.abstractatech.gamification.synergy.cashflow.HTML.Audio.FromAssets;
using System.Collections.Generic;

namespace com.abstractatech.gamification.synergy.cashflow
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed partial class Application
    {

        //{ SourceMethod = Int32<01e2> ldarg.0.try(<MoveNext>06000016, System.Runtime.CompilerServices.TaskAwaiter ByRef, <<_ctor>b__11>d__0 ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[System.String]
        //        ByRef) }
        //    script: error JSC1000: if block not detected correctly, opcode was { Branch = [0x0018]
        //    blt        +0 -2{[0x0006]
        //    ldfld      +1 -1{[0x0001]
        //    ldfld      +1 -1{[0x0000]
        //    ldarg.0    +1 -0} } } {[0x0017]
        //conv.i4    +1 -1{[0x0
        // assembly: V:\com.abstractatech.gamification.synergy.cashflow.Application.exe
        // type: com.abstractatech.gamification.synergy.cashflow.Application+<>c__DisplayClass0+<<_ctor>b__11>d__0+<MoveNext>06000016, com.abstractatech.gamification.synergy.cashflow.Application, Version=1.0.0.0, Culture=neutral
        // offset: 0x0018
        //  method:Int32<01e2> ldarg.0.try(<MoveNext>06000016, System.Runtime.CompilerServices.TaskAwaiter ByRef, <<_ctor>b__11>d__0 ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[System.String]
        //ByRef) }


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
            : this()
        {

            #region play
            page.BlurArea.onfocus +=
                delegate
                {
                    new drag().play();
                };

            page.Card.onfocus +=
                delegate
                {
                    new deal().play();
                };

            page.Card2.onfocus +=
               delegate
               {
                   new deal().play();
               };

            page.Card3.onfocus +=
             delegate
             {
                 new deal().play();
             };
            #endregion

            var Cards = new[] { page.Card, page.Card2, page.Card3 };
            var NFCTags = new List<string>();

            page.BlurArea.focus();

            "".With(
                async lastid =>
                {
                    var pause = 1;

                    while (true)
                        foreach (var ani in new[] { ".:.:.", ":.:.:" })
                        {
                            await Task.Delay(pause);
                            pause = 1;

                            ani.ToDocumentTitle();

                            // first time connection may get all cards?
                            lastid = await service.poll_onnfc(lastid,
                                async xml =>
                                {
                                    var id = xml.Attribute("id").Value;

                                    id.ToDocumentTitle();

                                    NFCTags.AddDistinct(id);


                                    pause = 5000;

                                    // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.TakeWhile(System.Collections.Generic.IEnumerable`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], System.Func`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]

                                    //var m = NFCTags.TakeWhile(k => k != id).Count();
                                    //var m = NFCTags.TakeWhile(k => k != id).Count();

                                    var Card = Cards[NFCTags.IndexOf(id) % Cards.Length];

                                    Card.blur();

                                    await Task.Delay(300);

                                    Card.focus();
                                }
                            );
                        }

                }
            );
        }

        private async void Application_Load(object sender, EventArgs e)
        {
        }

    }
}
