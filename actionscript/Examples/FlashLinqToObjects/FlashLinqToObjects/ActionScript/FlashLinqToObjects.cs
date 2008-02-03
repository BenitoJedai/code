using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System;
using ScriptCoreLib.ActionScript.flash.filters;


namespace FlashLinqToObjects.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashLinqToObjects : Sprite
    {
        public FlashLinqToObjects()
        {
            // this is a port from js:LinqToObjects

            var margin = 8;

            #region ApplyStyle
            Action<TextField> ApplyStyle =
                f =>
                {
                    f.background = true;
                    f.border = true;
                    f.type = TextFieldType.INPUT;

                    f.focusIn += delegate { f.backgroundColor = 0xffffff; };
                    f.focusOut += delegate { f.backgroundColor = 0xa0a0a0; };

                    f.backgroundColor = 0xa0a0a0;

                    f.width = 120;
                };
            #endregion

            var users = new TextField
            {
                text = "_martin, mike, mac, ken, neo, zen, jay, morpheous, trinity, Agent Smith, _psycho",
                wordWrap = true,
                multiline = true,
                x = margin,
                y = margin,
            };

            var filter = new TextField
            {
                text = "or",
                x = margin,
                y = margin + 100 + margin,
                height = 20,
            };

            var filter2 = new TextField
            {
                text = "a",
                x = margin,
                y = margin + 100 + margin + 20 + margin,
                height = 20
            };

            

            Action Update =
                delegate
                {
                    try
                    {
                        throw new Exception("changed");
                    }
                    catch (Exception e)
                    {
                        filter2.text = "catch: " + e.Message;
                    }
                    finally
                    {
                        filter.text = "finally";

                    }
                };

            ApplyStyle(users);
            ApplyStyle(filter);
            ApplyStyle(filter2);

            users.change += delegate { Update(); };
            filter.change += delegate { Update(); };
            filter2.change += delegate { Update(); };

            new[] { users, filter, filter2 }.AttachTo(this);



        }
    }

}
