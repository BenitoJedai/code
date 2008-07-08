﻿using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System;
using ScriptCoreLib.ActionScript.flash.filters;
using System.Linq;
using System.Collections.Generic;


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
                    f.focusOut += delegate { f.backgroundColor = 0xc0c0c0; };

                    f.backgroundColor = 0xa0a0a0;

                    f.width = 160;
                };
            #endregion

            var DefaultUsers = new List<string>
            {
                "_martin",
                "mike",
                "mac",
                "ken",
                "neo",
                "zen",
                "jay",
                "morpheus",
                "trinity",
                "Agent Smith",
                "_psycho"
            };


            var users = new TextField
            {
                text = DefaultUsers.Aggregate(
                    new { i = 0, text = "" },
                    (seed, value) =>
                    {
                        if (seed.i == 0)
                            return new { i = 1, text = value };

                        return new { i = seed.i + 1, text = seed.text + ", " + value };
                    }
                ).text,


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
                height = 20,
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

                        var query = from i in __users
                                    where i.ToLower().Contains(user_filter)
                                    let name = i.Trim()
                                    let isspecial = i.ToLower().Contains(user_filter2)
                                    orderby isspecial
                                    select new { isspecial, length = name.Length, name };



                        //foreach (var v in from i in query
                        //                  where i.isspecial
                        //                  select i)
                        //{
                        //    result.text += "result: *** " + v.name + "\n";
                        //}


                        foreach (var v in query)
                        {
                            if (v.isspecial)
                                result.text += "result: *** " + v.name + "\n";
                            else
                                result.text += "result: " + v.name + "\n";
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

            users.change += delegate { Update(); };
            filter.change += delegate { Update(); };
            filter2.change += delegate { Update(); };

            Func<TextField, string, TextField> ToLabel =
                (e, text) =>
                {
                    var r =
                         new TextField
                        {
                            text = text,
                            x = e.x + e.width + margin,
                            y = e.y,
                            autoSize = TextFieldAutoSize.LEFT,
                            selectable = false
                        };

                    r.click +=
                        ev => stage.focus = e;

                    return r;
                };

            new[] { users, filter, filter2, result,
                ToLabel(users, "Enter a list of names separated by commas"),
                ToLabel(filter,  "Enter a partial name to be found from the list above."),
                ToLabel(filter2,  "Enter a partial name to make the entry special"),
                
            }.AttachTo(this);

            Update();

        }


    }

}
