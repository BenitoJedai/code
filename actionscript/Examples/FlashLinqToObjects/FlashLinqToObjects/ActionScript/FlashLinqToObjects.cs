using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System;
using ScriptCoreLib.ActionScript.flash.filters;
using System.Linq;


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
                y = margin + users.height + margin,
                height = 20,
            };

            var filter2 = new TextField
            {
                text = "a",
                x = margin,
                y = margin + users.height + margin + filter.height + margin,
                height = 20
            };

            var result = new TextField
            {
                text = "",
                x = margin,
                y = margin + users.height + margin + filter.height + margin + filter2.height + margin,
                height = 100,
                autoSize = TextFieldAutoSize.LEFT
            };


            Action Update =
                delegate
                {
                    try
                    {
                        var user_filter = filter.text.Trim().ToLower();
                        var user_filter2 = filter2.text.Trim().ToLower();

                        var __users = users.text.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);




                        result.text = "";



                        foreach (var v in from i in __users
                                          where i.Trim().ToLower().Contains(user_filter)
                                          select i.Trim())
                        {
                            result.text += "result: " + v + "\n";
                        }


                    }
                    catch (Exception e)
                    {
                        result.text = "error: " + e.Message;
                    }

                };

            ApplyStyle(users);
            ApplyStyle(filter);
            ApplyStyle(filter2);
            //ApplyStyle(result);

            users.change += delegate { Update(); };
            filter.change += delegate { Update(); };
            filter2.change += delegate { Update(); };

            new[] { users, filter, filter2, result }.AttachTo(this);

            Update();

        }
    }

}
